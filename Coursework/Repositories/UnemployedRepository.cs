using DAL.Entities;
using DAL.Storage;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UnemployedRepository : JsonRepositoryBase<UnemployedEntity>, IUnemployedRepository
    {
        private const string FileName = "unemployed.json";

        public UnemployedRepository(FileDataContext context) : base(context, FileName) { }

        public IEnumerable<UnemployedEntity> FindByLastName(string lastName)
        {
            return Find(u => u.LastName != null && u.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
