using Borrow.Data.Repositories.Interfaces;
using Borrow.Models.Backend;
using Microsoft.EntityFrameworkCore;

namespace Borrow.Data.Repositories.Implementations
{
    public class ListingRepository : IRepository<Listing>
    {
        private DbContext Db { get; set; }
        public IQueryable<Listing> Query => throw new NotImplementedException();

        public void Add(Listing entity)
        {
            Db.Add(entity);
        }

        public void Add(List<Listing> entity)
        {
            Db.AddRange(entity);
        }

        public void Delete(Listing entity)
        {
            Db.Remove(entity);
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
