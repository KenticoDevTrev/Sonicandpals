using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using SAP.Models.Interfaces;
using SAP.Models.SaP;
using Microsoft.AspNetCore.Http;

namespace SAP.API
{
    public class GetChapters
    {
        public GetChapters(IChapterRepository chapterRepository)
        {
            ChapterRepository = chapterRepository;
        }

        public IChapterRepository ChapterRepository { get; }

        [FunctionName("GetChapters")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            HttpRequest req,
            ILogger log
            )
        {
            string Error = "";
            string Content = "";
            try
            {
                var ChapterItems = ChapterRepository.GetChapters();

                return new JsonResult(new GetChaptersResponse()
                {
                    Chapters = ChapterItems
                });
            }
            catch (UnsupportedMediaTypeException ex)
            {
                log.LogError(ex, "Unsupported media type returned");
                Error = "Unsupported Media Type: " + ex.Message + "|" + ex.StackTrace;
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                Error = "Error.  Content: " + Content + ", " + ex.Message + "|" + ex.StackTrace;
            }

            var ErrorResponse = new GetChaptersResponse()
            {
                Chapters = null,
                Error = Error
            };
            return new JsonResult(ErrorResponse);

        }
    }
}
