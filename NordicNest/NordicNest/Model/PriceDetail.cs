namespace NordicNest.Model
{
    public class PriceDetail
    {
        // PK
        public int PriceValueId { get; set; }
        public DateTime? Created {  get; set; }
        public DateTime? Modified { get; set; }

        // SKU/ProductID
        public string CatalogEntryCode { get; set; } 

        public string MarketId { get; set; }

        // Valutekod (kanske enum istället)
        public string CurrencyCode { get; set; }

        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidUntil { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
