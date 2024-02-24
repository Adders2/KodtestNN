using NordicNest.Model;

namespace NordicNest.DAL
{
    public class FakeDbContext : IFakeDbContext
    {
        public Dictionary<int, PriceDetail> Values { get; set; } = new Dictionary<int, PriceDetail>();

        public FakeDbContext()
        {
            // Read csv file...
            var currentDir = AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(currentDir, "../../../price_detail.csv");
            var filePath = Path.GetFullPath(path);

            // Don't like doing this in a constructor... maybe in Program.cs instead.
            File.ReadAllLines(filePath)
                .Skip(1)
                .ToList().ForEach(v => ReadCSVContent(v));
        }

        private void ReadCSVContent(string csvContentRow)
        {
            var columns = csvContentRow.Split("\t");

            var validUntil = DateTime.TryParse(columns[7], out DateTime validUntilParsed) ? validUntilParsed : DateTime.MinValue;

            var pd = new PriceDetail
            {
                PriceValueId = int.Parse(columns[0]),
                Created = columns[1] != null ? DateTime.Parse(columns[1]) : null,
                Modified = columns[2] != null ? DateTime.Parse(columns[2]) : null,
                CatalogEntryCode = columns[3],
                MarketId = columns[4],
                CurrencyCode = columns[5],
                ValidFrom = columns[6] != null ? DateTime.Parse(columns[6]) : null,
                ValidUntil = validUntil,
                UnitPrice = decimal.Parse(columns[8])
            };

            Values.Add(pd.PriceValueId, pd);
        }
    }
}
