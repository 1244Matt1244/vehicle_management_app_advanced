using FluentAssertions;
using FluentValidation.TestHelper;
using VehicleApp.Application.Commands.Makes.CreateMake;
using Xunit;

namespace VehicleApp.UnitTests;

public class CreateMakeCommandValidatorTests
{
    private readonly CreateMakeCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var result = _validator.TestValidate(new CreateMakeCommand("", "TM"));
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Too_Long()
    {
        var longName = new string('A', 101);
        var result = _validator.TestValidate(new CreateMakeCommand(longName, "TM"));
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Abrv_Is_Empty()
    {
        var result = _validator.TestValidate(new CreateMakeCommand("Test", ""));
        result.ShouldHaveValidationErrorFor(x => x.Abrv);
    }

    [Fact]
    public void Should_Have_Error_When_Abrv_Too_Long()
    {
        var result = _validator.TestValidate(new CreateMakeCommand("Test", "TOOLONGABRV"));
        result.ShouldHaveValidationErrorFor(x => x.Abrv);
    }

    [Fact]
    public void Should_Not_Have_Errors_When_Valid()
    {
        var result = _validator.TestValidate(new CreateMakeCommand("Test Make", "TM"));
        result.ShouldNotHaveAnyValidationErrors();
    }
}
