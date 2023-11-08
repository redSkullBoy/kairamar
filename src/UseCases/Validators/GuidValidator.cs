using FluentValidation;

namespace UseCases.Validators;

public class GuidValidator : AbstractValidator<Guid>
{
    public GuidValidator()
    {
        RuleFor(x => x).NotEmpty();
    }
}