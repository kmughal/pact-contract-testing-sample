using HackerNewsApi.Ioc;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContractTesting.Common;

public static class TestUtils
{
    public static HttpClient GetMockHttpClient<T>(HttpStatusCode statusCode, T mockedResponse, bool throwException = false)
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var responseMessage = mockedResponse is null ? new HttpResponseMessage { StatusCode = statusCode } :
            new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(mockedResponse), Encoding.UTF8, "application/json"),
                StatusCode = statusCode
            };

        if (throwException)
        {
            handlerMock.Protected()
          .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
          .Throws(new Exception("Something went wrong"));
        }
        else
        {
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);
        }

        HttpClient httpClient = new(handlerMock.Object);
        return httpClient;
    }

    public static IMediator GetMediatorAndPlugDI(Uri requestUrlForHttpClient)
    {
        var httpClientMock = new HttpClient() {  BaseAddress = requestUrlForHttpClient };
        var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var localSettingPath = Path.Combine(basePath!, "local.settings.json");
        var container = new ServiceCollection();

        var configuration = new ConfigurationBuilder()
        .AddJsonFile(localSettingPath)
        .Build();

        container.ConfigureDependencyInjection(configuration);
        container.AddSingleton<IHttpClientFactory>(new MockHttpClientFactory(httpClientMock));

        var serviceProvider = container.BuildServiceProvider();
        var mediator = serviceProvider.GetService<IMediator>();
        return mediator!;
    }
}

