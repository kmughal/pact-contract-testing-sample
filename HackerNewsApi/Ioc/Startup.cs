using HackerNewsApi.Common;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;

[assembly: FunctionsStartup(typeof(HackerNewsApi.Ioc.Startup))]
namespace HackerNewsApi.Ioc;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var localSettingPath = Path.Combine(basePath, "../local.settings.json");


        var configuration = new ConfigurationBuilder()
                        .AddJsonFile(localSettingPath)
                        .Build();

        builder.Services.ConfigureDependencyInjection(configuration);
    }
}