using System;
using RRental.Web.Api.Models;

namespace RRental.Web.Api.Entities
{
    public class Inventory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public EquipmentType EquipmentType { get; set; }
    }
}