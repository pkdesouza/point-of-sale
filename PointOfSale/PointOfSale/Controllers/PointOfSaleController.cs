using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PointOfSale.Filters;
using PointOfSaleDomain.Exceptions;
using PointOfSaleService.Interface;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PointOfSale.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PointOfSaleController : ControllerBase
    {
        private readonly ILogger<PointOfSaleController> _logger;
        public IChangeService ChangeService { get; set; }
        public ITransactionService TransactionService { get; set; }
        public PointOfSaleController(IChangeService changeService, ITransactionService transactionService)
        {
            ChangeService = changeService;
            TransactionService = transactionService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetTransactions()
        {
            try
            {
                return Ok(await TransactionService.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CalculeChange(decimal valueToPay, decimal totalValue) {
            try
            {
                var result = await ChangeService.GetChangeAsync(new PointOfSaleDomain.PointOfSale { ValueToPay = valueToPay, TotalValue = totalValue});
                return Ok(result);
            }
            catch (Exception ex) when (ex is PointOfSaleException)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
    }
}
