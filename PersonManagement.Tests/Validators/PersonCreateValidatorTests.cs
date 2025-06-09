using FluentValidation.TestHelper;
using PersonManagement.BLL.Validators;
using Faker;
using PersonManagement.BLL.Responses;

namespace PersonManagement.Tests.Validators;

public class PersonCreateValidatorTests
{
    private readonly PersonCreateValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_FirstName_Empty()
    {
        // Arrange
        var model = new PersonCreateResponse
        {
            FirstName = "",
            LastName = Name.Last()
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void Should_Pass_When_All_Valid()
    {
        // Arrange
        var model = new PersonCreateResponse
        {
            FirstName = Name.First(),
            LastName = Name.Last(),
            City = Address.City(),
            AddressLine = Address.StreetAddress()
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Require_AddressLine_If_City_Provided()
    {
        // Arrange
        var model = new PersonCreateResponse
        {
            FirstName = Name.First(),
            LastName = Name.Last(),
            City = Address.City()
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.AddressLine);
    }
}
