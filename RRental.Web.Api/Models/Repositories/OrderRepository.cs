using System;
using System.Collections.Generic;
using System.Linq;
using RRental.Web.Api.Data;
using RRental.Web.Api.Interfaces;

namespace RRental.Web.Api.Models.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public void Add(Order order)
        {
            context.Order.Add(order);
            context.SaveChanges();

        }

        public void Edit(Order order)
        {
            context.Entry(order).State = System.Data.Entity.EntityState.Modified;
        }

        public void Remove(Guid id)
        {
            Order item = context.Order.Find(id);
            context.Order.Remove(item);
            context.SaveChanges();
        }

        public IEnumerable<Order> GetOrders()
        {
            return context.Order;
        }

        public Order FindById(Guid id)
        {
            var item = (from r in context.Order where r.Id == id select r).FirstOrDefault();
            return item;
        }
    }
}