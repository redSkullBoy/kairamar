using Ardalis.Result;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using UseCases.Handlers.Trips.Dto;
using UseCases.Handlers.Trips.Queries;

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
        Get("trip");
        AllowAnonymous();
    }

    public override async Task HandleAsync(TripFilter filter, CancellationToken cancellationToken)
    {
        if (filter == null)
        {
            AddError("в запросе нет данных");
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

        var resultGetAll = await _mediator.Send(new GetAllRequest { Value = filter }, cancellationToken);

        //var result = resultGetAll.Map(s => new List<TripDto>(s.Value));

        //await ResultToSendAsync(result);

        return;
    }
}