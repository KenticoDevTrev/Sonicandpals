using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SAP.Models;
using System.Collections.Generic;
using System.Linq;
using CMS.Core;
using System.Net.Http;
using CMS.Helpers;
using System.IO;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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
        [FixedDelayRetry(5, "00:00:02")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            GetComicsRequest Request,
            ILogger log
            )
        {
            string Error = "";
            string Content = "";
           List <Comic> Comics = null;
            try
            {
                // For error testing
                //Content = await new StreamReader(req.Body).ReadToEndAsync();
                //GetComicsRequest Request = JsonConvert.DeserializeObject<GetComicsRequest>(Content);

                Comics = CacheHelper.Cache(cs =>
                {
                    bool ByEpisode = false;
                    var EpisodeQuery = EpisodeInfoProvider.Get()
                        .Source(x => x.Join<ChapterInfo>(nameof(EpisodeInfo.EpisodeChapterID), nameof(ChapterInfo.ChapterID)))
                        .OrderBy(nameof(EpisodeInfo.EpisodeNumber), nameof(EpisodeInfo.EpisodeSubNumber));
                    if (Request.EpisodeNumber > 0)
                    {
                        ByEpisode = true;
                        EpisodeQuery.WhereGreaterOrEquals(nameof(EpisodeInfo.EpisodeNumber), Request.EpisodeNumber);
                    }
                    else if (Request.Date != DateTimeHelper.ZERO_TIME)
                    {
                        ByEpisode = false;
                        EpisodeQuery.WhereGreaterOrEquals(nameof(EpisodeInfo.EpisodeDate), Request.Date.Date);
                    }
                    List<string> Columns = new List<string>()
                {
                    nameof(EpisodeInfo.EpisodeTitle),
                    nameof(EpisodeInfo.EpisodeNumber),
                    nameof(EpisodeInfo.EpisodeSubNumber),
                    nameof(EpisodeInfo.EpisodeIsAnimation),
                    nameof(EpisodeInfo.EpisodeFileUrl),
                    nameof(ChapterInfo.ChapterTitle)
                };
                    if (Request.IncludeCommentary)
                    {
                        Columns.Add(nameof(EpisodeInfo.EpisodeCommentary));
                    }
                    // Daily or Weekly limit
                    if (Request.Type.Equals("weekly"))
                    {
                        if (ByEpisode)
                        {
                            EpisodeQuery.WhereLessThan(nameof(EpisodeInfo.EpisodeNumber), Request.EpisodeNumber + 7);
                        }
                        else
                        {
                            EpisodeQuery.WhereLessThan(nameof(EpisodeInfo.EpisodeDate), Request.Date.AddDays(7).Date);
                        }
                    }
                    else
                    {
                        if (ByEpisode)
                        {
                            EpisodeQuery.WhereLessThan(nameof(EpisodeInfo.EpisodeNumber), Request.EpisodeNumber + 1);
                        }
                        else
                        {
                            EpisodeQuery.WhereLessThan(nameof(EpisodeInfo.EpisodeDate), Request.Date.AddDays(1).Date);
                        }
                    }

                    if (cs.Cached)
                    {
                        cs.CacheDependency = CacheHelper.GetCacheDependency(new string[]
                        {
                            "CustomDependencyThatYouHaveToManuallyTrigger"
                        });
                    }

                    var episodes = EpisodeQuery
                    .GetEnumerableTypedResult();

                    return episodes.Select(Episode => new Comic()
                    {
                        Date = Episode.EpisodeDate,
                        Chapter = Episode.EpisodeChapterID.ToString(),
                        Commentary = Episode.EpisodeCommentary,
                        EpisodeNumber = Episode.EpisodeNumber,
                        EpisodeSubNumber = (Episode.GetValue("EpisodeSubNumber") == null ? (int?)null : Episode.GetIntegerValue("EpisodeSubNumber", 0)),
                        ImageUrl = Episode.EpisodeFileUrl,
                        Title = Episode.EpisodeTitle,
                        IsAnimated = Episode.EpisodeIsAnimation
                    }).ToList();
                }, new CacheSettings(30, "GetComics|" + Request.ToString()));

                var Response = new ComicResponse()
                {
                    Date = (Comics.Count > 0 ? Comics[0].Date : Request.Date != DateTimeHelper.ZERO_TIME ? Request.Date : DateTime.Now),
                    Comics = Comics
                };
                return new JsonResult(Response);
            }
            catch (UnsupportedMediaTypeException ex)
            {
                log.LogError(ex, "Unsupported media type returned");
                Error = "Unsupported Media Type: "+ex.Message+"|"+ex.StackTrace;
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                Error = "Error.  Content: " + Content + ", " + ex.Message + "|" + ex.StackTrace;
            }

            var ErrorResponse = new ComicResponse()
            {
                Date = DateTime.Now,
                Comics = Comics,
                Error = Error
            };
            return new JsonResult(ErrorResponse);

        }
    }

    public class GetComicsRequest
    {
        public string Type { get; set; } = "daily"; // daily, weekly
        public bool IncludeCommentary { get; set; } = true;
        public int EpisodeNumber { get; set; } = 0;
        public DateTime Date { get; set; } = DateTimeHelper.ZERO_TIME;

        public override string ToString()
        {
            return $"{Type}|{IncludeCommentary}|{EpisodeNumber}|{Date}";
        }
    }
}
