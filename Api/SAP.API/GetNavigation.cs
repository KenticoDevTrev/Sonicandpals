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
    public class GetNavigation
    {
        public GetNavigation(IPageRepository pageRepository)
        {
            PageRepository = pageRepository;
        }

        public IPageRepository PageRepository { get; }

        [FunctionName("GetNavigation")]
        [FixedDelayRetry(5, "00:00:02")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            GetNavigation Request,
            ILogger log
            )
        {
            string Error = "";
            string Content = "";
            try
            {

                var NavItems = PageRepository.GetNavigation();
                
                return new JsonResult(NavItems);
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

    public class GetNavigationRequest
    {

        public override string ToString()
        {
            return $"";
        }
    }
}
