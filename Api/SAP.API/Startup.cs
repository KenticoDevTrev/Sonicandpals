using CMS.Core;
using CMS.EventLog;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SAP;
using SAP.API.Implementations;
using CMS;
using Kentico.Web.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using CMS.DataEngine;

[assembly: AssemblyDiscoverable]
[assembly: FunctionsStartup(typeof(MyNamespace.Startup))]
namespace MyNamespace
{
    public class Startup : FunctionsStartup
    {
       

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddKentico();
            Service.InitializeContainer();
            CMSApplication.Init();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddSingleton<IEpisodeInfoProvider>((s) =>
            {
                return new EpisodeInfoProvider();
            });


            

        }

    }
}