using FluentValidation;

namespace VehicleApp.Application.Commands.Makes.UpdateMake;

public class UpdateMakeCommandValidator : AbstractValidator<UpdateMakeCommand>
{
    public UpdateMakeCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Abrv)
            .NotEmpty().WithMessage("Abbreviation is required.")
            .MaximumLength(10);
    }
}
