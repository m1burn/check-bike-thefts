using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CheckBikeThefts.Domain;
using CheckBikeThefts.Interfaces;
using CheckBikeThefts.Interfaces.Repositories;
using CheckBikeThefts.UseCases.RiskAssessment;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;

namespace CheckBikeThefts.UseCases.UnitTests.RiskAssessment;

[TestFixture]
public class GetAllCitiesHandlerTests
{
    private Mock<ICityRepository> _mockCityRepository;
    private Mock<IValidator<City>> _mockCityValidator;
    private IMapper _mapper;
    private Mock<IApplicationLogger> _mockLogger;
    private GetAllCitiesHandler _handler;

    private readonly ICollection<ICityRepository.CityDto> _fakeCities = new List<ICityRepository.CityDto>
    {
        new(1, "City1", "County1", true),
        new(2, "City2", "County2", true),
        new(3, "City3", "County3", false),
        new(4, "City4", "County4", false),
    };

    [SetUp]
    public void SetUp()
    {
        _mockCityRepository = new Mock<ICityRepository>();
        _mockCityValidator = new Mock<IValidator<City>>();
        _mockLogger = new Mock<IApplicationLogger>();

        var configuration = new MapperConfiguration(cfg =>
        {
            AutoMapperConfigurator.Configure(cfg);
            Infrastructure.AutoMapperConfigurator.Configure(cfg);
        });
        configuration.AssertConfigurationIsValid();
        _mapper = new Mapper(configuration);

        _mockCityRepository.Setup(x => x.GetAll()).Returns(Task.FromResult(_fakeCities));
        _mockCityValidator.Setup(x => x.Validate(It.IsAny<City>())).Returns(new ValidationResult());

        _handler = new GetAllCitiesHandler(_mockCityRepository.Object, _mockCityValidator.Object, _mapper,
            _mockLogger.Object);
    }

    [Test]
    public async Task Execute_SomeCitiesAreInvalid_SkipsInvalidCities()
    {
        // Arrange
        var invalidCity = _fakeCities.ElementAt(0);

        _mockCityValidator.Setup(x => x.Validate(It.Is<City>(city => city.Id == invalidCity.Id)))
            .Returns(new ValidationResult(new[] {new ValidationFailure(string.Empty, "Error")}));

        // Act
        var result = await _handler.Execute(new IGetAllCities.In());
        var resultCityIds = result.Value.CurrentlyOperate.Concat(result.Value.NotCurrentlyOperate)
            .Select(city => city.Id)
            .ToList();

        // Assert
        Assert.That(result.Validation.IsValid, Is.True);
        Assert.That(resultCityIds.Contains(invalidCity.Id.Value), Is.False);
    }

    [Test]
    public async Task Execute_CitiesAreValid_SplitsIntoTwoListAndReturns()
    {
        // Arrange
        var expectedCurrentOperateCityIds =
            _fakeCities.Where(city => city.CurrentlyOperate == true).Select(city => city.Id).ToList();
        var expectedNotCurrentOperateCityIds =
            _fakeCities.Where(city => city.CurrentlyOperate == false).Select(city => city.Id).ToList();

        // Act
        var result = await _handler.Execute(new IGetAllCities.In());
        var actualCurrentOperateCityIds = result.Value.CurrentlyOperate.Select(city => city.Id).ToList();
        var actualNotCurrentOperateCityIds = result.Value.NotCurrentlyOperate.Select(city => city.Id).ToList();

        // Assert
        Assert.That(result.Validation.IsValid, Is.True);
        CollectionAssert.AreEquivalent(expectedCurrentOperateCityIds, actualCurrentOperateCityIds);
        CollectionAssert.AreEquivalent(expectedNotCurrentOperateCityIds, actualNotCurrentOperateCityIds);
    }
}