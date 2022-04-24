using System.Threading.Tasks;
using CheckBikeThefts.Interfaces;
using CheckBikeThefts.Interfaces.Repositories;
using CheckBikeThefts.UseCases.Cache;
using CheckBikeThefts.UseCases.RiskAssessment;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;

namespace CheckBikeThefts.UseCases.UnitTests.RiskAssessment;

[TestFixture]
public class GetCityBikeTheftsHandlerTests
{
    private Mock<ICityRepository> _mockCityRepository;
    private Mock<IStolenBikeRepository> _mockStolenBikeRepository;
    private Mock<IValidator<IGetCityBikeThefts.In>> _mockInputValidator;
    private Mock<ICacheService> _mockCacheService;
    private Mock<IApplicationLogger> _mockLogger;
    private GetCityBikeTheftsHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mockCityRepository = new Mock<ICityRepository>();
        _mockStolenBikeRepository = new Mock<IStolenBikeRepository>();
        _mockInputValidator = new Mock<IValidator<IGetCityBikeThefts.In>>();
        _mockCacheService = new Mock<ICacheService>();
        _mockLogger = new Mock<IApplicationLogger>();

        _handler = new GetCityBikeTheftsHandler(
            _mockCityRepository.Object,
            _mockStolenBikeRepository.Object,
            _mockInputValidator.Object,
            _mockCacheService.Object,
            _mockLogger.Object);
    }

    [Test]
    public async Task Execute_InputIsInvalid_ReturnsValidationError()
    {
        // Arrange
        var input = new IGetCityBikeThefts.In(1, false);
        var validationError = new ValidationResult(new[] {new ValidationFailure(nameof(input.CityId), "Error")});

        _mockInputValidator.Setup(x => x.Validate(input))
            .Returns(validationError);

        // Act
        var result = await _handler.Execute(input);

        // Assert
        Assert.That(result.Validation, Is.EqualTo(validationError));
    }
    
    [Test]
    public async Task Execute_InputIsValid_ReturnsNumberOfStolenBikes()
    {
        // Arrange
        var fakeCity = new ICityRepository.CityDto(1, "Name1", "Country1", true);
        var input = new IGetCityBikeThefts.In(1, false);
        var fakeStolenBikesNumber = 10;
        
        _mockInputValidator.Setup(x => x.Validate(input)).Returns(new ValidationResult());
        _mockCityRepository.Setup(x => x.GetById(input.CityId)).Returns(Task.FromResult(fakeCity));
        _mockStolenBikeRepository.Setup(x => x.GetStolenBikes(fakeCity.Name))
            .Returns(Task.FromResult(fakeStolenBikesNumber));

        // Act
        var result = await _handler.Execute(input);

        // Assert
        Assert.That(result.Validation.IsValid, Is.True);
        Assert.That(result.Value.CityId, Is.EqualTo(input.CityId));
        Assert.That(result.Value.StolenBikes, Is.EqualTo(fakeStolenBikesNumber));
    }
}