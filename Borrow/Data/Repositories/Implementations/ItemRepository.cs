using Borrow.Data.Repositories.Interfaces;
using Borrow.Models.Backend;
using Microsoft.EntityFrameworkCore;

namespace Borrow.Data.Repositories.Implementations
{
    public class ItemRepository : IRepository<Item>
    {
        public BorrowContext Db { get; set; }

        public IQueryable<Item> Query
        {
            get { return Db.Set<Item>().AsQueryable<Item>(); }
        }

        public ItemRepository(BorrowContext db)
        {
            this.Db = db;
        }

        public Item GetById(int id)
        {
            return Query.First(i=>i.Id== id);
        }

        public List<Item> FetchAll()
        {
            return Query.ToList();
        }

        public Item Add(Item entity)
        {
            return (Db.Add(entity)).Entity;
        }

        public void Add(List<Item> entity)
        {
            Db.AddRange(entity);
        }

        public void Delete(Item entity)
        {
            Db.Remove(entity);
        }

        public void Save()
        {
            Db.SaveChanges();
        }
    }
}
