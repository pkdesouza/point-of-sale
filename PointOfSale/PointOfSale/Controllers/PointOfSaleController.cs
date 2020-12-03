using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PointOfSale.Filters;

namespace PointOfSale.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PointOfSaleController : ControllerBase
    {
        private readonly ILogger<PointOfSaleController> _logger;
        
        public PointOfSaleController(ILogger<PointOfSaleController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public string Get() => "Ok!";
    }
}
