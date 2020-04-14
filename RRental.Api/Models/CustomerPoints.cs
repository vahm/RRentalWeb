using System;

namespace RRental.Api.Models
{
    public class CustomerPoints
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int Points { get; set; }
    }
}