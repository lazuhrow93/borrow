using Borrow.Data.Repositories.Interfaces;
using Borrow.Models.Backend;
using Microsoft.EntityFrameworkCore;

namespace Borrow.Data.Repositories.Implementations
{
    public class AppProfileRepository : IRepository<AppProfile>
    {
        public BorrowContext Db { get; set; }

        public IQueryable<AppProfile> Query
        {
            get { return Db.Set<AppProfile>().AsQueryable(); }
        }

        public AppProfileRepository(BorrowContext db)
        {
            this.Db = db;
        }

        public List<AppProfile> FetchAll()
        {
            return Query.ToList();
        }

        public AppProfile GetById(int id)
        {
            return Query.First(a => a.Id == id);
        }

        public AppProfile Add(AppProfile entity)
        {
            return (Db.Add(entity)).Entity;
        }

        public void Add(List<AppProfile> entity)
        {
            Db.AddRange(entity);
        }

        public void Delete(AppProfile entity)
        {
            Db.Remove(entity);
        }

        public void Save()
        {
            Db.SaveChanges();
        }
    }
}
