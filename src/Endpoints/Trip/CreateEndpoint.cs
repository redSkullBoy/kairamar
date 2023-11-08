using MediatR;
using FluentValidation;
using Microsoft.Extensions.Logging;
using UseCases.Handlers.Trips.Commands;
using UseCases.Handlers.Trips.Dto;

namespace Endpoints.Trip;

public class CreateEndpoint : AppEndpoint<CreateTripDto, TripDto>
{
    private readonly ILogger<CreateEndpoint> _logger;
    private readonly IMediator _mediator;
    private IValidator<CreateTripDto> _createValidator;

    public CreateEndpoint(ILogger<CreateEndpoint> logger, IMediator mediator, IValidator<CreateTripDto> createValidator)
    {
        _logger = logger;
        _mediator = mediator;
        _createValidator = createValidator;
    }

    public override void Configure()
    {
        Post("trip");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateTripDto dto, CancellationToken cancellationToken)
    {
        if (dto == null)
        {
            AddError("в запросе нет данных");
            await SendErrorsAsync();
            return;
        }

        var resultValid = await _createValidator.ValidateAsync(dto);

        if (!resultValid.IsValid)
        {
            foreach (var error in resultValid.Errors)
                AddError(error);

            await SendErrorsAsync();
            return;
        }

        var result = await _mediator.Send(new CreateCommand { Value = dto }, cancellationToken);

        await ResultToSendAsync(result);

        return;
    }
}