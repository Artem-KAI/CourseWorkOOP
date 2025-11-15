using DAL.Entities;
using DAL.Interfaces;

namespace MSTestProject.Helper
{
    public class FakeVacancyRepository : IVacancyRepository
    {
        public List<VacancyEntity> Data { get; } = new();

        public void Add(VacancyEntity item) => Data.Add(item);

        public void Update(VacancyEntity item)
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

        public VacancyEntity GetById(Guid id) =>
            Data.FirstOrDefault(x => x.Id == id);

        public IEnumerable<VacancyEntity> GetAll() => Data;

        public IEnumerable<VacancyEntity> Find(Func<VacancyEntity, bool> predicate) =>
            Data.Where(predicate);

        //   метод з інтерфейсу
        public IEnumerable<VacancyEntity> GetByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return new List<VacancyEntity>();

            return Data.Where(x =>
                x.Category != null &&
                x.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }

        // 🔥 метод з інтерфейсу
        public IEnumerable<VacancyEntity> GetOpenVacancies()
        {
            return Data.Where(x => x.IsOpen);
        }
    }
}
