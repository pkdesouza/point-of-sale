using Microsoft.AspNetCore.Mvc;
using PointOfSale.Filters;

namespace PointOfSale.Filters
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute() : base(typeof(CustomAuthorizationFilter))
        {
        }
    }
}
