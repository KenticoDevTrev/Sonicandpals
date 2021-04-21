using CMS.DataEngine;
using SAP.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAP.Library.Implementations
{
    public class KeepAliveService : IKeepAliveService
    {
        public bool TouchDatabase()
        {
            try
            {
                var Results = ConnectionHelper.ExecuteQuery("select 1 as Result", null, QueryTypeEnum.SQLQuery);
                return true;
            } catch(Exception)
            {
                return false;
            }
        }
    }
}
