using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using UseCases.Handlers.Trips.Commands;
using UseCases.Handlers.Trips.Dto;

namespace Endpoints.Trip;

public class AllEndpoint : AppEndpoint<TripFilter, List<TripDto>>
{
    private readonly ILogger<CreateEndpoint> _logger;
    private readonly IMediator _mediator;
    private IValidator<TripFilter> _createValidator;

    public AllEndpoint(ILogger<CreateEndpoint> logger, IMediator mediator, IValidator<TripFilter> createValidator)
    {
        _logger = logger;
        _mediator = mediator;
        _createValidator = createValidator;
    }

    public override void Configure()
    {
        Get("api/trip");
        AllowAnonymous();
    }

    public override async Task HandleAsync(TripFilter filter, CancellationToken cancellationToken)
    {
        if (filter == null)
        {
            ThrowError("в запросе нет данных");
            await SendErrorsAsync();
            return;
        }

        var resultValid = await _createValidator.ValidateAsync(filter);

        if (!resultValid.IsValid)
        {
            AddError(resultValid.Errors);
            await SendErrorsAsync();
            return;
        }

        //var result = await _mediator.Send(new CreateCommand { Value = dto }, cancellationToken);

        //await ResultToSendAsync(result);

        return;
    }
}