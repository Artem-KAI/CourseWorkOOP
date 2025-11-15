using DAL.Entities;
 
namespace DAL.Interfaces
{
    public interface IEmployerRepository : IRepository<EmployerEntity>
    {
        IEnumerable<EmployerEntity> FindByCompanyName(string namePart);
    }
}