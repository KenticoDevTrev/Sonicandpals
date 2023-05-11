using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using SAP.Models.Interfaces;

namespace SAP.API
{
    public class KeepAlive
    {
        public KeepAlive(IKeepAliveService keepAliveService)
        {
            KeepAliveService = keepAliveService;
        }

        public IKeepAliveService KeepAliveService { get; }

        [FunctionName("KeepAlive")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
            HttpRequest req,
            ILogger log
            )
        {
            return new JsonResult(new { Success = KeepAliveService.TouchDatabase() });
        }
    }

   
}
