using FluentValidation.TestHelper;
using PersonManagement.BLL.DTOs;
using PersonManagement.BLL.Validators;
using Faker;

namespace PersonManagement.Tests.Validators;

public class PersonCreateValidatorTests
{
    private readonly PersonCreateValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_FirstName_Empty()
    {
        var model = new PersonCreateDto
        {
            FirstName = "",
            LastName = Name.Last()
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void Should_Pass_When_All_Valid()
    {
        var model = new PersonCreateDto
        {
            FirstName = Name.First(),
            LastName = Name.Last(),
            City = Address.City(),
            AddressLine = Address.StreetAddress()
        };

        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Require_AddressLine_If_City_Provided()
    {
        var model = new PersonCreateDto
        {
            FirstName = Name.First(),
            LastName = Name.Last(),
            City = Address.City()
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.AddressLine);
    }
}
