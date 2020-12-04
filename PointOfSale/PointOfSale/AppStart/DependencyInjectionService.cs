using Microsoft.Extensions.DependencyInjection;
using PointOfSaleService;
using PointOfSaleService.Interface;

namespace PointOfSale.AppStart
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection RegisterDependencyServices(
            this IServiceCollection services)
        {
            #region services
            services.AddScoped<ICashService, CashService>();
            services.AddScoped<ICoinService, CoinService>();
            services.AddScoped<IChangeService, ChangeService>();
            #endregion
            return services;
        }
    }
}
