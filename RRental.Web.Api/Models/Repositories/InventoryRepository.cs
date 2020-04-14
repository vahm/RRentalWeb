using System;
using System.Collections.Generic;
using System.Linq;
using RRental.Web.Api.Data;
using RRental.Web.Api.Entities;
using RRental.Web.Api.Interfaces;

namespace RRental.Web.Api.Models.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public void Add(Inventory item)
        {
            context.Inventory.Add(item);
            context.SaveChanges();

        }

        public void Edit(Inventory item)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public void Remove(Guid id)
        {
            Inventory item = context.Inventory.Find(id);
            context.Inventory.Remove(item);
            context.SaveChanges();
        }

        public IEnumerable<Inventory> GetItems()
        {
            return context.Inventory;
        }

        public Inventory FindById(Guid id)
        {
            var item = (from r in context.Inventory where r.Id == id select r).FirstOrDefault();
            return item;
        }
    }
}