using FluentValidation;

namespace VehicleApp.Application.Commands.Models.CreateModel;

public class CreateModelCommandValidator : AbstractValidator<CreateModelCommand>
{
    public CreateModelCommandValidator()
    {
        RuleFor(x => x.MakeId)
            .GreaterThan(0).WithMessage("MakeId must be greater than 0.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Abrv)
            .NotEmpty().WithMessage("Abbreviation is required.")
            .MaximumLength(10);
    }
}
