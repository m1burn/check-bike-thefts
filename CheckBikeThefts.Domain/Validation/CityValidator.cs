using FluentValidation;

namespace CheckBikeThefts.Domain.Validation;

public class CityValidator : AbstractValidator<City>
{
    public CityValidator()
    {
        RuleFor(city => city.Id).GreaterThan(0);
        RuleFor(city => city.Name).NotEmpty();
        RuleFor(city => city.Country).NotEmpty();
    }
}