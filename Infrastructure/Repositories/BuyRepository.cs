using Domain.Entites;
using Domain.Interfaces;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Repositories
{
    public class BuyRepository : IRepository<Buy>
    {
        private readonly ShoppingStoreDbContext _shoppingStoreDbContext;
        public BuyRepository(ShoppingStoreDbContext shoppingStoreDbContext)
        {
            _shoppingStoreDbContext = shoppingStoreDbContext;
        }

        public Buy Add(Buy entity)
        {
            return _shoppingStoreDbContext.Buy.Add(entity).Entity;
        }

        public void Delete(Buy entity)
        {
            _shoppingStoreDbContext.Buy.Remove(entity);
        }

        public Buy Get(int id)
        {
            return _shoppingStoreDbContext.Buy.FirstOrDefault(x => x.BuyId == id);
        }

        public IList<Buy> GetAll()
        {
            return _shoppingStoreDbContext.Buy.ToList();
        }
    }
}
