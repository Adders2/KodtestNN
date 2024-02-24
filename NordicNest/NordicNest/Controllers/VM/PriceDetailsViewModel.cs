using NordicNest.Model.DTO;
using System.ComponentModel;

namespace NordicNest.Controllers.VM
{
    public class PriceDetailsViewModel
    {
        public string Market { get; set; }
        public string Price { get; set; }
        public string Currency { get; set; }
        public string StartAndEnd { get; set; }

        public PriceDetailsViewModel(PriceDetailDTO priceDetail)
        {
            Market = priceDetail.MarketId;
            Price = string.Format("{0:0.00}", priceDetail.Price);
            Currency = priceDetail.CurrencyCode;
            StartAndEnd = string.Format($"{priceDetail.ValidFrom} - {(priceDetail.ValidUntil == DateTime.MinValue ? "-" : priceDetail.ValidUntil)}");
        }
    }
}
