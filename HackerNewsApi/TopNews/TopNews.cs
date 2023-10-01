using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace HackerNewsApi.TopNews;

public class TopNews
{
    private readonly IMediator _mediator;

    public TopNews(IMediator mediator)
    {
        _mediator = mediator;
    }

    [FunctionName("TopNews")]
    public   async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "top-news")] 
        HttpRequest req,
        ILogger log,
        ExecutionContext context)
    {
        log.LogInformation($"Executing {context.FunctionName} with Id:{context.InvocationId}");
        try
        {
            var response = await _mediator.Send(new GetTopNewsRequest());
            log.LogInformation($"Executioin completed {context.FunctionName} with Id:{context.InvocationId}");
            return new OkObjectResult(response);
        }
        catch(Exception ex)
        {
            log.LogInformation($"Execution Failed {context.FunctionName} with Id:{context.InvocationId}");
            log.LogInformation($"Something went wrong due to {ex}");
            return new InternalServerErrorResult();
        }
    }
}
