using BLL.Models;

namespace BLL.Interfaces
{
    public interface IResumeService
    {
        IEnumerable<ResumeModel> GetAll();
        ResumeModel GetById(Guid id);
        void Add(ResumeModel model);
        void Update(ResumeModel model);
        void Delete(Guid id);

        void AddCategory(Guid resumeId, string category);
        void RemoveCategory(Guid resumeId);
        IEnumerable<ResumeModel> GetSortedByTitle();
        IEnumerable<ResumeModel> GetSortedByCategory();
    }
}
