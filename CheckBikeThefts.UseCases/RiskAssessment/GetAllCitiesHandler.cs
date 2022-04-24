using AutoMapper;
using CheckBikeThefts.Domain;
using CheckBikeThefts.Interfaces;
using CheckBikeThefts.Interfaces.Repositories;
using FluentValidation;

namespace CheckBikeThefts.UseCases.RiskAssessment;

public class GetAllCitiesHandler : UseCaseBaseHandler<IGetAllCities.In, IGetAllCities.Out>, IGetAllCities
{
    private readonly ICityRepository _cityRepository;
    private readonly IValidator<City> _cityValidator;
    private readonly IMapper _mapper;

    public GetAllCitiesHandler(
        ICityRepository cityRepository,
        IValidator<City> cityValidator,
        IMapper mapper,
        IApplicationLogger logger)
        : base(logger)
    {
        _cityRepository = cityRepository;
        _cityValidator = cityValidator;
        _mapper = mapper;
    }

    protected override async Task<Result<IGetAllCities.Out>> OnExecute(IGetAllCities.In input)
    {
        var cities = _mapper.Map<IEnumerable<City>>(await _cityRepository.GetAll())
            // If data storage contains an invalid city, skip it
            .Where(city => _cityValidator.Validate(city).IsValid)
            .ToList();
        var currentlyOperate =
            _mapper.Map<ICollection<IGetAllCities.Out.CityDto>>(cities.Where(city => city.CurrentlyOperate));
        var notCurrentlyOperate =
            _mapper.Map<ICollection<IGetAllCities.Out.CityDto>>(cities.Where(city => !city.CurrentlyOperate));
        return Result<IGetAllCities.Out>.Success(new IGetAllCities.Out(currentlyOperate, notCurrentlyOperate));
    }
}