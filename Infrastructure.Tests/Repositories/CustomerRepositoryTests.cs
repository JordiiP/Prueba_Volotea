using Domain.Entites;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Tests.Repositories
{
    public class CustomerRepositoryTests
    {
        public class ProductRespositoriyTests
        {
            private IRepository<Customer> _customerRepository;

            [SetUp]
            public void SetUp()
            {

            }

            [Test]
            public void Add_WhenHasData_ThenCustomerAdded()
            {
                using (var myContext = new ShoppingStoreDbContext(new DbContextOptionsBuilder<ShoppingStoreDbContext>().UseInMemoryDatabase("myDatabase").Options))
                {
                    //Arrange
                    var customer = new Customer()
                    {
                        CustomerId=1,
                        Dni="47167040C",
                        Name = "Jordi",
                        LastName= "Pablo"
                    };
                    _customerRepository = new CustomerRepository (myContext);

                    //Act
                    var result = _customerRepository.Add(customer);
                    myContext.SaveChanges();

                    //Assert
                    result = myContext.Customer.First(x => x.CustomerId == customer.CustomerId);
                    Assert.IsNotNull(result);
                    Assert.AreEqual(customer.CustomerId, result.CustomerId);
                    Assert.AreEqual(customer.Name, result.Name);
                    Assert.AreEqual(customer.LastName, result.LastName);
                    Assert.AreEqual(customer.Dni, result.Dni);
                }
            }

            [Test]
            public void Delete_WhenHasData_ThenCustomerDeleted()
            {
                using (var myContext = new ShoppingStoreDbContext(new DbContextOptionsBuilder<ShoppingStoreDbContext>().UseInMemoryDatabase("myDatabase").Options))
                {
                    //Arrange
                    var customerId = 1;
                    var customer = new Customer()
                    {
                        CustomerId = 1,
                        Dni = "47167040C",
                        Name = "Jordi",
                        LastName = "Pablo"
                    };
                    _customerRepository = new CustomerRepository(myContext);
                    var result = _customerRepository.Add(customer);
                    myContext.SaveChanges();
                    var currentCustomer = myContext.Customer.FirstOrDefault(x => x.CustomerId == customerId);

                    //Act
                    _customerRepository.Delete(currentCustomer);
                    myContext.SaveChanges();

                    //Assert
                    CollectionAssert.DoesNotContain(_customerRepository.GetAll(), currentCustomer);
                }
            }

            [Test]
            public void Get_WhenIdExist_ThenGetProduct()
            {
                using (var myContext = new ShoppingStoreDbContext(new DbContextOptionsBuilder<ShoppingStoreDbContext>().UseInMemoryDatabase("myDatabase").Options))
                {
                    //Arrange
                    var customerId = 1;
                    var customer = new Customer()
                    {
                        CustomerId = 1,
                        Dni = "47167040C",
                        Name = "Jordi",
                        LastName = "Pablo"
                    };
                    _customerRepository = new CustomerRepository(myContext); // Llamamos a esto que nos tendra todos los articulos de la base de 
                    var result = _customerRepository.Add(customer);
                    myContext.SaveChanges();
                    var currentCustomer = myContext.Customer.FirstOrDefault(x => x.CustomerId == customerId);

                    //Act
                    result = _customerRepository.Get(currentCustomer.CustomerId);

                    //Assert
                    Assert.IsNotNull(result);
                    Assert.AreEqual(currentCustomer.CustomerId, result.CustomerId);
                }
            }

            [Test]
            public void Get_WhenIdNotExist_ThenReturnNull()
            {
                using (var myContext = new ShoppingStoreDbContext(new DbContextOptionsBuilder<ShoppingStoreDbContext>().UseInMemoryDatabase("myDatabase").Options))
                {
                    //Arrange
                    var customerId = 1;
                    var customer = new Customer()
                    {
                        CustomerId = 5,
                        Dni = "47167040C",
                        Name = "Jordi",
                        LastName = "Pablo"
                    };
                    _customerRepository = new CustomerRepository(myContext); // Llamamos a esto que nos tendra todos los articulos de la base de 
                    var result = _customerRepository.Add(customer);
                    myContext.SaveChanges();

                    //Act
                    result = _customerRepository.Get(customerId);

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
                    var customer = new Customer()
                    {
                        CustomerId = 1,
                        Dni = "47167040C",
                        Name = "Jordi",
                        LastName = "Pablo"
                    };
                    _customerRepository = new CustomerRepository(myContext);

                    //act
                    _customerRepository.Add(customer);
                    myContext.SaveChanges();
                    var currentCustomer = _customerRepository.GetAll();

                    //Assert
                    Assert.IsNotEmpty(currentCustomer);
                }
            }

            [Test]
            public void Get_WhenProductNoExists_ThenReturnNull()
            {
                using (var myContext = new ShoppingStoreDbContext(new DbContextOptionsBuilder<ShoppingStoreDbContext>().UseInMemoryDatabase("myDatabase").Options))
                {
                    //arrange
                    //act
                    _customerRepository = new CustomerRepository(myContext);
                    var currentCustomer = _customerRepository.GetAll();

                    //Assert
                    Assert.IsEmpty(currentCustomer);
                }
            }
        }
    }
}
