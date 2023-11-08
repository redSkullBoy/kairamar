using ApplicationServices.Interfaces;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities.Enum;
using Domain.Entities.Model;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Dto;
using Infrastructure.Interfaces.Services;

namespace ApplicationServices.Implementation.Services;

public class AddressService : IAddressService
{
    private readonly IDbContext _context;
    private readonly IDataRefineService _dataRefineService;
    private readonly IMapper _mapper;

    public AddressService(IDbContext context, IDataRefineService dataRefineService, IMapper mapper)
    {
        _context = context;
        _dataRefineService = dataRefineService;
        _mapper = mapper;
    }
    public async Task<Result<IList<int>>> GetAddresIds(string text, CancellationToken cancellationToken)
    {
        var newAddressDataDtos = new List<AddressDataDto>();
        var resultAddresId = new List<int>();

        var result = await _dataRefineService.AddressAutoFillingAsync(text, cancellationToken);

        if (!result.IsSuccess)
        {
            return Result.NotFound();
        }

        var addresses = _context.Addresses.AsQueryable();

        var matchesAddressDic = new Dictionary<LocationTypeEnum, MatchesAddress>()
            {
                { LocationTypeEnum.Country, new CountryMatchesAddress() },
                { LocationTypeEnum.Region, new RegionMatchesAddress() },
                { LocationTypeEnum.City, new CityMatchesAddress() },
                { LocationTypeEnum.CityDistrict, new CityDistrictMatchesAddress() },
                { LocationTypeEnum.Settlement, new SettlementMatchesAddress() },
                { LocationTypeEnum.Street, new StreetMatchesAddress() },
                { LocationTypeEnum.House, new HouseMatchesAddress() },
                { LocationTypeEnum.LayoutStructure, new LayoutStructureMatchesAddress() }
            };

        foreach ( var addressDto in result.Value)
        {
            if(!matchesAddressDic.TryGetValue(addressDto.FiasLevel, out var matchesAddress))
            {
                newAddressDataDtos.Add(addressDto);
                continue; //пропуск
            }

            var serv = new MatchesAddressService(matchesAddressDic[addressDto.FiasLevel]);

            var resultAd = await serv.FindAsync(addressDto, addresses);

            if (resultAd.isNew)
            {
                newAddressDataDtos.Add(addressDto);
            }
            else
            {
                resultAddresId.Add((int)resultAd.id!);
            }
        }

        var addressDb = _mapper.Map<IEnumerable<Address>>(newAddressDataDtos);

        await _context.Addresses.AddRangeAsync(addressDb);

        resultAddresId.AddRange(addressDb.Select(s => s.Id));

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }

        return resultAddresId;
    }
}
