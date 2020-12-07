using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
namespace PointOfSale.Filters
{
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;
        public CustomAuthorizationFilter(IConfiguration configuration) {
            _configuration = configuration;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (_configuration.GetValue<bool>("UseAuthentication") && 
                context.HttpContext.Request.Headers["SecretKey"] != _configuration.GetValue<string>("SecretKey"))
                context.Result = new UnauthorizedResult();
        }
    }
}
