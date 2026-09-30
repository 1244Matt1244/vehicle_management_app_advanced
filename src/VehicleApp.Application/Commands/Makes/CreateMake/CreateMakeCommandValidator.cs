using FluentValidation;

namespace VehicleApp.Application.Commands.Makes.CreateMake;

public class CreateMakeCommandValidator : AbstractValidator<CreateMakeCommand>
{
    public CreateMakeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Abrv)
            .NotEmpty().WithMessage("Abbreviation is required.")
            .MaximumLength(10);
    }
}
