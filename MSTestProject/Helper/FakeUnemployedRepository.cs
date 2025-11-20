using DAL.Entities;
using DAL.Interfaces;

namespace MSTestProject.Helper
{
    public class FakeUnemployedRepository : IUnemployedRepository
    {
        public List<UnemployedEntity> Data { get; } = new List<UnemployedEntity>();

        public IEnumerable<UnemployedEntity> GetAll()
        {
            return Data;
        }

        public UnemployedEntity GetById(Guid id)
        {
            return Data.FirstOrDefault(x => x.Id == id);
        }

        public void Add(UnemployedEntity item) => Data.Add(item);

        public void Update(UnemployedEntity item)
        {
            var idx = Data.FindIndex(x => x.Id == item.Id);
            if (idx >= 0) Data[idx] = item;
        }

        public void Delete(Guid id) => Data.RemoveAll(x => x.Id == id);

        public IEnumerable<UnemployedEntity> Find(Func<UnemployedEntity, bool> predicate) => Data.Where(predicate);

    }
}
