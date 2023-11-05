using Borrow.Data.Repositories.Interfaces;
using Borrow.Models.Backend;
using Microsoft.EntityFrameworkCore;

namespace Borrow.Data.Repositories.Implementations
{
    public class AppProfileRepository : IRepository<AppProfile>
    {
        public DbContext Db { get; set; }
        public IQueryable<AppProfile> Query
        {
            get { return Db.Set<AppProfile>().AsQueryable(); }
        }

        public AppProfileRepository(DbContext db)
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

        public void Add(AppProfile entity)
        {
            Db.Add(entity);
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
