using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Mappers;
using BLL.Models;
using DAL.Interfaces;

namespace BLL.Services
{
    public class UnemployedService : IUnemployedService
    {
        private readonly IUnemployedRepository _repository;

        public UnemployedService(IUnemployedRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Add(UnemployedModel model)
        {
            if (model == null) throw new BusinessException("Unemployed model is null.");
            _repository.Add(UnemployedMapper.ToEntity(model));
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<UnemployedModel> GetAll()
        {
            return _repository.GetAll().Select(UnemployedMapper.ToModel);
        }

        public UnemployedModel GetById(Guid id)
        {
            var entity = _repository.GetById(id);
            if (entity == null) throw new NotFoundException("Unemployed not found.");
            return UnemployedMapper.ToModel(entity);
        }

        public void Update(UnemployedModel model)
        {
            if (model == null) throw new BusinessException("Unemployed model is null.");
            _repository.Update(UnemployedMapper.ToEntity(model));
        }

        ///////////////////////  4.2  ////////////////////////////
        public IEnumerable<UnemployedModel> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return Enumerable.Empty<UnemployedModel>();

            keyword = keyword.ToLower();

            return _repository.GetAll()
                .Where(u =>
                    (!string.IsNullOrEmpty(u.FirstName) && u.FirstName.ToLower().Contains(keyword)) ||
                    (!string.IsNullOrEmpty(u.LastName) && u.LastName.ToLower().Contains(keyword)) ||
                    (!string.IsNullOrEmpty(u.Email) && u.Email.ToLower().Contains(keyword))
                )
                .Select(UnemployedMapper.ToModel);
        }


        public IEnumerable<UnemployedModel> GetSortedByFirstName()
        {
            return _repository.GetAll()
                .OrderBy(u => u.FirstName)
                .Select(UnemployedMapper.ToModel);
        }

        public IEnumerable<UnemployedModel> GetSortedByLastName()
        {
            return _repository.GetAll()
                .OrderBy(u => u.LastName)
                .Select(UnemployedMapper.ToModel);
        }

    }
}
