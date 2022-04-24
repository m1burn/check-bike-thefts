using AutoMapper;
using CheckBikeThefts.Domain;
using CheckBikeThefts.Interfaces.Repositories;
using CheckBikeThefts.UseCases.RiskAssessment;
using CheckBikeThefts.Web.Models.Home;
using CheckBikeThefts.Web.Models.StolenBike;
using NUnit.Framework;

namespace CheckBikeThefts.Web.UnitTests;

[TestFixture]
public class AutoMapperTests
{
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        var configuration = AutoMapperConfigurator.Configure();
        configuration.AssertConfigurationIsValid();

        _mapper = new Mapper(configuration);
    }

    [Test]
    public void ShouldMapCityDtoToIndexResponseModel()
    {
        // Arrange
        var dto = new IGetAllCities.Out.CityDto(10, "Name10", "Country10");

        // Act
        var model = _mapper.Map<IndexResponseModel.City>(dto);

        // Assert
        Assert.That(model.Id, Is.EqualTo(dto.Id));
        Assert.That(model.Name, Is.EqualTo(dto.Name));
        Assert.That(model.Country, Is.EqualTo(dto.Country));
    }
    
    [Test]
    public void ShouldMapCityBikeTheftsToSearchResponseModel()
    {
        // Arrange
        var @out = new IGetCityBikeThefts.Out(10, 111);

        // Act
        var model = _mapper.Map<SearchResponseModel.City>(@out);

        // Assert
        Assert.That(model.CityId, Is.EqualTo(@out.CityId));
        Assert.That(model.StolenBikes, Is.EqualTo(@out.StolenBikes));
    }
}