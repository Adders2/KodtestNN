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

            if (priceDetails == null || !priceDetails.Any())
            {
                throw new ArgumentNullException("No pricedetails could be found for provided id");
            }

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

                if (basePrice == null)
                {
                    break;
                }

                var remaining = grouping.Where(pd => IsValidPriceChange(basePrice, pd));

                if (remaining.Count() == 1)
                {
                    priceDetailDTOs.Add(new PriceDetailDTO(basePrice));
                }
                else
                {
                    for (int i = 0; i < remaining.Count(); i++)
                    {
                        var priceDetail = remaining.ElementAtOrDefault(i);

                        if (priceDetail == null)
                        {
                            break;
                        }

                        // Cheaper and later start date.
                        var nextValidOne = remaining.FirstOrDefault(pd =>
                        {
                            return (pd.UnitPrice <= priceDetail?.UnitPrice && pd.ValidFrom > priceDetail.ValidFrom);
                        });

                        var lastPriceDetail = remaining.ElementAtOrDefault(i - 1);

                        // If it starts before current price and but is more expensive, skip it
                        if (lastPriceDetail != null &&
                            (priceDetail?.UnitPrice > lastPriceDetail.UnitPrice && priceDetail.ValidFrom < lastPriceDetail.ValidUntil))
                        {
                            continue;
                        }

                        if (nextValidOne != null)
                        {
                            // Starts later than current ends. Should be current and baseprice beginning at current end, and ending at nextvalids start
                            if (nextValidOne?.ValidFrom > priceDetail?.ValidUntil)
                            {
                                if (priceDetail.PriceValueId == basePrice.PriceValueId)
                                {
                                    priceDetailDTOs.Add(new PriceDetailDTO(basePrice, validUntil: nextValidOne.ValidFrom));
                                }
                                else
                                {
                                    priceDetailDTOs.Add(new PriceDetailDTO(priceDetail));
                                    priceDetailDTOs.Add(new PriceDetailDTO(basePrice, validFrom: priceDetail.ValidUntil, validUntil: nextValidOne.ValidFrom));
                                }
                            }
                            // Starts earlier than current ends, cut current price short
                            else if (nextValidOne?.ValidFrom < priceDetail.ValidUntil)
                            {
                                priceDetailDTOs.Add(new PriceDetailDTO(priceDetail, validUntil: nextValidOne.ValidFrom));
                            }
                        }
                        // No other valid. Should be current price replaced by baseprice at its end
                        else
                        {
                            priceDetailDTOs.Add(new PriceDetailDTO(priceDetail));
                            priceDetailDTOs.Add(new PriceDetailDTO(basePrice, validFrom: priceDetail.ValidUntil));
                        }
                    }
                }
            }
            return priceDetailDTOs;
        }

        private bool IsValidPriceChange(PriceDetail current, PriceDetail next)
        {
            if (current.PriceValueId == next.PriceValueId)
            {
                return true;
            }

            var nextStartsLaterThanCurrent = next.ValidFrom > current.ValidFrom;
            var currentHasNoEnd = current.ValidUntil.HasValue && current.ValidUntil.Value == DateTime.MinValue;
            var nextStartsBeforeCurrentEnd = current.ValidUntil.HasValue && next.ValidFrom < current.ValidUntil.Value;
            var nextIsCheaperThanCurrent = next.UnitPrice < current.UnitPrice;
            var nextStartsEarlierThanToday = next.ValidFrom < DateTime.UtcNow;

            return (nextStartsLaterThanCurrent && nextStartsEarlierThanToday) &&
                ((currentHasNoEnd && nextIsCheaperThanCurrent) ||
                (nextStartsBeforeCurrentEnd && nextIsCheaperThanCurrent));
        }
    }
}
