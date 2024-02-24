namespace NordicNest.Model.DTO
{
    public class PriceDetailDTO
    {
        public string MarketId { get; set; }
        public decimal Price { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidUntil { get; set; }

        public PriceDetailDTO(PriceDetail priceDetail, DateTime? validUntil = null, DateTime? validFrom = null)
        {
            CurrencyCode = priceDetail.CurrencyCode;
            MarketId = priceDetail.MarketId;
            Price = priceDetail.UnitPrice;
            ValidFrom = validFrom ?? priceDetail.ValidFrom.Value;
            ValidUntil = validUntil ?? (priceDetail.ValidUntil == DateTime.MinValue ? null : priceDetail.ValidUntil);
        }
    }
}
