using Borrow.Data.Repositories.Interfaces;
using Borrow.Models.Backend;
using Microsoft.EntityFrameworkCore;

namespace Borrow.Data.Repositories.Implementations
{
    public class ListingRepository : IRepository<Listing>
    {
        private BorrowContext Db { get; set; }
        public IQueryable<Listing> Query
        {
            get { return Db.Set<Listing>().AsQueryable<Listing>(); }
        }

        public ListingRepository(BorrowContext db)
        {
            this.Db = db;
        }

        public Listing Add(Listing entity)
        {
            return (Db.Add(entity)).Entity;
        }

        public void Add(List<Listing> entity)
        {
            Db.AddRange(entity);
        }

        public void Delete(Listing entity)
        {
            Db.Remove(entity);
        }

        public void Delete(IEnumerable<Listing> entities)
        {
            Db.RemoveRange(entities);
        }

        public List<Listing> FetchAll()
        {
            return Query.ToList();
        }

        public Listing GetById(int id)
        {
            return Query.First(l => l.Id == id);
        }

        public void Save()
        {
            Db.SaveChanges();
        }
    }
}
