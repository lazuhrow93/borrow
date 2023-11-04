﻿using Borrow.Data.Repositories.Interfaces;
using Borrow.Models.Backend;
using Microsoft.EntityFrameworkCore;

namespace Borrow.Data.Repositories.Implementations
{
    public class ItemRepository : IRepository<Item>
    {
        public BorrowContext Db { get; set; }

        public ItemRepository(BorrowContext db)
        {
            this.Db = db;
        }

        public IQueryable<Item> Query
        {
            get { return Db.Set<Item>().AsQueryable<Item>(); }
        }

        public Item GetById(int id)
        {
            return Query.First(i=>i.Id== id);
        }

        public List<Item> FetchAll()
        {
            return Query.ToList();
        }

        public void Add(Item entity)
        {
            Db.Add(entity);
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