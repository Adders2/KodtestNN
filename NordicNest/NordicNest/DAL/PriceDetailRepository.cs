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

        public IEnumerable<PriceDetail> GetPriceDetails(string id)
        {
            // Some kinda fake db lookup as such.
            return _fakeDbContext.Values
                .Where(v => v.Value.CatalogEntryCode == id)
                .Select(v => v.Value);
        }
    }
}
