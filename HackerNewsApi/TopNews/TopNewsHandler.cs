using HackerNewsApi.Common;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNewsApi.TopNews;


public class TopNewsHandler : IRequestHandler<GetTopNewsRequest, TopNewsResponse>
{
    private readonly HttpClient _client;
    private readonly Settings _settings;

    public TopNewsHandler(IHttpClientFactory httpClientFactory, IOptions<Settings> options)
    {
        _client = httpClientFactory.CreateClient();
        _settings = options.Value;
    }


    public async Task<TopNewsResponse> Handle(GetTopNewsRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync(_settings.TopNewsUri, cancellationToken);
        response.EnsureSuccessStatusCode();
        var contents = await response.Content.ReadAsStringAsync(cancellationToken);
        var ids = JsonConvert.DeserializeObject<List<int>>(contents);
        return new TopNewsResponse { TopPostIds = ids };
    }
}

