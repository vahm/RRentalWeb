using System;

namespace RRental.Web.Api.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public int BonusPoints { get; set; }
    }
}