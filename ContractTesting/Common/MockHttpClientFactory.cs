using System.Net.Http;

namespace ContractTesting.Common;

public class MockHttpClientFactory : IHttpClientFactory
{
    private readonly HttpClient _httpClient;
    public MockHttpClientFactory(HttpClient httpClient) => _httpClient = httpClient;
    public HttpClient CreateClient(string name) => _httpClient;
}
