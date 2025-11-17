using BLL.Models;

namespace BLL.Interfaces
{
    public interface IEmployerService
    {
        IEnumerable<EmployerModel> GetAll();
        EmployerModel GetById(Guid id);
        void Add(EmployerModel model);
        void Update(EmployerModel model);
        void Delete(Guid id);

        IEnumerable<EmployerModel> GetSortedByFirstName();
        IEnumerable<EmployerModel> GetSortedByLastName();
    }
}
