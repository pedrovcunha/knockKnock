using System;
using System.Net;
using System.Net.Http;
using knockKnock.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace KnockKnock.Tests
{
    public class KnockIntegrationTests : IDisposable
    {
        private readonly HttpClient _client;

        public KnockIntegrationTests()
        {
            TestServer server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Fact]
        [Trait("Category", "Integration-Fibonacci")]
        public async void GetFibonacciNumber_ReturnStatus200_WhenIndexIsPositive()
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/v1/Fibonacci?n=8");
            
            //Act
            var response = await _client.SendAsync(request);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        [Trait("Category", "Integration-Fibonacci")]
        public async void GetFibonacciNumber_ReturnStatus422_WhenIndexIsNegative()
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/v1/Fibonacci?n=-8");

            //Act
            var response = await _client.SendAsync(request);

            //Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        [Fact]
        [Trait("Category", "Integration-Fibonacci")]
        public async void GetFibonacciNumber_ReturnStatus406_WhenAcceptHeaderIsNotJson()
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/v1/Fibonacci?n=-8");
            _client.DefaultRequestHeaders.Add("Accept", "application/xml");

            //Act
            var response = await _client.SendAsync(request);

            //Assert
            Assert.Equal(HttpStatusCode.NotAcceptable, response.StatusCode);
        }

        [Fact]
        [Trait("Category", "Integration-Fibonacci")]
        public async void GetFibonacciNumber_ReturnStatus400_WhenIndexIsInvalidType()
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/v1/Fibonacci?n=test");
            _client.DefaultRequestHeaders.Add("Accept", "application/json");

            //Act
            var response = await _client.SendAsync(request);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        [Trait("Category", "Integration-ReverseWords")]
        public async void GetReverseWords_ReturnStatus200_WhenSentenceIsString()
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/v1/ReverseWords?sentence=test");

            //Act
            var response = await _client.SendAsync(request);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        [Trait("Category", "Integration-ReverseWords")]
        public async void GetReverseWords_ReturnStatus422_WhenSentenceIsNull()
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/v1/ReverseWords");

            //Act
            var response = await _client.SendAsync(request);

            //Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        [Fact]
        [Trait("Category", "Integration-ReverseWords")]
        public async void GetReverseWords_ReturnStatus406_WhenAcceptHeaderIsNotJson()
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/v1/ReverseWords?sentence=test");
            _client.DefaultRequestHeaders.Add("Accept", "application/xml");

            //Act
            var response = await _client.SendAsync(request);

            //Assert
            Assert.Equal(HttpStatusCode.NotAcceptable, response.StatusCode);
        }

        [Fact]
        [Trait("Category", "Integration-ReverseWords")]
        public async void GetReverseWords_ReturnStatus400_WhenSentenceIsEmpty()
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/v1/ReverseWords?sentence=");
            _client.DefaultRequestHeaders.Add("Accept", "application/json");

            //Act
            var response = await _client.SendAsync(request);

            //Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        [Fact]
        [Trait("Category", "Integration-TriangleType")]
        public async void GetTriangleType_ReturnStatus200_WhenAllParametersAreSend()
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/v1/TriangleType?a=2&b=2&c=2");

            //Act
            var response = await _client.SendAsync(request);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        [Trait("Category", "Integration-TriangleType")]
        public async void GetTriangleType_ReturnStatus400_WhenParameterIsMissing()
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/v1/TriangleType?a=test");

            //Act
            var response = await _client.SendAsync(request);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        [Trait("Category", "Integration-TriangleType")]
        public async void GetTriangleType_ReturnStatus400_WhenParameterWrongType()
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/v1/TriangleType?a=test&b=8&c=8");

            //Act
            var response = await _client.SendAsync(request);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


        public void Dispose()
        {
            _client.Dispose();
        }
        
    }
}
