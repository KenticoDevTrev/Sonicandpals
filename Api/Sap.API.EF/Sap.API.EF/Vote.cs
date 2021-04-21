using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using SAP.Models.Interfaces;
using SAP.Models.SaP;
using System.Net;
using Microsoft.Extensions.Primitives;

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
                IPAddress result = null;
                if (req.Headers.TryGetValue("X-Forwarded-For", out StringValues values))
                {
                    var ipn = values.FirstOrDefault().Split(new char[] { ',' }).FirstOrDefault().Split(new char[] { ':' }).FirstOrDefault();
                    IPAddress.TryParse(ipn, out result);
                }
                if (result == null)
                {
                    result = req.HttpContext.Connection.RemoteIpAddress;
                }
                string IP = result?.ToString();


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
