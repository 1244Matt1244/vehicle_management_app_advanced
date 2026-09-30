using FluentValidation.TestHelper;
using VehicleApp.Application.Commands.Makes.UpdateMake;
using Xunit;

namespace VehicleApp.UnitTests;

public class UpdateMakeCommandValidatorTests
{
    private readonly UpdateMakeCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Id_Is_Zero()
    {
        var result = _validator.TestValidate(new UpdateMakeCommand(0, "Test", "TM"));
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Empty()
    {
        var result = _validator.TestValidate(new UpdateMakeCommand(1, "", "TM"));
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_Not_Have_Errors_When_Valid()
    {
        var result = _validator.TestValidate(new UpdateMakeCommand(1, "Test", "TM"));
        result.ShouldNotHaveAnyValidationErrors();
    }
}
