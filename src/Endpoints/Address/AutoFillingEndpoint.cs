using Microsoft.Extensions.Logging;
using FastEndpoints;
using MediatR;
using UseCases.Handlers.Addresses.Commands;

namespace Endpoints.Address;

public class AutoFillingEndpoint : Endpoint<AutoFillingRequest, IEnumerable<string>>
{
    private readonly ILogger<AutoFillingEndpoint> _logger;
    private readonly IMediator _mediator;

    public AutoFillingEndpoint(ILogger<AutoFillingEndpoint> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("address/auto-filling");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AutoFillingRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new AutoFillingCommand { Text = request.Text }, cancellationToken);

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
