using BLL.Models;

namespace BLL.Interfaces
{
    public interface IUnemployedService
    {
        IEnumerable<UnemployedModel> GetAll();
        UnemployedModel GetById(Guid id);
        void Add(UnemployedModel model);
        void Update(UnemployedModel model);
        void Delete(Guid id);

        IEnumerable<UnemployedModel> Search(string keyword);

        IEnumerable<UnemployedModel> GetSortedByFirstName();
        IEnumerable<UnemployedModel> GetSortedByLastName();

    }
}
