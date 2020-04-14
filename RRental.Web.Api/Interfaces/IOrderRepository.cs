using System;
using System.Collections.Generic;
using RRental.Web.Api.Models;

namespace RRental.Web.Api.Interfaces
{
    public interface IOrderRepository
    {
        void Add(Order order);
        void Edit(Order order);
        void Remove(Guid id);
        IEnumerable<Order> GetOrders();
        Order FindById(Guid id);
    }
}
