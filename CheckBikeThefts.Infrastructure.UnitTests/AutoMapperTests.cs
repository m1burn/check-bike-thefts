using AutoMapper;
using CheckBikeThefts.Domain;
using CheckBikeThefts.Interfaces.Repositories;
using NUnit.Framework;

namespace CheckBikeThefts.Infrastructure.UnitTests;

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
    [TestCase(10, "Name10", "Country10", true, 10, "Name10", "Country10", true)]
    [TestCase(null, null, null, null, 0, "", "", false)]
    [TestCase(11, "Name11  ", "  Country11", false, 11, "Name11", "Country11", false)]
    [TestCase(8, "  ", "  ", true, 8, "", "", true)]
    public void ShouldMapCityDtoToCity(int? actualId, string actualName, string actualCountry,
        bool? actualCurrentlyOperate, int expectedId, string expectedName, string expectedCountry,
        bool expectedCurrentlyOperate)
    {
        // Arrange
        var dto = new ICityRepository.CityDto(actualId, actualName, actualCountry, actualCurrentlyOperate);

        // Act
        var city = _mapper.Map<City>(dto);

        // Assert
        Assert.That(city.Id, Is.EqualTo(expectedId));
        Assert.That(city.Name, Is.EqualTo(expectedName));
        Assert.That(city.Country, Is.EqualTo(expectedCountry));
        Assert.That(city.CurrentlyOperate, Is.EqualTo(expectedCurrentlyOperate));
    }
}