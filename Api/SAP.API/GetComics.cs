using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SAP.Models;
using System.Collections.Generic;
using System.Linq;

namespace SAP.API
{
    public class GetComics
    {
        
        public GetComics(IEpisodeInfoProvider episodeInfoProvider)
        {
            EpisodeInfoProvider = episodeInfoProvider;
        }

        public IEpisodeInfoProvider EpisodeInfoProvider { get; }

        [FunctionName("GetComics")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log
            )
        {


            /*var SampleComic = new ComicResponse()
            {
                Date = DateTime.Now,
                Comics = new List<Comic>()
                {
                    new Comic()
                    {
                        Date = DateTime.Now,
                        Chapter = "Test",
                        Commentary = "Test Commentary",
                        EpisodeNumber = 101,
                        ImageUrl = "~/SaP/media/Episodes/episode2.PNG",
                        Title = "Just a test"
                    }
                }
            };*/
            EpisodeInfo Episode = null;
            try
            {
                Episode = EpisodeInfoProvider.Get().WhereEquals("EpisodeNumber", 101).FirstOrDefault();
            } catch(Exception ex)
            {
                string debug = "";
            }
            var SampleComic = new ComicResponse()
            {
                Date = DateTime.Now,
                Comics = new List<Comic>()
                {
                    new Comic()
                    {
                        Date = Episode.EpisodeDate,
                        Chapter = Episode.EpisodeChapterID.ToString(),
                        Commentary = Episode.EpisodeCommentary,
                        EpisodeNumber = Episode.EpisodeNumber,
                        EpisodeSubNumber = (Episode.GetValue("EpisodeSubNumber") == null ? (int?)null : Episode.GetIntegerValue("EpisodeSubNumber", 0)),
                        ImageUrl = Episode.EpisodeFileUrl,
                        Title = Episode.EpisodeTitle
                    }
                }
            };
            return new JsonResult(SampleComic);
        }
    }
}
