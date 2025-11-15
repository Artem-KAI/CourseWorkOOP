using DAL.Entities;
using DAL.Storage;

namespace DAL.Repositories
{
    public class EmployerRepository : JsonRepositoryBase<EmployerEntity>, DAL.Interfaces.IEmployerRepository
    {
        private const string FileName = "employers.json";

        public EmployerRepository(FileDataContext context) : base(context, FileName) { }

        public IEnumerable<EmployerEntity> FindByCompanyName(string namePart)
        {
            if (string.IsNullOrWhiteSpace(namePart)) return new List<EmployerEntity>();
            return Find(e => !string.IsNullOrWhiteSpace(e.CompanyName) && e.CompanyName.IndexOf(namePart, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}
