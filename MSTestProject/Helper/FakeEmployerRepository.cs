using DAL.Entities;
using DAL.Interfaces;

namespace MSTestProject.Helper
{
    public class FakeEmployerRepository : IEmployerRepository
    {
        public List<EmployerEntity> Data { get; } = new();

        public void Add(EmployerEntity item) => Data.Add(item);

        public void Update(EmployerEntity item)
        {
            var old = Data.FirstOrDefault(x => x.Id == item.Id);
            if (old != null)
            {
                Data.Remove(old);
                Data.Add(item);
            }
        }

        public void Delete(Guid id)
        {
            var item = Data.FirstOrDefault(x => x.Id == id);
            if (item != null) Data.Remove(item);
        }

        public EmployerEntity GetById(Guid id) =>
            Data.FirstOrDefault(x => x.Id == id);

        public IEnumerable<EmployerEntity> GetAll() => Data;

        public IEnumerable<EmployerEntity> Find(Func<EmployerEntity, bool> predicate) =>
            Data.Where(predicate);

        // 🔥 Обов’язковий метод з інтерфейсу
        public IEnumerable<EmployerEntity> FindByCompanyName(string namePart)
        {
            if (string.IsNullOrWhiteSpace(namePart))
                return new List<EmployerEntity>();

            return Data.Where(x =>
                x.CompanyName != null &&
                x.CompanyName.Contains(namePart, StringComparison.OrdinalIgnoreCase));
        }
    }
}
