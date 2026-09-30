using FluentValidation.TestHelper;
using VehicleApp.Application.Commands.Models.UpdateModel;
using Xunit;

namespace VehicleApp.UnitTests;

public class UpdateModelCommandValidatorTests
{
    private readonly UpdateModelCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Id_Is_Zero()
    {
        var result = _validator.TestValidate(new UpdateModelCommand(0, 1, "Test", "TM"));
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Should_Have_Error_When_MakeId_Is_Zero()
    {
        var result = _validator.TestValidate(new UpdateModelCommand(1, 0, "Test", "TM"));
        result.ShouldHaveValidationErrorFor(x => x.MakeId);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Empty()
    {
        var result = _validator.TestValidate(new UpdateModelCommand(1, 1, "", "TM"));
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_Not_Have_Errors_When_Valid()
    {
        var result = _validator.TestValidate(new UpdateModelCommand(1, 1, "Test", "TM"));
        result.ShouldNotHaveAnyValidationErrors();
    }
}
