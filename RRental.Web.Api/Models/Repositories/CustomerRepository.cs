using System;
using System.Collections.Generic;
using System.Linq;
using RRental.Web.Api.Data;
using RRental.Web.Api.Entities;
using RRental.Web.Api.Interfaces;

namespace RRental.Web.Api.Models.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public void Add(Customer customer)
        {
            context.Customer.Add(customer);
            context.SaveChanges();

        }

        public void Edit(Customer customer)
        {
            context.Entry(customer).State = System.Data.Entity.EntityState.Modified;
        }

        public void Remove(Guid id)
        {
            Customer customer = context.Customer.Find(id);
            context.Customer.Remove(customer);
            context.SaveChanges();
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return context.Customer;
        }

        public Customer FindById(Guid id)
        {
            var item = (from r in context.Customer where r.Id == id select r).FirstOrDefault();
            return item;
        }
    }
}