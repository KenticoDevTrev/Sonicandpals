using System.Threading.Tasks;
using System.Linq;
using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

using CMS.Core;
using CMS.DataEngine;
using CMS.Membership;
using System.IO;
using System;
using Microsoft.Extensions.DependencyInjection;
using SAP;
using System.Collections.Generic;

[assembly: FunctionsStartup(typeof(MyNamespace.Startup))]
namespace MyNamespace
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string ConnectionString = "";
            builder.Services.AddLogging();
            var _loggerFactory = new LoggerFactory();
            var logger = _loggerFactory.CreateLogger("Startup");
            try
            {
                // Loads kentico libraries into the app domain
                RegisterCmsAssemblies();

                // Loads custom libraries into the app domain
                RegisterCustomAssemblies();

                // Prinit types
                CMSApplication.PreInit();

                // Merge kentico services into the builder services
                Service.MergeDescriptors(builder.Services);

                var Config = builder.GetContext().Configuration;
                string EnvironmentName = builder.GetContext().EnvironmentName;
                
                // Wait for database to be ready since this is a external service
                CMSApplication.WaitForDatabaseAvailable.Value = true;

                if (EnvironmentName.Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                {
                    // Setup connection string (manually or read frOm key/vault, settings file etc.)  This will read from local.settings.json -> ConnectionStrings.CMSConnectionString
                    ConnectionString = Config.GetSection("ConnectionStrings").GetSection("CMSConnectionString").Value;
                }
                else
                {
                    // In Azure Functions, will be grabbing just the CMSConnectionString from the Azure Functio -> Configuration -> Application Settings
                    ConnectionString = Environment.GetEnvironmentVariable("CMSConnectionString", EnvironmentVariableTarget.Process);
                }
                
                // Set connection string
                ConnectionHelper.ConnectionString = ConnectionString;

                // Init database
                CMSApplication.Init();
            } catch(Exception ex)
            {
                logger.LogError(ex, "An error occurred during startup");
            }
        }


        // Kentico libraries aren't loaded into the AppDomain directly. For some reason all referenced assemblies 
        // are outside the working folder of the azure function. We can load them but we need to find the proper location. 
        // This method ensures Kentico assemblies only. Custom assembly can be loaded thru  AssemblyDiscoveryHelper.RegisterAdditionalAssemblies method.
        private static void RegisterCmsAssemblies()
        {
            // In this case we count with fact that assembly file name is identical with assembly name
            var assemblyLocation = Path.GetDirectoryName(Assembly.Load("CMS.Core").Location);
            var cmsAssemblies = Directory.EnumerateFiles(assemblyLocation, "CMS.*.dll");

            var assemblyNames = cmsAssemblies.Select(path => Path.GetFileNameWithoutExtension(path));
            var assemblies = assemblyNames.Select(name => Assembly.Load(name)).ToArray();

            AssemblyDiscoveryHelper.RegisterAdditionalAssemblies(assemblies);
        }

        // Custom libraries aren't loaded into the AppDomain directly. For some reason all referenced assemblies 
        // are outside the working folder of the azure function. We can load them but we need to find the proper location. 
        // This method ensures custom assemblies are also loaded. Custom assembly can be loaded thru  AssemblyDiscoveryHelper.RegisterAdditionalAssemblies method.
        private static void RegisterCustomAssemblies()
        {
            // Can use Wildcards as well
            List<Tuple<string, string>> AssemblyAndFiles = new List<Tuple<string, string>>()
            {
                new Tuple<string, string>("SAP.XperienceLibraries", "SAP.XperienceLibraries.dll"),
                new Tuple<string, string>("SAP.Library", "SAP.Library.dll"),
                new Tuple<string, string>("SAP.Models", "SAP.Models.dll"),
            };

            foreach(Tuple<string, string> AssemblyAndFile in AssemblyAndFiles)
            {
                // In this case we count with fact that assembly file name is identical with assembly name
                var assemblyLocation = Path.GetDirectoryName(Assembly.Load(AssemblyAndFile.Item1).Location);
                var cmsAssemblies = Directory.EnumerateFiles(assemblyLocation, AssemblyAndFile.Item2);

                var assemblyNames = cmsAssemblies.Select(path => Path.GetFileNameWithoutExtension(path));
                var assemblies = assemblyNames.Select(name => Assembly.Load(name)).ToArray();

                AssemblyDiscoveryHelper.RegisterAdditionalAssemblies(assemblies);
            }
            
        }
    }

    
}