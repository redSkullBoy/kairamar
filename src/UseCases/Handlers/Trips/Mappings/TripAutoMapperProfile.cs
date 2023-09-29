using AutoMapper;
using Domain.Entities.Model;
using UseCases.Handlers.Trips.Dto;

namespace UseCases.Handlers.Trips.Mappings;

public class TripAutoMapperProfile : Profile
{
    public TripAutoMapperProfile()
    {
        CreateMap<CreateTripDto, Trip>()
            ;
        CreateMap<Trip, TripDto>()
            ;
    }
}
