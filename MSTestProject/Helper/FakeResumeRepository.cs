using DAL.Entities;
using DAL.Interfaces;

namespace MSTestProject.Helper
{
    public class FakeResumeRepository : IResumeRepository
    {
        public List<ResumeEntity> Data { get; } = new List<ResumeEntity>();

        public IEnumerable<ResumeEntity> GetAll() => Data;

        public ResumeEntity GetById(Guid id) => Data.FirstOrDefault(x => x.Id == id);

        public void Add(ResumeEntity item) => Data.Add(item);

        public void Update(ResumeEntity item)
        {
            var idx = Data.FindIndex(x => x.Id == item.Id);
            if (idx >= 0) Data[idx] = item;
        }

        public void Delete(Guid id) => Data.RemoveAll(x => x.Id == id);

        public IEnumerable<ResumeEntity> Find(Func<ResumeEntity, bool> predicate) => Data.Where(predicate);
    }
}
