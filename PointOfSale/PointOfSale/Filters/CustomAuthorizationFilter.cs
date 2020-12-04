using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Configuration;

namespace PointOfSale.Filters
{
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings.Get("UseAuthentication")) && 
                !context.HttpContext.Request.Headers.ContainsKey("SecretKey") ||
                context.HttpContext.Request.Headers["SecretKey"] != ConfigurationManager.AppSettings["SecretKey"])
                context.Result = new UnauthorizedResult();
        }
    }
}
