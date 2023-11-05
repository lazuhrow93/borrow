using Borrow.Data.Repositories.Interfaces;
using Borrow.Models.Backend;
using Microsoft.EntityFrameworkCore;

namespace Borrow.Data.Repositories.Implementations
{
    public class NeighborhoodRepository : IRepository<Neighborhood>
    {
        private DbContext Db;
        public IQueryable<Neighborhood> Query
        {
            get { return Db.Set<Neighborhood>().AsQueryable(); }
        }

        public NeighborhoodRepository(DbContext db)
        {
            this.Db = db;
        }

        public void Add(Neighborhood entity)
        {
            Db.Add(entity);
        }

        public void Add(List<Neighborhood> entity)
        {
            Db.AddRange(entity);
        }

        public void Delete(Neighborhood entity)
        {
            Db.Remove(entity);
        }

        public List<Neighborhood> FetchAll()
        {
            return Query.ToList();
        }

        public Neighborhood GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            Db.SaveChanges();
        }
    }
}
