using System.Data.Entity;
using RRental.Web.Api.Entities;
using RRental.Web.Api.Models;

namespace RRental.Web.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=RRentalWebApiDBConnectionString")
        {
            Database.SetInitializer(new ApplicationDbInitializer());
        }

        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Customer> Customer { get; set; }
    }
}