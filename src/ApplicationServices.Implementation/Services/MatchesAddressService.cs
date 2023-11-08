using Domain.Entities.Model;
using Infrastructure.Interfaces.Dto;
using Microsoft.EntityFrameworkCore;

namespace ApplicationServices.Implementation.Services;

public class MatchesAddressService
{
    private MatchesAddress _matchesAddress;

    public MatchesAddressService(MatchesAddress matchesAddress)
    {
        _matchesAddress = matchesAddress;
    }

    public async Task<(bool isNew, int? id)> FindAsync(AddressDataDto addressDto, IQueryable<Address> address)
    {
        var newItem = !_matchesAddress.Any(addressDto, address);

        if (newItem)
        {
            return (newItem, null);
        }
        var id = await _matchesAddress.GetIdAsync(addressDto, address);

        return (newItem, id);
    }
}

public abstract class MatchesAddress
{
    public abstract bool Any(AddressDataDto addressDto, IQueryable<Address> address);

    public abstract Task<int> GetIdAsync(AddressDataDto addressDto, IQueryable<Address> address);
}

public class CountryMatchesAddress : MatchesAddress
{
    public override bool Any(AddressDataDto addressDto, IQueryable<Address> address)
    {
        return address.Any(s => s.Country == addressDto.Country);
    }

    public override async Task<int> GetIdAsync(AddressDataDto addressDto, IQueryable<Address> address)
    {
        var id = await address.Where(s => s.Country == addressDto.Country).Select(s => s.Id).FirstAsync();
        return id;
    }
}

public class RegionMatchesAddress : MatchesAddress
{
    public override bool Any(AddressDataDto addressDto, IQueryable<Address> address)
    {
        return address.Any(s => s.RegionFiasId == addressDto.RegionFiasId);
    }

    public override async Task<int> GetIdAsync(AddressDataDto addressDto, IQueryable<Address> address)
    {
        var id = await address.Where(s => s.RegionFiasId == addressDto.RegionFiasId).Select(s => s.Id).FirstAsync();
        return id;
    }
}

public class CityMatchesAddress : MatchesAddress
{
    public override bool Any(AddressDataDto addressDto, IQueryable<Address> address)
    {
        return address.Any(s => s.CityFiasId == addressDto.CityFiasId);
    }

    public override async Task<int> GetIdAsync(AddressDataDto addressDto, IQueryable<Address> address)
    {
        var id = await address.Where(s => s.CityFiasId == addressDto.CityFiasId).Select(s => s.Id).FirstAsync();
        return id;
    }
}

public class CityDistrictMatchesAddress : MatchesAddress
{
    public override bool Any(AddressDataDto addressDto, IQueryable<Address> address)
    {
        return address.Any(s => s.CityDistrictFiasId == addressDto.CityDistrictFiasId);
    }

    public override async Task<int> GetIdAsync(AddressDataDto addressDto, IQueryable<Address> address)
    {
        var id = await address.Where(s => s.CityDistrictFiasId == addressDto.CityDistrictFiasId).Select(s => s.Id).FirstAsync();
        return id;
    }
}

public class SettlementMatchesAddress : MatchesAddress
{
    public override bool Any(AddressDataDto addressDto, IQueryable<Address> address)
    {
        return address.Any(s => s.SettlementFiasId == addressDto.SettlementFiasId);
    }

    public override async Task<int> GetIdAsync(AddressDataDto addressDto, IQueryable<Address> address)
    {
        var id = await address.Where(s => s.SettlementFiasId == addressDto.SettlementFiasId).Select(s => s.Id).FirstAsync();
        return id;
    }
}

public class StreetMatchesAddress : MatchesAddress
{
    public override bool Any(AddressDataDto addressDto, IQueryable<Address> address)
    {
        return address.Any(s => s.StreetFiasId == addressDto.StreetFiasId);
    }

    public override async Task<int> GetIdAsync(AddressDataDto addressDto, IQueryable<Address> address)
    {
        var id = await address.Where(s => s.StreetFiasId == addressDto.StreetFiasId).Select(s => s.Id).FirstAsync();
        return id;
    }
}

public class HouseMatchesAddress : MatchesAddress
{
    public override bool Any(AddressDataDto addressDto, IQueryable<Address> address)
    {
        return address.Any(s => s.HouseFiasId == addressDto.HouseFiasId);
    }

    public override async Task<int> GetIdAsync(AddressDataDto addressDto, IQueryable<Address> address)
    {
        var id = await address.Where(s => s.HouseFiasId == addressDto.HouseFiasId).Select(s => s.Id).FirstAsync();
        return id;
    }
}

public class LayoutStructureMatchesAddress : MatchesAddress
{
    public override bool Any(AddressDataDto addressDto, IQueryable<Address> address)
    {
        return address.Any(s => s.AreaFiasId == addressDto.AreaFiasId);
    }

    public override async Task<int> GetIdAsync(AddressDataDto addressDto, IQueryable<Address> address)
    {
        var id = await address.Where(s => s.AreaFiasId == addressDto.AreaFiasId).Select(s => s.Id).FirstAsync();
        return id;
    }
}