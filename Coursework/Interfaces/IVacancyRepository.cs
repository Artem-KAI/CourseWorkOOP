using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IVacancyRepository : IRepository<VacancyEntity>
    {
        IEnumerable<VacancyEntity> GetByCategory(string category);
        IEnumerable<VacancyEntity> GetOpenVacancies();
    }
}
