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
using SAP.Models.Interfaces;
using SAP.Models.SaP;

namespace SAP.API
{
    public class Vote
    {
        public Vote(IComicRepository comicRepository)
        {
            ComicRepository = comicRepository;
        }

        public IComicRepository ComicRepository { get; }

        [FunctionName("Vote")]
        [FixedDelayRetry(5, "00:00:02")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            VoteRequest Request,
            HttpRequest req,
            ILogger log
            )
        {
            string Error = "";
            string Content = "";
            try
            {
                string IP = req.HttpContext.Connection.RemoteIpAddress.ToString();
                var VoteSuccessful = ComicRepository.Vote(Request.EpisodeNumber, Request.EpisodeSubNumber, Request.StarRating, IP);
                return new JsonResult(new VoteResponse()
                {
                    Successful = VoteSuccessful
                });
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

            var ErrorResponse = new VoteResponse()
            {
                Successful = false,
                Error = Error
            };
            return new JsonResult(ErrorResponse);

        }
    }

    public class VoteRequest
    {
        public int EpisodeNumber { get; set; }
        public int? EpisodeSubNumber { get; set; }
        public int StarRating { get; set; }

        public override string ToString()
        {
            return $"{EpisodeNumber}|{EpisodeSubNumber ?? 0}|{StarRating}";
        }
    }
}
