using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;

namespace PointOfSaleService
{
    public abstract class BaseService
    {
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            }
        }
    }
}
