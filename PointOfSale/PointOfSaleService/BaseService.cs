using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PointOfSaleService
{
    public abstract class BaseService
    {
        private readonly IConfiguration _configuration;
        public BaseService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        protected IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_configuration.GetSection("ConnectionString").Value);
            }
        }
    }
}
