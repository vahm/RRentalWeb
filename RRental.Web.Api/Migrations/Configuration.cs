using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using CsvHelper;
using RRental.Web.Api.Entities;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace RRental.Web.Api.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.ApplicationDbContext context)
        {
            if (!context.Inventory.Any())
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = "RRental.Web.Api.Data.SeedData.InventoryData.csv";
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                        csv.Configuration.HeaderValidated = null;
                        csv.Configuration.MissingFieldFound = null;
                        var items = csv.GetRecords<Inventory>().ToArray();
                        foreach (Inventory item in items)
                        {
                            item.Id = Guid.NewGuid();
                            context.Inventory.Add(item);
                        }
                    }
                }
            }
            var customer = new Customer();
            customer.CustomerName = "Default Customer";
            customer.Id = Guid.NewGuid();
            context.Customer.Add(customer);

            context.SaveChanges();
        }
    }
}
