using System;
using knockKnock.API.Services;
using Xunit;
using knockKnock.API.Models;

namespace KnockKnock.Tests.Services
{
    public class TriangleTypeFixture
    {
        public TriangleTypeService TriangleTypeService => new TriangleTypeService();
    }
    public class TriangleTypeServiceTests : IClassFixture<TriangleTypeFixture>, IDisposable
    {
        private readonly TriangleTypeFixture _triangleTypeFixture;
        public TriangleTypeServiceTests(TriangleTypeFixture triangleTypeFixture)
        {
            _triangleTypeFixture = triangleTypeFixture;
        }

        [Theory]
        [InlineData(2, 2, 2, Triangle.TriangleType.Equilateral)]
        [InlineData(2, 2, 3, Triangle.TriangleType.Isosceles)]
        [InlineData(2, 3, 4, Triangle.TriangleType.Scalene)]
        [InlineData(2, 3, 5, Triangle.TriangleType.NotATriangle)]
        [InlineData(2, 3, 6, Triangle.TriangleType.NotATriangle)]
        [InlineData(-288779084, -288779084, -288779084, Triangle.TriangleType.NotATriangle)]
        [InlineData(288779084, 288779084, 288779084, Triangle.TriangleType.Equilateral)]
        [Trait("Category", "Triangle Type")]
        public async void SrvTriangleType_ReturnExpectedValue_WhenIndexIsInlineData(int sideA, int sideB, int sideC, Triangle.TriangleType expected)
        {
            // Arrange
            var triangleTypeServices = _triangleTypeFixture.TriangleTypeService;

            // Act
            var result = await triangleTypeServices.SrvTriangleType(sideA, sideB, sideC);

            // Assert
            Assert.Equal(expected, result);
        }

        public void Dispose()
        {
            // TODO: Implement dispose if needed.
        }
    }
}
