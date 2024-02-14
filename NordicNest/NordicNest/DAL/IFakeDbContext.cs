using NordicNest.Model;

namespace NordicNest.DAL
{
    public interface IFakeDbContext
    {
        // Might be a bit weird to search by catalogentrycode here.... we'll see.
        // Idea was to have the PK as key in the faked db.
        public Dictionary<int, PriceDetail> Values { get; }
    }
}
