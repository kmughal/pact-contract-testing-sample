using MediatR;
using System.Collections.Generic;

namespace HackerNewsApi.TopNews;

public class TopNewsResponse
{
    public List<int> TopPostIds { get; init; }
}


public class GetTopNewsRequest : IRequest<TopNewsResponse>
{
     
}