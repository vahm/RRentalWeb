using System;
using System.Collections.Generic;
using RRental.Web.Api.Entities;

namespace RRental.Web.Api.Interfaces
{
    public interface IInventoryRepository
    {
        void Add(Inventory item);
        void Edit(Inventory item);
        void Remove(Guid idd);
        IEnumerable<Inventory> GetItems();
        Inventory FindById(Guid id);
    }
}
