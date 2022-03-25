using Domain.Entites;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace Infrastructure.Tests.Repositories
{
    class BuyRepositoryTests
    {
        public class ProductRespositoriyTests
        {
            private IRepository<Buy> _buyRespositorySut;

            [SetUp]
            public void SetUp()
            {

            }

            [Test]
            public void Add_WhenHasData_ThenBuyAdded()
            {
                using (var myContext = new ShoppingStoreDbContext(new DbContextOptionsBuilder<ShoppingStoreDbContext>().UseInMemoryDatabase("myDatabase").Options))
                {
                    //Arrange
                    var buy = new Buy()
                    {
                        BuyId=1,
                        Quantity=3,
                        ProductId=1
                    };
                    _buyRespositorySut = new BuyRepository(myContext);

                    //Act
                    var result = _buyRespositorySut.Add(buy);
                    myContext.SaveChanges();

                    //Assert
                    result = myContext.Buy.First(x => x.BuyId == buy.BuyId);
                    Assert.IsNotNull(result);
                    Assert.AreEqual(buy.BuyId, result.BuyId);
                    Assert.AreEqual(buy.Quantity, result.Quantity);
                    Assert.AreEqual(buy.ProductId, result.ProductId);
                }
            }

            [Test]
            public void Delete_WhenHasData_ThenBuyDeleted()
            {
                using (var myContext = new ShoppingStoreDbContext(new DbContextOptionsBuilder<ShoppingStoreDbContext>().UseInMemoryDatabase("myDatabase").Options))
                {
                    //Arrange
                    var buyId = 1;
                    var buy = new Buy()
                    {
                        BuyId = 1,
                        Quantity = 3,
                        ProductId = 1
                    };
                    _buyRespositorySut = new BuyRepository(myContext);
                    var result = _buyRespositorySut.Add(buy);
                    myContext.SaveChanges();
                    var currentBuy = myContext.Buy.FirstOrDefault(x => x.BuyId == buyId);

                    //Act
                    _buyRespositorySut.Delete(currentBuy);
                    myContext.SaveChanges();

                    //Assert
                    CollectionAssert.DoesNotContain(_buyRespositorySut.GetAll(), currentBuy);
                }
            }

            [Test]
            public void Get_WhenIdExist_ThenGetBuy()
            {
                using (var myContext = new ShoppingStoreDbContext(new DbContextOptionsBuilder<ShoppingStoreDbContext>().UseInMemoryDatabase("myDatabase").Options))
                {
                    //Arrange
                    var buyId = 1;
                    var buy = new Buy()
                    {
                        BuyId = 1,
                        Quantity = 3,
                        ProductId = 1
                    };
                    _buyRespositorySut = new BuyRepository(myContext); // Llamamos a esto que nos tendra todos los articulos de la base de 
                    var result = _buyRespositorySut.Add(buy);
                    myContext.SaveChanges();
                    var currentBuy = myContext.Buy.FirstOrDefault(x => x.BuyId== buyId);

                    //Act
                    result = _buyRespositorySut.Get(currentBuy.BuyId);

                    //Assert
                    Assert.IsNotNull(result);
                    Assert.AreEqual(currentBuy.BuyId, result.BuyId);
                }
            }

            [Test]
            public void Get_WhenIdNotExist_ThenReturnNull()
            {
                using (var myContext = new ShoppingStoreDbContext(new DbContextOptionsBuilder<ShoppingStoreDbContext>().UseInMemoryDatabase("myDatabase").Options))
                {
                    //Arrange
                    var buyId = 1;
                    var buy = new Buy()
                    {
                        BuyId = 5,
                        Quantity = 3,
                        ProductId = 1
                    };
                    _buyRespositorySut = new BuyRepository(myContext); // Llamamos a esto que nos tendra todos los articulos de la base de 
                    var result = _buyRespositorySut.Add(buy);
                    myContext.SaveChanges();

                    //Act
                    result = _buyRespositorySut.Get(buyId);

                    //Assert
                    Assert.IsNull(result);
                }
            }

            [Test]
            public void GetAll_WhenOneBuyExist_ThenGetAllProduct()
            {
                using (var myContext = new ShoppingStoreDbContext(new DbContextOptionsBuilder<ShoppingStoreDbContext>().UseInMemoryDatabase("myDatabase").Options))
                {
                    //arrange
                    var buyId = 1;
                    var buy = new Buy()
                    {
                        BuyId = 1,
                        Quantity = 3,
                        ProductId = 1
                    };
                    _buyRespositorySut = new BuyRepository(myContext);

                    //act
                    _buyRespositorySut.Add(buy);
                    myContext.SaveChanges();
                    var currentBuy = _buyRespositorySut.GetAll();

                    //Assert
                    Assert.IsNotEmpty(currentBuy);
                }
            }

            [Test]
            public void Get_WhenBuyNoExists_ThenReturnNull()
            {
                using (var myContext = new ShoppingStoreDbContext(new DbContextOptionsBuilder<ShoppingStoreDbContext>().UseInMemoryDatabase("myDatabase").Options))
                {
                    //arrange
                    //act
                    _buyRespositorySut =  new BuyRepository(myContext);
                    var currentBuy = _buyRespositorySut.GetAll();

                    //Assert
                    Assert.IsEmpty(currentBuy);
                }
            }
        }
    }
}

