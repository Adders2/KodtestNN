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

        public PriceDetail GetPriceDetail(string id)
        {
            return _priceDetailRepository.GetPriceDetail(id);
        }

        public IEnumerable<PriceDetail> GetPriceDetails(string id)
        {
            return _priceDetailRepository.GetPriceDetails(id);
        }
    }
}
