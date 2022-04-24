using CheckBikeThefts.Domain.Validation;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace CheckBikeThefts.Domain.UnitTests.Validation;

[TestFixture]
public class CityValidatorTests
{
    private City _city;
    private CityValidator _validator;

    [SetUp]
    public void Setup()
    {
        _city = new City
        {
            Id = 1,
            Name = "Name1",
            Country = "Country1",
            CurrentlyOperate = true
        };
        _validator = new CityValidator();
    }

    [Test]
    [TestCase(-1)]
    [TestCase(0)]
    public void Validate_CityIdIsLessThanOrEqualZero_ReturnsValidationError(int cityId)
    {
        // Arrange
        _city.Id = cityId;
        
        // Act
        var validation = _validator.TestValidate(_city);
        
        // Assert
        validation.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    public void Validate_NameIsEmpty_ReturnsValidationError(string name)
    {
        // Arrange
        _city.Name = name;
        
        // Act
        var validation = _validator.TestValidate(_city);
        
        // Assert
        validation.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    public void Validate_CountryIsEmpty_ReturnsValidationError(string country)
    {
        // Arrange
        _city.Country = country;
        
        // Act
        var validation = _validator.TestValidate(_city);
        
        // Assert
        validation.ShouldHaveValidationErrorFor(x => x.Country);
    }
    
    [Test]
    public void Validate_CityIsValid_ReturnsSuccessStatus()
    {
        // Act
        var validation = _validator.TestValidate(_city);
        
        // Assert
        Assert.That(validation.IsValid, Is.True);
    }
}