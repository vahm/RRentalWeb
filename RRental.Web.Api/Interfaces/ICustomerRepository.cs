using System;
using System.Collections.Generic;
using RRental.Web.Api.Entities;

namespace RRental.Web.Api.Interfaces
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        void Edit(Customer customer);
        void Remove(Guid id);
        IEnumerable<Customer> GetCustomers();
        Customer FindById(Guid id);
    }
}
