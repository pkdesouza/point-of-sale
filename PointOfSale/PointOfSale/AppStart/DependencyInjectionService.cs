using Microsoft.Extensions.DependencyInjection;
using PointOfSaleInfra;
using PointOfSaleService;
using PointOfSaleService.Interface;

namespace PointOfSale.AppStart
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection RegisterDependencyServices(
            this IServiceCollection services)
        {
            services.AddTransient<PointOfSaleContext, PointOfSaleContext>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<ICoinService, CoinService>();
            services.AddScoped<IChangeService, ChangeService>();

            return services;
        }
    }
}
