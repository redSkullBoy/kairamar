using AutoMapper;
using Domain.Entities.Model;
using Infrastructure.Interfaces.Dto;

namespace UseCases.Handlers.Addresses.Mappings;

public class AddressAutoMapperProfile : Profile
{
    public AddressAutoMapperProfile()
    {
        CreateMap<AddressDataDto, Address>()
            ;
    }
}
