using Borrow.Data.Repositories.Interfaces;
using Borrow.Models.Backend;
using Microsoft.EntityFrameworkCore;

namespace Borrow.Data.Repositories.Implementations
{
    public class NeighborhoodRepository : IRepository<Neighborhood>
    {
        private BorrowContext Db { get; set; }
        public IQueryable<Neighborhood> Query
        {
            get { return Db.Set<Neighborhood>().AsQueryable(); }
        }

        public NeighborhoodRepository(BorrowContext db)
        {
            this.Db = db;
        }

        public Neighborhood Add(Neighborhood entity)
        {
            return (Db.Add(entity)).Entity;
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
            return Query.First(n=>n.Id == id);
        }

        public void Save()
        {
            Db.SaveChanges();
        }
    }
}
