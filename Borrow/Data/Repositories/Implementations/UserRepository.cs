using Borrow.Data.Repositories.Interfaces;
using Borrow.Models.Backend;
using Microsoft.EntityFrameworkCore;

namespace Borrow.Data.Repositories.Implementations
{
    public class UserRepository : IRepository<User>
    {
        private BorrowContext Db { get; set; }

        public IQueryable<User> Query
        {
            get { return Db.Set<User>().AsQueryable(); }
        }

        public UserRepository(BorrowContext db)
        {
            this.Db = db;
        }

        public User Add(User entity)
        {
            return (Db.Add(entity)).Entity;
        }

        public void Add(List<User> entity)
        {
            Db.AddRange(entity);
        }

        public void Delete(User entity)
        {
            Db.Remove(entity);
        }

        public List<User> FetchAll()
        {
            return Query.ToList();
        }

        public User GetById(int id)
        {
            return Query.First(u => u.Id == id.ToString());
        }

        public void Save()
        {
            Db.SaveChanges();
        }
    }
}
