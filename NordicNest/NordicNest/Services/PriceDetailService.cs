using NordicNest.DAL;
using NordicNest.Model;
using NordicNest.Model.DTO;

namespace NordicNest.Services
{
    public class PriceDetailService : IPriceDetailService
    {
        private readonly IPriceDetailRepository _priceDetailRepository;

        public PriceDetailService(IPriceDetailRepository repository)
        {
            _priceDetailRepository = repository;
        }

        public IEnumerable<PriceDetailDTO> GetPriceDetails(string id)
        {
            var priceDetails = _priceDetailRepository.GetPriceDetails(id);

            return GetPriceIntervals(priceDetails);
        }

      
        private List<PriceDetailDTO> GetPriceIntervals(IEnumerable<PriceDetail> priceDetails)
        {
            var ordered = priceDetails.OrderBy(pd => pd.ValidFrom);

            // Handle prices per market and currency
            var marketCurrencyGrouping = ordered.GroupBy(pd => new { pd.MarketId, pd.CurrencyCode });

            var priceDetailDTOs = new List<PriceDetailDTO>();

            foreach (var grouping in marketCurrencyGrouping)
            {
                var basePrice = grouping.FirstOrDefault();

                for (int i = 0; i < grouping.Count(); i++)
                {
                    var priceDetail = grouping.ElementAtOrDefault(i);

                    if (priceDetail != null)
                    {
                        var nextValid = GetNextValidPriceDTO(priceDetail, grouping);

                        if (nextValid == null)
                        {
                            priceDetailDTOs.Add(new PriceDetailDTO(priceDetail));

                            var next = grouping.ElementAtOrDefault(i + 1);
                            priceDetailDTOs.Add(
                                new PriceDetailDTO(basePrice, validFrom: priceDetail?.ValidUntil, validUntil: next?.ValidFrom));
                        }
                        else
                        {
                            priceDetailDTOs.Add(new PriceDetailDTO(priceDetail, validUntil: nextValid?.ValidFrom));
                        }
                    }
                }
            }

            return priceDetailDTOs;
        }

        private PriceDetailDTO? GetNextValidPriceDTO(PriceDetail current, IEnumerable<PriceDetail> group)
        {
            group = group.Where(pd => pd.PriceValueId != current.PriceValueId);

            var nextValid = group.FirstOrDefault(pd =>
            {
                var nextStartsLaterThanCurrent = pd.ValidFrom > current.ValidFrom;
                var currentHasNoEnd = current.ValidUntil.HasValue && current.ValidUntil.Value == DateTime.MinValue;
                var nextStartsBeforeCurrentEnd = current.ValidUntil.HasValue && pd.ValidFrom < current.ValidUntil.Value;
                var nextIsCheaperThanCurrent = pd.UnitPrice < current.UnitPrice;

                // Should no price increases be allowed from "basePrice"? DKK for EN market seems to have an increase from baseprice...
                return nextStartsLaterThanCurrent &&
                    (currentHasNoEnd ||
                    (nextStartsBeforeCurrentEnd && nextIsCheaperThanCurrent));
            });

            if (nextValid == null)
                return null;

            return new PriceDetailDTO(nextValid);
        }
    }
}
