/*using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Net;
using System;
using Microsoft.Extensions.Primitives;
using System.Linq;

namespace SAP.API
{
    public class CheckIP
    {

        [FunctionName("CheckIP")]
        [FixedDelayRetry(5, "00:00:02")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
            HttpRequest req,
            ILogger log
            )
        {
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
                return new JsonResult(new { IP = IP });
            } catch(Exception ex)
            {
                return new JsonResult(new { Error = ex.Message });
            }
        }
    }

   
}
*/