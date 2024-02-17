using NordicNest.Model;

namespace NordicNest.DAL
{
    public interface IPriceDetailRepository
    {
        PriceDetail GetPriceDetail(string id);
        IEnumerable<PriceDetail> GetPriceDetails(string id);
    }
}
