using Endpoints.Register;
using Microsoft.Extensions.Logging;
using FastEndpoints;
using Infrastructure.Interfaces.Services;

namespace Endpoints.Address;

public class AutoFillingEndpoint : Endpoint<AutoFillingRequest, IEnumerable<string>>
{
    private readonly ILogger<RegisterEndpoint> _logger;
    private readonly IDataRefineService _dataRefineService;

    public AutoFillingEndpoint(ILogger<RegisterEndpoint> logger, IDataRefineService dataRefineService)
    {
        _logger = logger;
        _dataRefineService = dataRefineService;
    }

    public override void Configure()
    {
        Get("api/address/auto-filling");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AutoFillingRequest request, CancellationToken cancellationToken)
    {
        var result = await _dataRefineService.AddressAutoFillingAsync(request.Text);

        if (result.IsSuccess) 
        {
            var res = result.Value.Select(s => s.Note);

            await SendAsync(res);
            return;
        }

        await SendErrorsAsync();
        return;
    }
}
