using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PointOfSale.AppStart
{
    public class SwaggerConfig
    {
        public class AddAuthorizationHeaderParameterOperationFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                if (operation.Parameters != null)
                {
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = "SecretKey",
                        In = ParameterLocation.Header,
                        Description = "Chave de autenticação",
                        Required = false
                    });
                }
            }
        }
    }
}
