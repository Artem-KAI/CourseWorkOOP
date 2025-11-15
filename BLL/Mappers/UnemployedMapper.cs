using BLL.Models;
using DAL.Entities;
 
namespace BLL.Mappers
{
    public static class UnemployedMapper
    {
        public static UnemployedModel ToModel(UnemployedEntity entity)
        {
            return new UnemployedModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DOB = entity.DOB,
                Phone = entity.Phone,
                Email = entity.Email,
                ResumeIds = new List<Guid>(entity.ResumeIds)
            };
        }

        public static UnemployedEntity ToEntity(UnemployedModel model)
        {
            return new UnemployedEntity
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DOB = model.DOB,
                Phone = model.Phone,
                Email = model.Email,
                ResumeIds = new List<Guid>(model.ResumeIds)
            };
        }
    }
}
