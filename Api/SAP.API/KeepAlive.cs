using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace SAP.API
{
    public class KeepAlive
    {

        [FunctionName("KeepAlive")]
        [FixedDelayRetry(5, "00:00:02")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
            HttpRequest req,
            ILogger log
            )
        {
            return new JsonResult(new { Success = true });

        }
    }

   
}
