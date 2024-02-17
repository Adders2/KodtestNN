using NordicNest.Model;

namespace NordicNest.Services
{
    public interface IPriceDetailService
    {
        IEnumerable<PriceDetail> GetPriceDetails(string id);

        PriceDetail GetPriceDetail(string id);
    }
}
