using Domain.Entites;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NMock;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Tests.Repositories
{
    public class ProductRespositoriyTests
    {
        private IRepository<Product> _productRespositorySut;

        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void Add_WhenHasData_ThenProductAdded()
        {
            using (var myContext = new ShoppingStoreDbContext(new DbContextOptionsBuilder<ShoppingStoreDbContext>().UseInMemoryDatabase("myDatabase").Options))
            {
                //Arrange
                var product = new Product()
                {
                    ProductId = 1,
                    UnitPrice = 3.75M,
                    ProductTypeId = 1

                };
                _productRespositorySut = new ProductRepository(myContext);

                //Act
                var result = _productRespositorySut.Add(product);
                myContext.SaveChanges();

                //Assert
                result = myContext.Product.First(x => x.ProductId == product.ProductId);
                Assert.IsNotNull(result);
                Assert.AreEqual(product.ProductId, result.ProductId);
                Assert.AreEqual(product.ProductTypeId, result.ProductTypeId);
                Assert.AreEqual(product.UnitPrice, result.UnitPrice);
            }
        }

        [Test]
        public void Delete_WhenHasData_ThenProductDeleted()
        {
            using (var myContext = new ShoppingStoreDbContext(new DbContextOptionsBuilder<ShoppingStoreDbContext>().UseInMemoryDatabase("myDatabase").Options))
            {
                //Arrange
                var productId = 1;
                var product = new Product()
                {
                    ProductId = 1,
                    ProductTypeId = 1,
                    UnitPrice=10.5M
                };
                _productRespositorySut = new ProductRepository(myContext); 
                var result = _productRespositorySut.Add(product);
                myContext.SaveChanges();
                var currentProduct = myContext.Product.FirstOrDefault(x => x.ProductId == productId);

                //Act
                _productRespositorySut.Delete(currentProduct);
                myContext.SaveChanges();

                //Assert
                CollectionAssert.DoesNotContain(_productRespositorySut.GetAll(), currentProduct);
            }
        }

        [Test]
        public void Get_WhenIdExist_ThenGetProduct()
        {
            using (var myContext = new ShoppingStoreDbContext(new DbContextOptionsBuilder<ShoppingStoreDbContext>().UseInMemoryDatabase("myDatabase").Options))
            {
                //Arrange
                var productId = 1;
                var product = new Product()
                {
                    ProductId = 1,
                    ProductTypeId = 1,
                    UnitPrice = 10.5M
                };
                _productRespositorySut = new ProductRepository(myContext); // Llamamos a esto que nos tendra todos los articulos de la base de 
                var result = _productRespositorySut.Add(product);
                myContext.SaveChanges();
                var currentProduct = myContext.Product.FirstOrDefault(x => x.ProductId == productId);

                //Act
                result = _productRespositorySut.Get(currentProduct.ProductId);

                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(currentProduct.ProductId, result.ProductId);
            }
        }
        [Test]
        public void Get_WhenIdNotExist_ThenReturnNull ()
        {
            using (var myContext = new ShoppingStoreDbContext(new DbContextOptionsBuilder<ShoppingStoreDbContext>().UseInMemoryDatabase("myDatabase").Options))
            {
                //Arrange
                var productId = 1;
                var product = new Product()
                {
                    ProductId = 5,
                    ProductTypeId = 1,
                    UnitPrice = 10.5M
                };
                _productRespositorySut = new ProductRepository(myContext); // Llamamos a esto que nos tendra todos los articulos de la base de 
                var result = _productRespositorySut.Add(product);
                myContext.SaveChanges();

                //Act
                result = _productRespositorySut.Get(productId);

                //Assert
                Assert.IsNull(result);
            }
        }

        [Test]
        public void GetAll_WhenOneProductExist_ThenGetAllProduct()
        {
            using (var myContext = new ShoppingStoreDbContext(new DbContextOptionsBuilder<ShoppingStoreDbContext>().UseInMemoryDatabase("myDatabase").Options))
            {
                //arrange
                var product = new Product()
                {
                    ProductId = 1,
                    ProductTypeId = 1,
                    UnitPrice = 10.5M
                };
                _productRespositorySut = new ProductRepository(myContext);

                //act
                _productRespositorySut.Add(product);
                myContext.SaveChanges();
                var currentProduct = _productRespositorySut.GetAll();

                //Assert
                Assert.IsNotEmpty(currentProduct);
            }
        }

        [Test]
        public void Get_WhenProductNoExists_ThenReturnNull()
        {
            using (var myContext = new ShoppingStoreDbContext(new DbContextOptionsBuilder<ShoppingStoreDbContext>().UseInMemoryDatabase("myDatabase").Options))
            {
                //arrange
                //act
                _productRespositorySut = new ProductRepository(myContext);
                var currentProduct = _productRespositorySut.GetAll();

                //Assert
                Assert.IsEmpty(currentProduct);
            }
        }
    }
}
