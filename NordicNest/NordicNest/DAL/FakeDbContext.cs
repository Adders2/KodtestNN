using NordicNest.Model;

namespace NordicNest.DAL
{
    public class FakeDbContext : IFakeDbContext
    {
        public Dictionary<int, PriceDetail> Values { get; set; }

        public FakeDbContext()
        {
            // Just for now. Maybe read CSV file into this later
            Values = new Dictionary<int, PriceDetail>();
        }
    }
}
