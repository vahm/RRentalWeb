using System;

namespace RRental.Api.Models
{
    public class Inventory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
    }
}