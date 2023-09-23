using Ardalis.Result;
using Dadata;
using Infrastructure.Interfaces.Configurations;
using Infrastructure.Interfaces.Dto;
using Infrastructure.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace Infrastructure.Implementation.Services;

public class DadataRefineService : IDataRefineService
{
    private readonly IOptions<DadataRefineConfiguration> _config;

    public DadataRefineService(IOptions<DadataRefineConfiguration> options)
    {
        _config = options;
    }

    public async Task<Result<AddressDataDto>> AddressAutoFillingAsync(string text)
    {
        var addressData = new AddressDataDto();

        var response = new Dadata.Model.SuggestResponse<Dadata.Model.Address>();

        try
        {
            var api = new SuggestClientAsync(_config.Value.Token);
            response = await api.SuggestAddress(text);
        } 
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }

        if (response.suggestions.Count > 0)
        {
            var address = response.suggestions[0].data;

            addressData.PostalCode = address.postal_code;
            addressData.Country = address.country;
            addressData.City = address.city;
            addressData.Region = address.region;

            return Result.Success(addressData);
        }

        return Result.NotFound();
    }
}
