using NordicNest.DAL;
using NordicNest.Model;

namespace NordicNest.Services
{
    public class PriceDetailService : IPriceDetailService
    {
        private readonly IPriceDetailRepository _priceDetailRepository;

        public PriceDetailService(IPriceDetailRepository repository)
        {
            _priceDetailRepository = repository;
        }
        public IEnumerable<PriceDetail> GetPriceDetails()
        {
            var priceDetail = new PriceDetail
            {
                CatalogEntryCode = "test",
                Created = DateTime.Now,
                CurrencyCode = "test",
                MarketId = "test",
                Modified = DateTime.Now,
                PriceValueId = 1,
                UnitPrice = 1,
                ValidFrom = DateTime.Now,
                ValidUntil = DateTime.Now
            };


            return new List<PriceDetail> { priceDetail};
        }
    }
}
