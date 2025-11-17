using BLL.Models;

namespace BLL.Interfaces
{
    public interface IVacancyService
    {
        IEnumerable<VacancyModel> GetAll();
        VacancyModel GetById(Guid id);
        void Add(VacancyModel model);
        void Update(VacancyModel model);
        void Delete(Guid id);
        IEnumerable<VacancyModel> GetByCategory(string category);

        void AddCategory(Guid vacancyId, string category);
        void RemoveCategory(Guid vacancyId);
        IEnumerable<VacancyModel> GetSortedByTitle();
        IEnumerable<VacancyModel> GetSortedByCategory();
    }
}
