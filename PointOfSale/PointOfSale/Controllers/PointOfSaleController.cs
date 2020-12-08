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
        public PointOfSaleController(IChangeService changeService, ITransactionService transactionService, ILogger<PointOfSaleController> logger)
        {
            ChangeService = changeService;
            TransactionService = transactionService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetTransactions()
        {
            _logger.LogInformation($"Executando o método {nameof(GetTransactions)}");
            return Ok(await TransactionService.GetAllAsync());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CalculeChange(decimal valueToPay, decimal totalValue)
        {
            _logger.LogInformation($"Executando o método {nameof(CalculeChange)}");
            var result = await ChangeService.GetChangeAsync(new PointOfSaleDomain.PointOfSale { ValueToPay = valueToPay, TotalValue = totalValue });
            _logger.LogInformation($"Execução realizada com sucesso do método {nameof(CalculeChange)}");
            return Ok(result);
        }
    }
}
