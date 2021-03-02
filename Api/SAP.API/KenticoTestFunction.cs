using CMS.Membership;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP.API
{
    public class GetUsersFunction
    {
        private readonly IUserInfoProvider userProvider;


        // UserInfoProvider injected into the azure function
        public GetUsersFunction(IUserInfoProvider userProvider)
        {
            this.userProvider = userProvider;
        }


        [FunctionName("GetUsersFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var users = await userProvider.Get()
                .Columns(nameof(UserInfo.UserName))
                .GetEnumerableResultAsync();

            var userNames = users.Select(user => user.GetString(0));

            return new OkObjectResult(String.Join(Environment.NewLine, userNames));
        }
    }
}
