using CheckBikeThefts.Interfaces;
using CheckBikeThefts.Interfaces.Repositories;
using CheckBikeThefts.UseCases.Cache;
using FluentValidation;
using FluentValidation.Results;

namespace CheckBikeThefts.UseCases.RiskAssessment;

public class GetCityBikeTheftsHandler :
    CacheableUseCaseBaseHandler<IGetCityBikeThefts.In, IGetCityBikeThefts.Out>,
    IGetCityBikeThefts
{
    private readonly ICityRepository _cityRepository;
    private readonly IStolenBikeRepository _stolenBikeRepository;
    private readonly IValidator<IGetCityBikeThefts.In> _inputValidator;

    public GetCityBikeTheftsHandler(
        ICityRepository cityRepository,
        IStolenBikeRepository stolenBikeRepository,
        IValidator<IGetCityBikeThefts.In> inputValidator,
        ICacheService cacheService,
        IApplicationLogger logger)
        : base(cacheService, logger)
    {
        _cityRepository = cityRepository;
        _stolenBikeRepository = stolenBikeRepository;
        _inputValidator = inputValidator;

        CacheExpiry = TimeSpan.FromHours(10);
    }

    protected override async Task<Result<IGetCityBikeThefts.Out>> OnExecute(IGetCityBikeThefts.In input)
    {
        var inputValidation = _inputValidator.Validate(input);
        if (!inputValidation.IsValid)
        {
            return Result<IGetCityBikeThefts.Out>.Fail(inputValidation);
        }

        var city = await _cityRepository.GetById(input.CityId);
        if (city?.Name != null)
        {
            var stolenBikes = await _stolenBikeRepository.GetStolenBikes(city.Name);
            return Result<IGetCityBikeThefts.Out>.Success(new IGetCityBikeThefts.Out(input.CityId, stolenBikes));
        }

        return Result<IGetCityBikeThefts.Out>.Fail(new ValidationResult(
            new[] {new ValidationFailure(nameof(input.CityId), "City has not been found")}));
    }

    protected override string GetCacheKey(IGetCityBikeThefts.In input) => input.CityId.ToString();
}