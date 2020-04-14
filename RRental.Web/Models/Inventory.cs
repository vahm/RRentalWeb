using System;

namespace RRental.Web.Models
{
    public class Inventory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public EquipmentType EquipmentType { get; set; }
    }
}
