using Borrow.Data.Repositories.Interfaces;
using Borrow.Models.Backend;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;

namespace Borrow.Data.Repositories.Implementations
{
    public class ListValueRepository : IRepository<ListValue>
    {
        private BorrowContext Db { get; set; }

        public ListValueRepository(BorrowContext db)
        {
            this.Db = db;
        }

        public IQueryable<ListValue> Query
        {
            get { return Db.Set<ListValue>().AsQueryable<ListValue>(); }
        }

        public ListValue Add(ListValue entity)
        {
            return (Db.Add(entity)).Entity;
        }

        public void Add(List<ListValue> entity)
        {
            Db.AddRange(entity);
        }

        public void Delete(ListValue entity)
        {
            Db.Remove(entity);
        }

        public void Delete(IEnumerable<ListValue> entities)
        {
            Db.RemoveRange(entities);
        }

        public List<ListValue> FetchAll()
        {
            return Query.ToList();
        }

        public ListValue GetById(int id) //different from the rest of entity. ok?
        {
            return Query.First(e => e.Value == id);
        }

        public ListValue GetByName(string name)
        {
            return Query.First(e => e.Name == name);
        }

        public void Save()
        {
            Db.SaveChanges();
        }
    }
}
