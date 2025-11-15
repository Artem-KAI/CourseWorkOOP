using BLL.Models;
using DAL.Entities;
 
namespace BLL.Mappers
{
    public static class ResumeMapper
    {
        public static ResumeModel ToModel(ResumeEntity entity)
        {
            return new ResumeModel
            {
                Id = entity.Id,
                UnemployedId = entity.UnemployedId,
                Title = entity.Title,
                Skills = new List<string>(entity.Skills),
                ExperienceYears = entity.ExperienceYears,
                Education = entity.Education,
                CreatedDate = entity.CreatedDate,
                Category = entity.Category
            };
        }

        public static ResumeEntity ToEntity(ResumeModel model)
        {
            return new ResumeEntity
            {
                Id = model.Id,
                UnemployedId = model.UnemployedId,
                Title = model.Title,
                Skills = new List<string>(model.Skills),
                ExperienceYears = model.ExperienceYears,
                Education = model.Education,
                CreatedDate = model.CreatedDate,
                Category = model.Category
            };
        }
    }
}
