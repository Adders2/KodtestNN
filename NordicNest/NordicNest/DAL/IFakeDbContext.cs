using NordicNest.Model;

namespace NordicNest.DAL
{
    public interface IFakeDbContext
    {
        // Idea was to have the PK as key in the faked db.
        Dictionary<int, PriceDetail> Values { get; }
    }
}
