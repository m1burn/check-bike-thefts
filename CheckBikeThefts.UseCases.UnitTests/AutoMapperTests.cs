using AutoMapper;
using CheckBikeThefts.Domain;
using CheckBikeThefts.UseCases.RiskAssessment;
using NUnit.Framework;

namespace CheckBikeThefts.UseCases.UnitTests;

[TestFixture]
public class AutoMapperTests
{
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        var configuration = new MapperConfiguration(AutoMapperConfigurator.Configure);
        configuration.AssertConfigurationIsValid();

        _mapper = new Mapper(configuration);
    }

    [Test]
    [TestCase(10, "Name10", "Country10")]
    [TestCase(20, "Name20", "Country20")]
    [TestCase(30, "Name30", "Country30")]
    public void ShouldMapCityToCityDto(int id, string name, string country)
    {
        // Arrange
        var city = new City
        {
            Id = id,
            Name = name,
            Country = country,
            CurrentlyOperate = true
        };
        
        // Act
        var dto = _mapper.Map<IGetAllCities.Out.CityDto>(city);
        
        // Assert
        Assert.That(city.Id, Is.EqualTo(dto.Id));
        Assert.That(city.Name, Is.EqualTo(dto.Name));
        Assert.That(city.Country, Is.EqualTo(dto.Country));
    }
}