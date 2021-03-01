using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SAP.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace SAP.API
{
    public static class TestDB
    {
        [FunctionName("TestDB")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "HBS-TFAYAS05\\SQLExpress2014";
                builder.UserID = "Sonicandpals";
                builder.Password = "Sonicandpals";
                builder.InitialCatalog = "Sonicandpals";
                
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * from SAP_Episode where EpisodeNumber = 101";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var tb = new DataTable();
                            tb.Load(reader);
                            EpisodeInfo episode = new EpisodeInfo(tb.Rows[0]);
                            return new OkObjectResult("Episode: "+episode.EpisodeTitle);
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\nDone. Press enter.");
            Console.ReadLine();


            return new OkObjectResult("Failed");
        }
    }
}
