using Microsoft.Extensions.DependencyInjection;
using UseCases.Handlers.Addresses.Mappings;
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
    }
}
