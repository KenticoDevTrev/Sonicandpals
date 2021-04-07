using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP.API
{
    class Program
    {
        readonly static string _storageConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage") ?? string.Empty;

        static Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureAppConfiguration(configurationBuilder =>
                {
                    configurationBuilder.AddCommandLine(args);
                })
                .ConfigureFunctionsWorker((hostBuilderContext, workerApplicationBuilder) =>
                {
                    workerApplicationBuilder.UseFunctionExecutionMiddleware();
                })
                .ConfigureServices(services =>
                {
                    services.AddHttpClient();

                    //services.AddSingleton<BlobStorageService>();
                    //services.AddSingleton<CloudBlobClient>(CloudStorageAccount.Parse(_storageConnectionString).CreateCloudBlobClient());
                })
                .Build();

            return host.RunAsync();
        }
    }
}
