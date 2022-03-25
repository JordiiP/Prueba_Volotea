using Domain.Interfaces;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShoppingStoreDbContext _shoppingStoreDbContext;

        public UnitOfWork(ShoppingStoreDbContext shoppingStoreDbContext)
        {
            _shoppingStoreDbContext = shoppingStoreDbContext;
        }
        public int SaveChanges()
        {
            return _shoppingStoreDbContext.SaveChanges();
        }
    }
}
