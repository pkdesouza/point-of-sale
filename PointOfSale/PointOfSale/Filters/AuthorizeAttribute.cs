using Microsoft.AspNetCore.Mvc;

namespace PointOfSale.Filters
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute() : base(typeof(CustomAuthorizationFilter)) 
        { 
        }
    }
}
