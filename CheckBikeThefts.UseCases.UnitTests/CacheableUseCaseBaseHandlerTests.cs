using System;
using System.Threading.Tasks;
using CheckBikeThefts.Interfaces;
using CheckBikeThefts.UseCases.Cache;
using CheckBikeThefts.UseCases.RiskAssessment;
using Moq;
using NUnit.Framework;

namespace CheckBikeThefts.UseCases.UnitTests;

[TestFixture]
public class CacheableUseCaseBaseHandlerTests
{
    private Mock<ICacheService> _mockCacheService;
    private Mock<IApplicationLogger> _mockLogger;
    private TestableCacheableUseCaseBaseHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mockCacheService = new Mock<ICacheService>();
        _mockLogger = new Mock<IApplicationLogger>();
        _handler = new TestableCacheableUseCaseBaseHandler(_mockCacheService.Object, _mockLogger.Object);
    }

    [Test]
    public async Task Execute_CacheIsEmpty_ExecutesHandlerAndCachesResult()
    {
        // Arrange
        var result = await _handler.Execute(new CacheableUseCaseInput(false));

        // Assert
        _mockCacheService.Verify(x => x.Set(_handler.CacheKey, result.Value, _handler.CacheExpiry), Times.Once);

        Assert.That(_handler.Executed, Is.EqualTo(1));
    }

    [Test]
    public async Task Execute_ResultIsCached_ReturnsCachedResult()
    {
        // Arrange
        var fakeResult = new object();

        _mockCacheService.Setup(x => x.Get(_handler.CacheKey)).Returns(fakeResult);

        // Arrange
        await _handler.Execute(new CacheableUseCaseInput(false));

        // Assert
        _mockCacheService.Verify(x => x.Set(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<TimeSpan>()), Times.Never);

        Assert.That(_handler.Executed, Is.Zero);
    }
    
    [Test]
    public async Task Execute_ResultIsCachedButForceUpdateRequested_ExecutesHandlerAndCachesResult()
    {
        // Arrange
        var fakeResult = new object();

        _mockCacheService.Setup(x => x.Get(_handler.CacheKey)).Returns(fakeResult);

        // Arrange
        var result = await _handler.Execute(new CacheableUseCaseInput(true));

        // Assert
        _mockCacheService.Verify(x => x.Set(_handler.CacheKey, result.Value, _handler.CacheExpiry), Times.Once);

        Assert.That(_handler.Executed, Is.EqualTo(1));
    }

    private class TestableCacheableUseCaseBaseHandler : CacheableUseCaseBaseHandler<CacheableUseCaseInput, object>
    {
        public int Executed { get; set; }

        public string CacheKey => "key";

        public TestableCacheableUseCaseBaseHandler(ICacheService cacheService, IApplicationLogger logger)
            : base(cacheService, logger)
        {
        }

        protected override Task<Result<object>> OnExecute(CacheableUseCaseInput input)
        {
            Executed++;
            return Task.FromResult(Result<object>.Success(new object()));
        }

        protected override string GetCacheKey(CacheableUseCaseInput input) => CacheKey;
    }
}