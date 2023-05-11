using Microsoft.Data.SqlClient;
using SAP.Models.Interfaces;
using System;
using System.Data;
using System.Xml.Linq;

namespace Sap.API.EF.EntityFramework.Implementations
{
    public class KeepAliveService : IKeepAliveService
    {
        public KeepAliveService() { }

        public bool TouchDatabase()
        {
            try
            {
                string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    if (connection != null && connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }


                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = @"select 1 as Result";
                        var reader = cmd.ExecuteReader();
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
