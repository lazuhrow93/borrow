namespace Borrow.Data.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public IQueryable<TEntity> Query { get; }
        public List<TEntity> FetchAll();
        public TEntity GetById(int id);
        void Add(TEntity entity);
        void Add(List<TEntity> entity);
        void Delete(TEntity entity);
        void Save();
    }
}
