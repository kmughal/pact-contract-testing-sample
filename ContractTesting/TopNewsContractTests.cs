using ContractTesting.Common;
using FluentAssertions;
using HackerNewsApi.TopNews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using PactNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using ExecutionContext = Microsoft.Azure.WebJobs.ExecutionContext;

namespace ContractTesting;

public class TopNewsTests
{
    private readonly IPactBuilderV3 _pactBuilder;
    private readonly string _topNewsUri = "/top-news";

    public TopNewsTests()
    {
        var config = new PactConfig
        {
            PactDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            DefaultJsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            },
            LogLevel = PactLogLevel.Debug
        };

        _pactBuilder = Pact.V3("Hackers.Api.TopNews", "HackersApi", config)
            .WithHttpInteractions();
    }


    [Test]
    public async Task ShouldReturnTopNewsIdsForHappyPath()
    {
        var expectedResponse = new TopNewsResponse
        {
            TopPostIds = new List<int> { 37713091, 37708292, 37713418, }
        };

        _pactBuilder
            .UponReceiving("Get Top News Ids")
            .Given("Received request to get top news ids")
            .WithRequest(HttpMethod.Get, _topNewsUri)
            .WillRespond()
            .WithStatus(HttpStatusCode.OK)
            .WithHeader("Content-Type", "application/json; charset=utf-8")
            .WithJsonBody(expectedResponse.TopPostIds);
       

        await _pactBuilder.VerifyAsync(async (ctx) =>
        {
            var requestPath = new Uri($"{ctx.MockServerUri}${_topNewsUri}");
            var mediator = TestUtils.GetMediatorAndPlugDI(requestPath);
            var sut = new TopNews(mediator);

            ExecutionContext executionContext = new();
            Mock<HttpRequest> mockRequest = new();
            mockRequest.Setup(req => req.Method).Returns("GET");
             
            var mockLogger = new Mock<ILogger>();

            var response = await sut.Run(mockRequest.Object, mockLogger.Object, executionContext) as OkObjectResult;
            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(200);
            var responseValue = response!.Value as TopNewsResponse;
            responseValue.Should().NotBeNull();
            responseValue!.TopPostIds.Should().BeEquivalentTo(expectedResponse.TopPostIds);
        });
    }


}



