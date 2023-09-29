using FluentValidation;
using UseCases.Handlers.Trips.Dto;

namespace UseCases.Handlers.Trips.Validators;

public class TripFilterValidator : AbstractValidator<TripFilter>
{
    public TripFilterValidator()
    {
        RuleFor(fl => fl.FromAddressId)
            .NotEmpty().WithMessage("Поле {PropertyName} обязательно для заполнения")
            ;
        RuleFor(fl => fl.ToAddressId)
            .NotEmpty().WithMessage("Поле {PropertyName} обязательно для заполнения")
            ;
        RuleFor(fl => fl.StartDateLocal)
            .NotNull().WithMessage("Поле {PropertyName} обязательно для заполнения")
            ;
        RuleFor(fl => fl.RequestedSeats)
            .NotEmpty().WithMessage("Поле {PropertyName} обязательно для заполнения")
            ;
    }
}