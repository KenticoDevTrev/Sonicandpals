using CMS.Core;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using CMS;
using Microsoft.Extensions.Configuration;
using CMS.DataEngine;

[assembly: AssemblyDiscoverable]
[assembly: FunctionsStartup(typeof(MyNamespace.Startup))]
namespace MyNamespace
{
    public class Startup : FunctionsStartup
    {

        public override void Configure(IFunctionsHostBuilder builder)
        {
            // builder.GetContext().Configuration uses the local.settings.json and resolves the connection string properly.
            Service.Use<IConfiguration>(() => builder.GetContext().Configuration);
            CMSApplication.Init();
        }
    }
}