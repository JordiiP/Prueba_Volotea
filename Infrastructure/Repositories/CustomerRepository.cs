using Domain.Entites;
using Domain.Interfaces;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly ShoppingStoreDbContext _shoppingStoreDbContext;
        public CustomerRepository(ShoppingStoreDbContext shoppingStoreDbContext)
        {
            _shoppingStoreDbContext = shoppingStoreDbContext;
        }

        public Customer Add(Customer entity)
        {
            return _shoppingStoreDbContext.Customer.Add(entity).Entity;
        }

        public void Delete(Customer entity)
        {
            _shoppingStoreDbContext.Customer.Remove(entity);
        }

        public Customer Get(int id)
        {
            return _shoppingStoreDbContext.Customer.FirstOrDefault(x => x.CustomerId == id);
        }

        public IList<Customer> GetAll()
        {
            return _shoppingStoreDbContext.Customer.ToList();
        }
    }
}
