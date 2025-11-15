using DAL.Interfaces;
using DAL.Storage;
using DAL.Exceptions;

namespace DAL.Repositories
{
    public abstract class JsonRepositoryBase<T> where T : IEntity
    {
        private readonly string _fileName;
        private readonly FileDataContext _context;
        private readonly object _lock = new object();
        protected List<T> Items { get; private set; }

        protected JsonRepositoryBase(FileDataContext context, string fileName)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _fileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            Items = _context.LoadList<T>(_fileName);
        }

        public virtual IEnumerable<T> GetAll()
        {
            lock (_lock)
            {
                // повертаємо копію щоб уникнути зовнішніх модифікацій
                return Items.Select(x => x).ToList();
            }
        }

        public virtual T GetById(Guid id)
        {
            lock (_lock)
            {
                var item = Items.FirstOrDefault(i => i.Id == id);
                if (item == null)
                    throw new DataAccessException($"Entity of type {typeof(T).Name} with id {id} not found.");
                return item;
            }
        }

        public virtual void Add(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            lock (_lock)
            {
                if (Items.Any(i => i.Id == item.Id))
                    throw new DataAccessException($"Entity with id {item.Id} already exists.");

                Items.Add(item);
                Save();
            }
        }

        public virtual void Update(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            lock (_lock)
            {
                var idx = Items.FindIndex(i => i.Id == item.Id);
                if (idx == -1)
                    throw new DataAccessException($"Entity of type {typeof(T).Name} with id {item.Id} not found.");

                Items[idx] = item;
                Save();
            }
        }

        public virtual void Delete(Guid id)
        {
            lock (_lock)
            {
                var idx = Items.FindIndex(i => i.Id == id);
                if (idx == -1)
                    throw new DataAccessException($"Entity of type {typeof(T).Name} with id {id} not found.");

                Items.RemoveAt(idx);
                Save();
            }
        }

        public virtual IEnumerable<T> Find(Func<T, bool> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            lock (_lock)
            {
                return Items.Where(predicate).ToList();
            }
        }

        protected void Save()
        {
            try
            {
                _context.SaveList(_fileName, Items);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Failed to save data to storage.", ex);
            }
        }
    }
}
