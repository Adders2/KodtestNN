using NordicNest.DAL;
using NordicNest.Services;

namespace NordicNest
{
    public static class DependencyInjection
    {
        public static void Initiate(IServiceCollection services)
        {
            services.AddSingleton<IFakeDbContext, FakeDbContext>();

            services.AddScoped<IPriceDetailRepository, PriceDetailRepository>();

            services.AddScoped<IPriceDetailService, PriceDetailService>();
            
        }
    }
}
