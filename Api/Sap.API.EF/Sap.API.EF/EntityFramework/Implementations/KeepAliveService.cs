using SAP.Models.Interfaces;
using System;
using System.Data;

namespace Sap.API.EF.EntityFramework.Implementations
{
    public class KeepAliveService : IKeepAliveService
    {
        public KeepAliveService() { }

        public bool TouchDatabase()
        {
            try { 
            string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");
            var connection = new System.Data.SqlClient.SqlConnection(connectionString);

            if (connection != null && connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var dt = new DataTable();

            using (var com = new System.Data.SqlClient.SqlDataAdapter("select 1 as Result", connection))
            {
                com.Fill(dt);
            }
            return true;
            } catch(Exception ex)
            {
                return false;
            }
        }
    }
}
