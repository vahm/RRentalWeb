using System;

namespace RRental.Web.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string CartNo { get; set; }
        public Guid ItemId { get; set; }
        public int Duration { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
