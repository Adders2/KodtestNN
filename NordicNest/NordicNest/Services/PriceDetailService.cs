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
            return _priceDetailRepository.GetPriceDetails();
        }
    }
}
