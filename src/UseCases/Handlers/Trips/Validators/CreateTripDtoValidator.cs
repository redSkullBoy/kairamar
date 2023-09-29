using FluentValidation;
using UseCases.Handlers.Trips.Dto;

namespace UseCases.Handlers.Trips.Validators;

public class CreateTripDtoValidator<T> : AbstractValidator<T> where T : CreateTripDto
{
    public CreateTripDtoValidator()
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
        RuleFor(fl => fl.EndDateLocal)
            .NotNull().WithMessage("Поле {PropertyName} обязательно для заполнения")
            ;
        RuleFor(fl => fl.RequestedSeats)
            .NotEmpty().WithMessage("Поле {PropertyName} обязательно для заполнения")
            ;
        RuleFor(fl => fl.Description)
            .NotEmpty().WithMessage("Поле {PropertyName} обязательно для заполнения")
            .Length(5, 250).WithMessage("Длина поля {PropertyName} от {MinLength} до {MaxLength} символов")
            ;
    }
}
