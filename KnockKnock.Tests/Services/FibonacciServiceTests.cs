using System;
using knockKnock.API.Services;
using Xunit;

namespace KnockKnock.Tests.Services
{
    public class FibonacciFixture
    {
        public FibonacciService FibonacciService => new FibonacciService();
    }
    public class FibonacciServiceTests : IClassFixture<FibonacciFixture>, IDisposable
    {
        private readonly FibonacciFixture _fibonacciFixture;
        public FibonacciServiceTests(FibonacciFixture fibonacciFixture)
        {
            _fibonacciFixture = fibonacciFixture;
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(10, 55)]
        [Trait("Category", "Fibonacci - Recursive")]
        public async void SrvFibonacciRecursive_ReturnExpectedValue_WhenIndexIsInlineData(long index, long expected)
        {
            // Arrange
            var fibonacciServices = _fibonacciFixture.FibonacciService;

            // Act
            var result = await fibonacciServices.SvrRecursiveFibonacci(0, 1, 1, index);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(10, 55)]
        [Trait("Category", "Fibonacci")]
        public async void SrvFibonacci_ReturnExpectedValue_WhenIndexIsInlineData(long index, long expected)
        {
            // Arrange
            var fibonacciServices = _fibonacciFixture.FibonacciService;

            // Act 
            var result = await fibonacciServices.SvrFibonacci(index);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        [Trait("Category", "Fibonacci")]
        public async System.Threading.Tasks.Task SrvFibonacci_ThrowArgumentException_WhenIndexIsNegative()
        {
            // Arrange
            var fibonacciServices = _fibonacciFixture.FibonacciService;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => fibonacciServices.SvrFibonacci(long.MinValue));
        }

        public void Dispose()
        {
            // TODO: Implement dispose if needed.
        }
    }
}
