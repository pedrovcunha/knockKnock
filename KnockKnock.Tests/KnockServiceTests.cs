using System;
using knockKnock.API.Services;
using Xunit;
using Xunit.Abstractions;

// Naming Format: MethodName_ExpectedBehavior_StateUnderTest
namespace KnockKnock.Tests
{
    // Saves memory just instantiating the class once.
    public class KnockKnockFixture
    {
        public KnockService KnockService => new KnockService();
    }
    public class KnockServiceTests : IClassFixture<KnockKnockFixture>, IDisposable
    {
        private readonly KnockKnockFixture _knockKnockFixture;
        public KnockServiceTests(ITestOutputHelper testOutputHelper, KnockKnockFixture knockKnockFixture)
        {
            _knockKnockFixture = knockKnockFixture;
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(10, 55)]
        [Trait("Category", "Fibonacci - Recursive")]
        public void SrvFibonacciRecursive_ReturnExpectedValue_WhenIndexIsInlineData(long index, long expected)
        {
            // Arrange
            var knockServices = _knockKnockFixture.KnockService;

            // Act
            var result = knockServices.SvrRecursiveFibonacci(0, 1, 1, index);

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
        public void SrvFibonacci_ReturnExpectedValue_WhenIndexIsInlineData(long index, long expected)
        {
            // Arrange
            var knockService = _knockKnockFixture.KnockService;

            // Act 
            var result = knockService.SvrFibonacci(index);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        [Trait("Category", "Fibonacci")]
        public void SrvFibonacci_ThrowArgumentException_WhenIndexIsNegative()
        {
            // Arrange
            var knockService = _knockKnockFixture.KnockService;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => knockService.SvrFibonacci(long.MinValue));
        }

        [Theory]
        [InlineData("Test01", "10tseT")]
        [InlineData("reverse the word", "esrever eht drow")]
        [Trait("Category", "Reverse Word")]
        public void SvrReverseWord_ReturnExpectedValue_WhenIndexIsInlineData(string input, string expected)
        {
            // Arrange
            var knockServices = _knockKnockFixture.KnockService;

            // Act
            var result = knockServices.SvrReverseWord(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(2, 2, 2, KnockService.TriangleType.Equilateral)]
        [InlineData(2, 2, 3, KnockService.TriangleType.Isosceles)]
        [InlineData(2, 3, 4, KnockService.TriangleType.Scalene)]
        [InlineData(2, 3, 5, KnockService.TriangleType.NotATriangle)]
        [InlineData(2, 3, 6, KnockService.TriangleType.NotATriangle)]
        [Trait("Category", "Triangle Type")]
        public void SrvTriangleType_ReturnExpectedValue_WhenIndexIsInlineData(int sideA, int sideB, int sideC, KnockService.TriangleType expected)
        {
            // Arrange
            var knockServices = _knockKnockFixture.KnockService;

            // Act
            var result = knockServices.SrvTriangleType(sideA, sideB, sideC);

            // Assert
            Assert.Equal(expected, result);
        }

        public void Dispose()
        {
            // TODO: Implement dispose if needed.
        }
    }
}
