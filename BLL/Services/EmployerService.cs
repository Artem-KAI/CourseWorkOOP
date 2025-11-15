using BLL.Models;
using BLL.Mappers;
using BLL.Interfaces;
using BLL.Exceptions;
using DAL.Interfaces;

namespace BLL.Services
{
    public class EmployerService : IEmployerService
    {
        private readonly IEmployerRepository _repository;

        public EmployerService(IEmployerRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Add(EmployerModel model)
        {
            if (model == null) throw new BusinessException("Employer model is null.");
            _repository.Add(EmployerMapper.ToEntity(model));
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<EmployerModel> GetAll()
        {
            return _repository.GetAll().Select(EmployerMapper.ToModel);
        }

        public EmployerModel GetById(Guid id)
        {
            var entity = _repository.GetById(id);
            if (entity == null) throw new NotFoundException("Employer not found.");
            return EmployerMapper.ToModel(entity);
        }

        public void Update(EmployerModel model)
        {
            if (model == null) throw new BusinessException("Employer model is null.");
            _repository.Update(EmployerMapper.ToEntity(model));
        }

        ///////////////////////  3.5.1  /////////////////////////
        public IEnumerable<EmployerModel> GetSortedByFirstName()
        {
            return _repository.GetAll()
                .Select(EmployerMapper.ToModel)
                .OrderBy(e => e.FirstName);
        }

        ///////////////////////  3.5.2  /////////////////////////
        public IEnumerable<EmployerModel> GetSortedByLastName()
        {
            return _repository.GetAll()
                .Select(EmployerMapper.ToModel)
                .OrderBy(e => e.LastName);
        }
    }
}
