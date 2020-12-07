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
            try
            {
                _logger.LogInformation($"Executando o método {nameof(GetTransactions)}");
                return Ok(await TransactionService.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro inesperado na execução do método {nameof(GetTransactions)}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CalculeChange(decimal valueToPay, decimal totalValue) 
        {
            try
            {
                _logger.LogInformation($"Executando o método {nameof(CalculeChange)}");
                var result = await ChangeService.GetChangeAsync(new PointOfSaleDomain.PointOfSale { ValueToPay = valueToPay, TotalValue = totalValue});
                _logger.LogInformation($"Execução realizada com sucesso do método {nameof(CalculeChange)}");
                return Ok(result);
            }
            catch (Exception ex) when (ex is PointOfSaleException)
            {
                _logger.LogWarning($"Dados invalidos para execução do método {nameof(CalculeChange)}", ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro inesperado na execução do método {nameof(CalculeChange)}", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
    }
}
