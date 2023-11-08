using Ardalis.Result;
using Dadata;
using Domain.Entities.Enum;
using Infrastructure.Interfaces.Configurations;
using Infrastructure.Interfaces.Dto;
using Infrastructure.Interfaces.Services;
using Microsoft.Extensions.Options;
using Utils.Extensions;

namespace Infrastructure.Implementation.Services;

public class DadataRefineService : IDataRefineService
{
    private readonly IOptions<DadataRefineConfiguration> _config;

    public DadataRefineService(IOptions<DadataRefineConfiguration> options)
    {
        _config = options;
    }

    public async Task<Result<IList<AddressDataDto>>> AddressAutoFillingAsync(string text, CancellationToken ctn)
    {
        IList<AddressDataDto> addressData = new List<AddressDataDto>();

        var response = new Dadata.Model.SuggestResponse<Dadata.Model.Address>();

        try
        {
            var api = new SuggestClientAsync(_config.Value.Token);
            response = await api.SuggestAddress(text, 5, ctn);
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
                    CountryIsoCode = suggest.data.country_iso_code,
                    RegionIsoCode = suggest.data.region_iso_code,
                    RegionFiasId = suggest.data.region_fias_id,
                    RegionKladrId = suggest.data.region_kladr_id,
                    RegionWithType = suggest.data.region_with_type,
                    RegionType = suggest.data.region_type,
                    RegionTypeFull = suggest.data.region_type_full,
                    Region = suggest.data.region,
                    AreaFiasId = suggest.data.area_fias_id,
                    AreaKladrId = suggest.data.area_kladr_id,
                    AreaWithType = suggest.data.area_with_type,
                    AreaType = suggest.data.area_type,
                    AreaTypeFull = suggest.data.area_type_full,
                    Area = suggest.data.area,
                    CityFiasId = suggest.data.city_fias_id,
                    CityKladrId = suggest.data.city_kladr_id,
                    CityWithType = suggest.data.city_with_type,
                    CityType = suggest.data.city_type,
                    CityTypeFull = suggest.data.city_type_full,
                    City = suggest.data.city,
                    CityArea = suggest.data.city_area,
                    CityDistrictFiasId = suggest.data.city_district_fias_id,
                    CityDistrictKladrId = suggest.data.city_district_kladr_id,
                    CityDistrictWithType = suggest.data.city_district_with_type,
                    CityDistrictType = suggest.data.city_district_type,
                    CityDistrictTypeFull = suggest.data.city_district_type_full,
                    SettlementFiasId = suggest.data.settlement_fias_id,
                    SettlementKladrId = suggest.data.settlement_kladr_id,
                    SettlementWithType = suggest.data.settlement_with_type,
                    SettlementType = suggest.data.settlement_type,
                    SettlementTypeFull = suggest.data.settlement_type_full,
                    Settlement = suggest.data.settlement,
                    StreetFiasId = suggest.data.street_fias_id,
                    StreetKladrId = suggest.data.street_kladr_id,
                    StreetWithType = suggest.data.street_with_type,
                    StreetType = suggest.data.street_type,
                    StreetTypeFull = suggest.data.street_type_full,
                    Street = suggest.data.street,
                    HouseFiasId = suggest.data.house_fias_id,
                    HouseKladrId = suggest.data.house_kladr_id,
                    HouseType = suggest.data.house_type,
                    HouseTypeFull = suggest.data.house_type_full,
                    House = suggest.data.house,
                    BlockType = suggest.data.block_type,
                    BlockTypeFull = suggest.data.block_type_full,
                    Block = suggest.data.block,
                    FlatType = suggest.data.flat_type,
                    FlatTypeFull = suggest.data.flat_type_full,
                    Flat = suggest.data.flat,
                    FlatArea = suggest.data.flat_area,
                    SquareMeterPrice = suggest.data.square_meter_price,
                    FlatPrice = suggest.data.flat_price,
                    PostalBox = suggest.data.postal_box,
                    FiasId = suggest.data.fias_id,
                    FiasLevel = suggest.data.fias_level.ToEnum<LocationTypeEnum>(),
                    FiasActualityState = suggest.data.fias_actuality_state,
                    KladrId = suggest.data.kladr_id,
                    CapitalMarker = suggest.data.capital_marker,
                    Okato = suggest.data.okato,
                    Oktmo = suggest.data.oktmo,
                    TaxOffice = suggest.data.tax_office,
                    TaxOfficeLegal = suggest.data.tax_office_legal,
                    Timezone = suggest.data.timezone,
                    GeoLat = suggest.data.geo_lat,
                    GeoLon = suggest.data.geo_lon,
                    BeltwayHit = suggest.data.beltway_hit,
                    BeltwayDistance = suggest.data.beltway_distance,
                    QcGeo = suggest.data.qc_geo,
                    QcComplete = suggest.data.qc_complete,
                    QcHouse = suggest.data.qc_house,
                    HistoryValues = suggest.data.history_values,
                    UnparsedParts = suggest.data.unparsed_parts,
                    Source = suggest.data.source,
                    Qc = suggest.data.qc,
                };

                addressData.Add(addressDto);
            }

            return Result.Success(addressData);
        }

        return Result.NotFound();
    }
}
