using DataAccess.Sqlite;
using Infrastructure.Implementation;
using Utils.Modules;
using UseCases;
using FastEndpoints;
using FastEndpoints.Swagger;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add project modules
        builder.Services.RegisterModule<DataAccessModule>(builder.Configuration);
        builder.Services.RegisterModule<InfrastructureModule>(builder.Configuration);
        builder.Services.RegisterModule<UseCasesModule>(builder.Configuration);

        //builder.Services.AddAutoMapper(typeof(OrdersAutoMapperProfile));

        // Add services to the container.
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddOptions();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddFastEndpoints();
        builder.Services.SwaggerDocument(); //add this

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseFastEndpoints();
        app.UseSwaggerGen(); //add this

        app.MapControllers();

        app.Run();
    }
}
