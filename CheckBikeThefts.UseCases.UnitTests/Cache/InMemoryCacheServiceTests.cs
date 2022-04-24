using System;
using CheckBikeThefts.UseCases.Cache;
using NUnit.Framework;

namespace CheckBikeThefts.UseCases.UnitTests.Cache;

[TestFixture]
public class InMemoryCacheServiceTests
{
    private TestableInMemoryCacheService _cache;

    [SetUp]
    public void SetUp()
    {
        _cache = new TestableInMemoryCacheService();
    }

    [Test]
    public void Get_KeyNotExist_ReturnsNull()
    {
        // Act
        var cachedValue = _cache.Get("unknown_key");

        // Assert
        Assert.That(cachedValue, Is.Null);
    }

    [Test]
    public void Get_CacheExpired_ReturnsNull()
    {
        // Arrange
        const string value = "value";
        const string key = "key";
        var utcNow = new DateTime(2020, 10, 10, 12, 12, 00);
        var expiry = TimeSpan.FromMinutes(10);

        _cache.SetUtcNow(utcNow);
        _cache.Set(key, value, expiry);
        _cache.SetUtcNow(utcNow.Add(expiry).AddSeconds(1));

        // Act
        var cachedValue = _cache.Get(key);

        // Assert
        Assert.That(cachedValue, Is.Null);
    }

    [Test]
    public void Get_CacheNotExpired_ReturnsValue()
    {
        // Arrange
        const string value = "value";
        const string key = "key";
        var utcNow = new DateTime(2020, 10, 10, 12, 12, 00);
        var expiry = TimeSpan.FromMinutes(10);

        _cache.SetUtcNow(utcNow);
        _cache.Set(key, value, expiry);

        // Act
        var cachedValue = _cache.Get(key);

        // Assert
        Assert.That(cachedValue, Is.EqualTo(value));
    }

    private class TestableInMemoryCacheService : InMemoryCacheService
    {
        private DateTime _utcNow;

        protected override DateTime UtcNow => _utcNow;

        public void SetUtcNow(DateTime utcNow)
        {
            _utcNow = utcNow;
        }
    }
}