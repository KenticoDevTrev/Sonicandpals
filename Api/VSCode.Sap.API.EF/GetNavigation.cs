﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using SAP.Models.Interfaces;
using SAP.Models.SaP;
using System.Collections.Generic;

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
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, new string[]{ "post", "get" }, Route = null)]
            HttpRequest req,
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

}
