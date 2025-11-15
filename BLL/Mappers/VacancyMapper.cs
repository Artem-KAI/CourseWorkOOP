using BLL.Models;
using DAL.Entities;

namespace BLL.Mappers
{
    public static class VacancyMapper
    {
        public static VacancyModel ToModel(VacancyEntity entity)
        {
            return new VacancyModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Category = entity.Category,
                EmployerId = entity.EmployerId,
                PostedDate = entity.PostedDate,
                SalaryFrom = entity.SalaryFrom,
                SalaryTo = entity.SalaryTo,
                IsOpen = entity.IsOpen
            };
        }

        public static VacancyEntity ToEntity(VacancyModel model)
        {
            return new VacancyEntity
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Category = model.Category,
                EmployerId = model.EmployerId,
                PostedDate = model.PostedDate,
                SalaryFrom = model.SalaryFrom,
                SalaryTo = model.SalaryTo,
                IsOpen = model.IsOpen
            };
        }
    }
}
