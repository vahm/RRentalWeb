﻿using System;
using System.ComponentModel.DataAnnotations;
using RRental.Web.Api.Entities;

namespace RRental.Web.Api.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public string CartNo { get; set; }
        public Guid ItemId { get; set; }
        public int Duration { get; set; }
        public DateTime DateCreated { get; set; }
        public Customer Customer { get; set; }
    }
}