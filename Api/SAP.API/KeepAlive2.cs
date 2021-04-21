using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using SAP.Models.Interfaces;
using CMS.Core;
using System;
using CMS.DataEngine;
using System.Collections.Generic;
using System.Linq;

namespace SAP.API
{
    public class KeepAlive2
    {
        public KeepAlive2()
        {
        }

        public IKeepAliveService KeepAliveService { get; }

        [FunctionName("KeepAlive2")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
            HttpRequest req
            )
        {
            List<string> Errors = new List<string>();
            try
            {
                _ = Service.Resolve<IEventLogWriter>();
            }
            catch (Exception)
            {
                Errors.Add("Unable to resolve IEventLogWriter");
            }

            try
            {
                _ = ConnectionHelper.ExecuteQuery("Select 1 as Temp", null, QueryTypeEnum.SQLQuery);
            } catch(Exception)
            {
                Errors.Add("Cannot access database");
            }
            
            return new JsonResult(new { Success = Errors.Count == 0, Message = String.Join(", ", Errors) });
        }
    }

   
}
