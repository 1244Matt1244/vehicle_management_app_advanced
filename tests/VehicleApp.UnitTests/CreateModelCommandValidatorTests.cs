using FluentValidation.TestHelper;
using VehicleApp.Application.Commands.Models.CreateModel;
using Xunit;

namespace VehicleApp.UnitTests;

public class CreateModelCommandValidatorTests
{
    private readonly CreateModelCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_MakeId_Is_Zero()
    {
        var result = _validator.TestValidate(new CreateModelCommand(0, "Test", "TM"));
        result.ShouldHaveValidationErrorFor(x => x.MakeId);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Empty()
    {
        var result = _validator.TestValidate(new CreateModelCommand(1, "", "TM"));
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Abrv_Empty()
    {
        var result = _validator.TestValidate(new CreateModelCommand(1, "Test", ""));
        result.ShouldHaveValidationErrorFor(x => x.Abrv);
    }

    [Fact]
    public void Should_Not_Have_Errors_When_Valid()
    {
        var result = _validator.TestValidate(new CreateModelCommand(1, "Test Model", "TM"));
        result.ShouldNotHaveAnyValidationErrors();
    }
}
