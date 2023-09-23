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

    public async Task<Result<List<AddressDataDto>>> AddressAutoFillingAsync(string text)
    {
        var addressData = new List<AddressDataDto>();

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
            foreach (var suggest in response.suggestions)
            {
                var addressDto = new AddressDataDto
                {
                    Note = suggest.unrestricted_value,
                    PostalCode = suggest.data.postal_code,
                    Country = suggest.data.country,
                    City = suggest.data.city,
                    Region = suggest.data.region,
                };

                addressData.Add(addressDto);
            }

            return Result.Success(addressData);
        }

        return Result.NotFound();
    }
}
