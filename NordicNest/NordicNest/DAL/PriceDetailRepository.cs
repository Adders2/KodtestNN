using NordicNest.Model;

namespace NordicNest.DAL
{
    public class PriceDetailRepository : IPriceDetailRepository
    {
        private readonly IFakeDbContext _fakeDbContext;
        public PriceDetailRepository(IFakeDbContext fakeDbContext)
        {
            _fakeDbContext = fakeDbContext;
        }

        public IEnumerable<PriceDetail> GetPriceDetails()
        {
            // Some kinda fake db lookup as such.
            return _fakeDbContext.Values.Select(v => v.Value);
        }
    }
}
