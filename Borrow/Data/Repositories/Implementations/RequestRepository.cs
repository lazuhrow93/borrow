using Borrow.Data.Repositories.Interfaces;
using Borrow.Models.Backend;
using Microsoft.EntityFrameworkCore;

namespace Borrow.Data.Repositories.Implementations
{
    public class RequestRepository : IRepository<Request>
    {
        private BorrowContext Db { get; set; }

        public IQueryable<Request> Query
        {
            get { return Db.Set<Request>().AsQueryable(); }
        }
        
        public RequestRepository(BorrowContext db)
        {
            this.Db = db;
        }

        public Request Add(Request entity)
        {
            return (Db.Add(entity)).Entity;
        }

        public void Add(List<Request> entity)
        {
            Db.AddRange(entity);
        }

        public void Delete(Request entity)
        {
            Db.Remove(entity);
        }

        public List<Request> FetchAll()
        {
            return Query.ToList();
        }

        public Request GetById(int id)
        {
            return Query.First(r => r.Id == id);
        }

        public void Save()
        {
            Db.SaveChanges();
        }
    }
}
