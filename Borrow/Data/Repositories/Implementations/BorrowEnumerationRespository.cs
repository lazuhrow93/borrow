using Borrow.Data.Repositories.Interfaces;
using Borrow.Models.Backend;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;

namespace Borrow.Data.Repositories.Implementations
{
    public class BorrowEnumerationRespositry : IRepository<BorrowEnumeration>
    {
        private BorrowContext Db { get; set; }

        public IQueryable<BorrowEnumeration> Query
        {
            get { return Db.Set<BorrowEnumeration>().AsQueryable<BorrowEnumeration>(); }
        }

        public BorrowEnumeration Add(BorrowEnumeration entity)
        {
            return (Db.Add(entity)).Entity;
        }

        public void Add(List<BorrowEnumeration> entity)
        {
            Db.AddRange(entity);
        }

        public void Delete(BorrowEnumeration entity)
        {
            Db.Remove(entity);
        }

        public List<BorrowEnumeration> FetchAll()
        {
            return Query.ToList();
        }

        public BorrowEnumeration GetById(int id)
        {
            return Query.First(e => e.Id == id);
        }

        public BorrowEnumeration GetByValue(int value)
        {
            return Query.First(e => e.Value == value);
        }

        public BorrowEnumeration GetByName(string name)
        {
            return Query.First(e => e.Name == name);
        }

        public void Save()
        {
            Db.SaveChanges();
        }
    }
}
