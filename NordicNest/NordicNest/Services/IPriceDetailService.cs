using NordicNest.Model;
using NordicNest.Model.DTO;

namespace NordicNest.Services
{
    public interface IPriceDetailService
    {
        IEnumerable<PriceDetailDTO> GetPriceDetails(string id);
    }
}
