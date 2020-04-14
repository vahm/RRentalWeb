using System;
using System.Collections.Generic;
using RRental.Api.Models;

namespace RRental.Api.Interfaces
{
    public interface IInventoryRepository
    {
        IEnumerable<Inventory> GetAllInventoryItems();
        Inventory this[Guid id] { get; }
        Inventory AddItem(Inventory item);
        Inventory UpdateInventory(Inventory item);
        void DeleteItem(Guid id);
    }
}
