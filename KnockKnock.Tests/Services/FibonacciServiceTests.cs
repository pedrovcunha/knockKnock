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
        [InlineData(-6, -8)]
        [InlineData(-5, 5)]
        [InlineData(-7, 13)]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(10, 55)]
        [InlineData(41, 165580141)]
        [InlineData(53, 53316291173)]
        [InlineData(65, 17167680177565)]
        [InlineData(82, 61305790721611591)]
        [InlineData(91, 4660046610375530309)]
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
        public async System.Threading.Tasks.Task SrvFibonacci_ThrowArgumentException_WhenIndexOverflow()
        {
            // Arrange
            var fibonacciServices = _fibonacciFixture.FibonacciService;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => fibonacciServices.SvrFibonacci(long.MinValue));
            await Assert.ThrowsAsync<ArgumentException>(() => fibonacciServices.SvrFibonacci(93));
        }

        public void Dispose()
        {
            // TODO: Implement dispose if needed.
        }
    }
}
