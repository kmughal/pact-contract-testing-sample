using HackerNewsApi.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNewsApi.Ioc;

public static class ServiceConfigureExtension
{
    public static void ConfigureDependencyInjection(this IServiceCollection services,IConfiguration configuration)
    {

        var settings = configuration.GetSection("Settings").Get<Settings>();
       

        services.AddOptions<Settings>().Configure(options =>
        {
            configuration.GetSection("Settings").Bind(options);
        });


        services.AddHttpClient();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Startup>());
    }
}
