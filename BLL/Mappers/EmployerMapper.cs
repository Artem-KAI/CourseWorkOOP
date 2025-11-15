using BLL.Models;
using DAL.Entities;

namespace BLL.Mappers
{
    public static class EmployerMapper
    {

        public static EmployerModel ToModel(EmployerEntity entity)
        {
            var names = (entity.ContactPerson ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string firstName = names.Length > 0 ? names[0] : "";
            string lastName = names.Length > 1 ? names[1] : "";

            return new EmployerModel
            {
                Id = entity.Id,
                CompanyName = entity.CompanyName,
                FirstName = firstName,
                LastName = lastName,
                Phone = entity.Phone,
                Email = entity.Email,
                VacancyIds = new List<Guid>(entity.VacancyIds)
            };
        }

        public static EmployerEntity ToEntity(EmployerModel model)
        {
            return new EmployerEntity
            {
                Id = model.Id,
                CompanyName = model.CompanyName,
                ContactPerson = $"{model.FirstName} {model.LastName}",
                Phone = model.Phone,
                Email = model.Email,
                VacancyIds = new List<Guid>(model.VacancyIds)
            };
        }
    }
}
