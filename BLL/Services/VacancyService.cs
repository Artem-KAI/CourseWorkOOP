using BLL.Models;
using BLL.Mappers;
using BLL.Interfaces;
using BLL.Exceptions;
using DAL.Interfaces;

namespace BLL.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly IVacancyRepository _repository;
 
        public VacancyService(IVacancyRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Add(VacancyModel model)
        {
            if (model == null) throw new BusinessException("Vacancy model is null.");
            _repository.Add(VacancyMapper.ToEntity(model));
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<VacancyModel> GetAll()
        {
            return _repository.GetAll().Select(VacancyMapper.ToModel);
        }

        public VacancyModel GetById(Guid id)
        {
            var entity = _repository.GetById(id);
            if (entity == null) throw new NotFoundException("Vacancy not found.");
            return VacancyMapper.ToModel(entity);
        }

        public IEnumerable<VacancyModel> GetByCategory(string category)
        {
            return _repository.GetByCategory(category).Select(VacancyMapper.ToModel);
        }

        public void Update(VacancyModel model)
        {
            if (model == null) throw new BusinessException("Vacancy model is null.");
            _repository.Update(VacancyMapper.ToEntity(model));
        }

        ///////////////////////// 1.1 /////////////////////////////
        public void AddCategory(Guid vacancyId, string category)
        {
            var vacancy = _repository.GetById(vacancyId);
            vacancy.Category = category;
            _repository.Update(vacancy);
        }

        ///////////////////////// 1.2 /////////////////////////////
        public void RemoveCategory(Guid vacancyId)
        {
            var vacancy = _repository.GetById(vacancyId);
            vacancy.Category = null;
            _repository.Update(vacancy);
        }

        ///////////////////////// 1.5 /////////////////////////////
        public IEnumerable<VacancyModel> GetSortedByTitle()
        {
            return _repository.GetAll()
                .OrderBy(v => v.Title)
                .Select(VacancyMapper.ToModel);
        }
        public IEnumerable<VacancyModel> GetSortedByCategory()
        {
            return _repository.GetAll()
                .OrderBy(v => v.Category)
                .Select(VacancyMapper.ToModel);
        }

    }
}
