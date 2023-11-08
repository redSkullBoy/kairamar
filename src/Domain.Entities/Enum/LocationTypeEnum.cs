using System.ComponentModel;

namespace Domain.Entities.Enum;

public enum LocationTypeEnum
{
    [Description("Страна")]
    Country = 0,
    
    [Description("Регион")]
    Region = 1,
    
    [Description("Район")]
    District = 3,
    
    [Description("Город")]
    City = 4,
    
    [Description("Район города")]
    CityDistrict = 5,
    
    [Description("Населенный пункт")]
    Settlement = 6,
    
    [Description("Улица")]
    Street = 7,
    
    [Description("Дом")]
    House = 8,
    
    [Description("Квартира или комната")]
    Apartment = 9,
    
    [Description("Планировочная структура")]
    LayoutStructure = 65,
    
    [Description("Земельный участок")]
    LandPlot = 75,
    
    [Description("Иностранный или пустой")]
    ForeignOrEmpty = -1
}
