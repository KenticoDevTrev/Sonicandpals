using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SAP.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using SAP.Models.Interfaces;

namespace SAP.API
{
    public class GetTodaysComics
    {
        public GetTodaysComics(IComicRepository comicRepository)
        {
            ComicRepository = comicRepository;
        }

        public IComicRepository ComicRepository { get; }

        [FunctionName("GetTodaysComics")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, new string[] { "post", "get" }, Route = null)]
            HttpRequest req,
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

                Comics = ComicRepository.GetTodaysComics().ToList();

                var Response = new ComicResponse()
                {
                    Date = (Comics.Count > 0 ? Comics[0].Date : DateTime.Now),
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

 
}
