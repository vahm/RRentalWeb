using System;
using System.ComponentModel.DataAnnotations;

namespace RRental.Api.Models
{
    public class OrderModel
    {
        [Key]
        public Guid Id { get; set; }
        public string CartNo { get; set; }
        public Guid ItemId { get; set; }
        public int Duration { get; set; }
        public DateTime DateCreated { get; set; }
    }
}