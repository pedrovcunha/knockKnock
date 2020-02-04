using System;
using knockKnock.API.Services;
using Xunit;

namespace KnockKnock.Tests.Services
{
    public class ReverseWordFixture
    {
        public ReverseWordService ReverseWordService => new ReverseWordService();
    }
    public class ReverseWordsServiceTests : IClassFixture<ReverseWordFixture>, IDisposable
    {
        private readonly ReverseWordFixture _reverseWordFixture;
        public ReverseWordsServiceTests(ReverseWordFixture reverseWordFixture)
        {
            _reverseWordFixture = reverseWordFixture;
        }

        [Theory]
        [InlineData("Test01", "10tseT")]
        [InlineData("reverse the word", "esrever eht drow")]
        [Trait("Category", "Reverse Word")]
        public async void SvrReverseWord_ReturnExpectedValue_WhenIndexIsInlineData(string input, string expected)
        {
            // Arrange
            var reverseWordService = _reverseWordFixture.ReverseWordService;

            // Act
            var result = await reverseWordService.SvrReverseWord(input);

            // Assert
            Assert.Equal(expected, result);
        }
        public void Dispose()
        {
            // TODO: Implement dispose if needed.
        }
    }
}
