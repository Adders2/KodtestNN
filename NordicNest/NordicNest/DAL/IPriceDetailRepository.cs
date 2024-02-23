using NordicNest.Model;

namespace NordicNest.DAL
{
    public interface IPriceDetailRepository
    {
        IEnumerable<PriceDetail> GetPriceDetails(string id);
    }
}
