using FluentValidation;
using PersonManagement.BLL.DTOs;

namespace PersonManagement.BLL.Validators;

public class PersonCreateValidator : AbstractValidator<PersonCreateDto>
{
    public PersonCreateValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
        When(x => !string.IsNullOrWhiteSpace(x.City) || !string.IsNullOrWhiteSpace(x.AddressLine), () =>
        {
            RuleFor(x => x.City).NotEmpty().WithMessage("City is required if address is filled.");
            RuleFor(x => x.AddressLine).NotEmpty().WithMessage("AddressLine is required if city is filled.");
        });
    }
}
