using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sap.API.EF.EntityFramework.Context;
using Sap.API.EF.EntityFramework.Implementations;
using SAP.Models.Interfaces;
using System;

[assembly: FunctionsStartup(typeof(AzureFunctionsTodo.Startup))]

namespace AzureFunctionsTodo
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Declare Entity contexts
            string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");
            builder.Services.AddDbContext<ChapterContext>(
                options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));
            builder.Services.AddDbContext<EpisodeContext>(
                options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));
            builder.Services.AddDbContext<InfoPageContext>(
                options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));

            // Add implementations
            builder.Services.AddTransient<IChapterRepository, ChapterRepository>();
            builder.Services.AddTransient<IComicRepository, ComicRepository>();
            builder.Services.AddTransient<IPageRepository, PageRepository>();
            builder.Services.AddTransient<IKeepAliveService, KeepAliveService>();
        }
    }
}