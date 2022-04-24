using FluentValidation;

namespace CheckBikeThefts.UseCases.RiskAssessment;

public class GetCityBikeTheftsValidator : AbstractValidator<IGetCityBikeThefts.In>
{
    public GetCityBikeTheftsValidator()
    {
        RuleFor(@in => @in.CityId).GreaterThan(0);
    }
}