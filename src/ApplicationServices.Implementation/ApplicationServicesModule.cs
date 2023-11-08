using ApplicationServices.Implementation.Services;
using ApplicationServices.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Utils.Modules;

namespace ApplicationServices.Implementation;

public class ApplicationServicesModule : Module
{
    public override void Load(IServiceCollection services)
    {
        services.AddTransient<IAddressService, AddressService>();
    }
}