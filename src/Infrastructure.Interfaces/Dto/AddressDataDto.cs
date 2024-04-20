using Domain.Entities.Enum;

namespace Infrastructure.Interfaces.Dto;

public class AddressDataDto
{
    public string FiasId { get; set; } = string.Empty;

    public LocationTypeEnum FiasLevel { get; set; }

    public string Value { get; set; } = string.Empty;

    public string Note { get; set; } = string.Empty;

    public string PostalCode { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public string CountryIsoCode { get; set; } = string.Empty;

    public string RegionIsoCode { get; set; } = string.Empty;

    public string RegionFiasId { get; set; } = string.Empty;

    public string RegionKladrId { get; set; } = string.Empty;

    public string RegionWithType { get; set; } = string.Empty;

    public string RegionType { get; set; } = string.Empty;

    public string RegionTypeFull { get; set; } = string.Empty;

    public string Region { get; set; } = string.Empty;

    public string AreaFiasId { get; set; } = string.Empty;

    public string AreaKladrId { get; set; } = string.Empty;

    public string AreaWithType { get; set; } = string.Empty;

    public string AreaType { get; set; } = string.Empty;

    public string AreaTypeFull { get; set; } = string.Empty;

    public string Area { get; set; } = string.Empty;

    public string CityFiasId { get; set; } = string.Empty;

    public string CityKladrId { get; set; } = string.Empty;

    public string CityWithType { get; set; } = string.Empty;

    public string CityType { get; set; } = string.Empty;

    public string CityTypeFull { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string CityArea { get; set; } = string.Empty;

    public string CityDistrictFiasId { get; set; } = string.Empty;

    public string CityDistrictKladrId { get; set; } = string.Empty;

    public string CityDistrictWithType { get; set; } = string.Empty;

    public string CityDistrictType { get; set; } = string.Empty;

    public string CityDistrictTypeFull { get; set; } = string.Empty;

    public string CityDistrictArea { get; set; } = string.Empty;

    public string SettlementFiasId { get; set; } = string.Empty;

    public string SettlementKladrId { get; set; } = string.Empty;

    public string SettlementWithType { get; set; } = string.Empty;

    public string SettlementType { get; set; } = string.Empty;

    public string SettlementTypeFull { get; set; } = string.Empty;

    public string Settlement { get; set; } = string.Empty;

    public string StreetFiasId { get; set; } = string.Empty;

    public string StreetKladrId { get; set; } = string.Empty;

    public string StreetWithType { get; set; } = string.Empty;

    public string StreetType { get; set; } = string.Empty;

    public string StreetTypeFull { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public string HouseFiasId { get; set; } = string.Empty;

    public string HouseKladrId { get; set; } = string.Empty;

    public string HouseType { get; set; } = string.Empty;

    public string HouseTypeFull { get; set; } = string.Empty;

    public string House { get; set; } = string.Empty;

    public string BlockType { get; set; } = string.Empty;

    public string BlockTypeFull { get; set; } = string.Empty;

    public string Block { get; set; } = string.Empty;

    public string FlatType { get; set; } = string.Empty;

    public string FlatTypeFull { get; set; } = string.Empty;

    public string Flat { get; set; } = string.Empty;

    public string FlatArea { get; set; } = string.Empty;

    public string SquareMeterPrice { get; set; } = string.Empty;

    public string FlatPrice { get; set; } = string.Empty;

    public string PostalBox { get; set; } = string.Empty;

    public string FiasActualityState { get; set; } = string.Empty;

    public string KladrId { get; set; } = string.Empty;

    public string CapitalMarker { get; set; } = string.Empty;

    public string Okato { get; set; } = string.Empty;

    public string Oktmo { get; set; } = string.Empty;

    public string TaxOffice { get; set; } = string.Empty;

    public string TaxOfficeLegal { get; set; } = string.Empty;

    public string Timezone { get; set; } = string.Empty;

    public string GeoLat { get; set; } = string.Empty;

    public string GeoLon { get; set; } = string.Empty;

    public string BeltwayHit { get; set; } = string.Empty;

    public string BeltwayDistance { get; set; } = string.Empty;

    public string QcGeo { get; set; } = string.Empty;

    public string QcComplete { get; set; } = string.Empty;

    public string QcHouse { get; set; } = string.Empty;

    public List<string>? HistoryValues { get; set; }

    public string UnparsedParts { get; set; } = string.Empty;

    public string Source { get; set; } = string.Empty;

    public string Qc { get; set; } = string.Empty;
}
