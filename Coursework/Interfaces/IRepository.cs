namespace DAL.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        void Add(T item);
        void Update(T item);
        void Delete(Guid id);
        IEnumerable<T> Find(Func<T, bool> predicate);
    }
}


