using System;
using System.Threading.Tasks;
using CheckBikeThefts.Interfaces;
using Moq;
using NUnit.Framework;

namespace CheckBikeThefts.UseCases.UnitTests;

[TestFixture]
public class UseCaseBaseHandlerTests
{
    private Mock<IApplicationLogger> _mockLogger;
    private UseCaseBaseHandler<object, object> _handler;

    [SetUp]
    public void SetUp()
    {
        _mockLogger = new Mock<IApplicationLogger>();
        _handler = new TestableUseCaseBaseHandler(_mockLogger.Object);
    }

    [Test]
    [TestCase("input", typeof(NotImplementedException))]
    [TestCase(null, typeof(ArgumentNullException))]
    public void Execute_ThrowsException_LogsExceptionAndReThrowsIt(object input, Type exceptionType)
    {
        // Assert
        Assert.CatchAsync(exceptionType, () => _handler.Execute(input));
        
        _mockLogger.Verify(x => x.Error(It.Is<Exception>(ex => ex.GetType() == exceptionType)), Times.Once);
    }

    private class TestableUseCaseBaseHandler : UseCaseBaseHandler<object, object>
    {
        public TestableUseCaseBaseHandler(IApplicationLogger logger)
            : base(logger)
        {
        }

        protected override Task<Result<object>> OnExecute(object input)
        {
            throw new NotImplementedException();
        }
    }
}