using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using SAP.Models.Interfaces;
using SAP.Models.SaP;

namespace SAP.API
{
    public class GetPage
    {
        public GetPage(IPageRepository pageRepository)
        {
            PageRepository = pageRepository;
        }

        public IPageRepository PageRepository { get; }

        [FunctionName("GetPage")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            GetPageRequest Request,
            ILogger log
            )
        {
            string Error = "";
            string Content = "";
            try
            {
                
                var Page = PageRepository.GetPage(Request.PageIdentifier);
                return new JsonResult(new PageResponse()
                {
                    Page = Page
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

            var ErrorResponse = new PageResponse()
            {
                Page = null,
                Error = Error
            };
            return new JsonResult(ErrorResponse);

        }
    }

    public class GetPageRequest
    {
        public Guid PageIdentifier { get; set; }

        public override string ToString()
        {
            return $"{PageIdentifier}";
        }
    }
}
