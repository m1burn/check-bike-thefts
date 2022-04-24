using CheckBikeThefts.UseCases.RiskAssessment;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace CheckBikeThefts.UseCases.UnitTests.RiskAssessment;

[TestFixture]
public class GetCityBikeTheftsValidatorTests
{
    private IGetCityBikeThefts.In _input;
    private GetCityBikeTheftsValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _input = new IGetCityBikeThefts.In(1, false);
        _validator = new GetCityBikeTheftsValidator();
    }

    [Test]
    [TestCase(-1)]
    [TestCase(0)]
    public void Validate_CityIdIsLessThanOrEqualZero_ReturnsValidationError(int cityId)
    {
        // Arrange
        _input = new IGetCityBikeThefts.In(cityId, false);

        // Act
        var validation = _validator.TestValidate(_input);

        // Assert
        validation.ShouldHaveValidationErrorFor(x => x.CityId);
    }
    
    [Test]
    public void Validate_CityIsValid_ReturnsSuccessStatus()
    {
        // Act
        var validation = _validator.TestValidate(_input);
        
        // Assert
        Assert.That(validation.IsValid, Is.True);
    }
}