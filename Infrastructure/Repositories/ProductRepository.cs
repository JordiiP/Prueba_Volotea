using Domain.Entites;
using Domain.Interfaces;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly ShoppingStoreDbContext _shoppingStoreDbContext;
        public ProductRepository(ShoppingStoreDbContext shoppingStoreDbContext)
        {
            _shoppingStoreDbContext = shoppingStoreDbContext;
        }

        public Product Add(Product entity)
        {
            return _shoppingStoreDbContext.Product.Add(entity).Entity;
        }

        public void Delete(Product entity)
        {
            _shoppingStoreDbContext.Product.Remove(entity);
        }

        public Product Get(int id)
        {
            return _shoppingStoreDbContext.Product.FirstOrDefault(x => x.ProductId == id);
        }

        public IList<Product> GetAll()
        {
            return _shoppingStoreDbContext.Product.ToList();
        }
    }
}
