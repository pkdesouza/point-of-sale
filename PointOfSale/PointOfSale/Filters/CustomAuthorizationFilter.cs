using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Configuration;

namespace PointOfSale.Filters
{
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("X-SECRETKEY") ||
                context.HttpContext.Request.Headers["X-SECRETKEY"] != ConfigurationManager.AppSettings["X-SECRETKEY"])
                context.Result = new UnauthorizedResult();
        }
    }
}
