using BLL.Models;
using BLL.Mappers;
using BLL.Interfaces;
using BLL.Exceptions;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ResumeService : IResumeService
    {
        private readonly IResumeRepository _repository;

        public ResumeService(IResumeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Add(ResumeModel model)
        {
            if (model == null) throw new BusinessException("Resume model is null.");
            _repository.Add(ResumeMapper.ToEntity(model));
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<ResumeModel> GetAll()
        {
            return _repository.GetAll().Select(ResumeMapper.ToModel);
        }

        public ResumeModel GetById(Guid id)
        {
            var entity = _repository.GetById(id);
            if (entity == null) throw new NotFoundException("Resume not found.");
            return ResumeMapper.ToModel(entity);
        }

        public void Update(ResumeModel model)
        {
            if (model == null) throw new BusinessException("Resume model is null.");
            _repository.Update(ResumeMapper.ToEntity(model));
        }

        ///////////////////////// 1.1 /////////////////////////////
        public void AddCategory(Guid resumeId, string category)
        {
            var resume = _repository.GetById(resumeId);
            resume.Category = category;
            _repository.Update(resume);
        }

        ///////////////////////// 1.2 /////////////////////////////
        public void RemoveCategory(Guid resumeId)
        {
            var resume = _repository.GetById(resumeId);
            resume.Category = null;
            _repository.Update(resume);
        }

        ///////////////////////// 1.6 /////////////////////////////
        public IEnumerable<ResumeModel> GetSortedByTitle()
        {
            return _repository.GetAll()
                .OrderBy(r => r.Title)
                .Select(ResumeMapper.ToModel);
        }
        ///////////////////////// 1.7 /////////////////////////////
        public IEnumerable<ResumeModel> GetSortedByCategory()
        {
            return _repository.GetAll()
                .OrderBy(r => r.Category)
                .Select(ResumeMapper.ToModel);
        }

    }
}
