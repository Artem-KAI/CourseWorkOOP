using DAL.Entities;
using DAL.Interfaces;
using DAL.Storage;

namespace DAL.Repositories
{
    public class VacancyRepository : JsonRepositoryBase<VacancyEntity>, IVacancyRepository
    {
        private const string FileName = "vacancies.json";

        public VacancyRepository(FileDataContext context) : base(context, FileName) { }

        public IEnumerable<VacancyEntity> GetByCategory(string category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));
            return Find(v => v.Category != null && v.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<VacancyEntity> GetOpenVacancies()
        {
            return Find(v => v.IsOpen);
        }
    }
}
