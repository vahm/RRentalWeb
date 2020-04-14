using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using RRental.Api.Interfaces;
using CsvHelper;

namespace RRental.Api.Models
{
    public class InventoryRepository : IInventoryRepository
    {
        private List<Inventory> items = new List<Inventory>();

        public IEnumerable<Inventory> GetAllInventoryItems()
        {
            return items;
        }

        public Inventory this[Guid id] => items.SingleOrDefault(x => x.Id == id);

        public Inventory AddItem(Inventory item)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(Inventory));
            }

            item.Id = Guid.NewGuid();
            items.Add(item);
            return item;
        }

        public void DeleteItem(Guid id)
        {
            var item = items.SingleOrDefault(v => v.Id == id) ?? throw new ArgumentNullException(nameof(Inventory));
            items.Remove(item);
        }
        public Inventory UpdateInventory(Inventory item) => AddItem(item);
    }
}