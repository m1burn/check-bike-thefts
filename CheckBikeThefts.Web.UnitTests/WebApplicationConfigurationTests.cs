using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace CheckBikeThefts.Web.UnitTests;

[TestFixture]
public class WebApplicationConfigurationTests
{
    private CheckFileExists _checkFileExists;
    private Mock<IConfiguration> _mockConfiguration;

    [SetUp]
    public void SetUp()
    {
        _checkFileExists = _ => true;
        _mockConfiguration = new Mock<IConfiguration>();

        _mockConfiguration.Setup(x => x[It.IsAny<string>()]).Returns("fakeValue");
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    public void BikeIndexBaseUrl_NotConfigured_ThrowsException(string value)
    {
        // Arrange
        _mockConfiguration.Setup(x => x["BikeIndexBaseUrl"]).Returns(value);
        
        // Assert
        Assert.Catch<ConfigurationException>(() => new WebApplicationConfiguration(_mockConfiguration.Object, _checkFileExists));
    }
    
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    public void CityCsvFilePath_NotConfigured_ThrowsException(string value)
    {
        // Arrange
        _mockConfiguration.Setup(x => x["CityCsvFilePath"]).Returns(value);
        
        // Assert
        Assert.Catch<ConfigurationException>(() => new WebApplicationConfiguration(_mockConfiguration.Object, _checkFileExists));
    }
    
    [Test]
    public void CityCsvFilePath_FileNotExists_ThrowsException()
    {
        // Arrange
        _checkFileExists = _ => false;
        
        _mockConfiguration.Setup(x => x["CityCsvFilePath"]).Returns("fakePath");
        
        // Assert
        Assert.Catch<ConfigurationException>(() => new WebApplicationConfiguration(_mockConfiguration.Object, _checkFileExists));
    }
    
    [Test]
    public void Ctr_ConfigurationIsValid_CreatesInstance()
    {
        // Act
        var appConfiguration = new WebApplicationConfiguration(_mockConfiguration.Object, _checkFileExists);
    }
}