using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UseCases.Handlers.Addresses.Mappings;
using UseCases.Handlers.Trips.Dto;
using UseCases.Handlers.Trips.Validators;
using Utils.Modules;

namespace UseCases;

public class UseCasesModule : Module
{
    public override void Load(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AddressAutoMapperProfile));

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(UseCasesModule).Assembly);
        });

        //FluentValidation
        services.AddTransient<IValidator<CreateTripDto>, CreateTripDtoValidator<CreateTripDto>>();
        services.AddTransient<IValidator<TripFilter>, TripFilterValidator>();
    }
}
