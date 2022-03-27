using Domain.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entites;
using ShoppingStoreApiExam.V1.Controllers.Customers.Service;
using ShoppingStoreApiExam.V1.Controllers.Customers.Service.Interfaces;
using ShoppingStoreApiExam.V1.Controllers.Customers.CustomerRequests;

namespace ShoppingStoreApiExam.Test.V1.Controllers.Customers.Services
{
    class CustomerServiceTests
    {
        private Mock<IRepository<Customer>> _customerRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;

        private ICustomerService _customerServiceSut;

        [SetUp]
        public void SetUp()
        {
            _customerRepositoryMock = new Mock<IRepository<Customer>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _customerServiceSut = new CustomerService(_customerRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Test]
        public void Submit_WhenCustomerRequestHasCorrectInfo_ThenSubmitSuccess()
        {
            //Arrange
            var customerRequest = new CustomerRequest()
            {
                Name = "Martha",
                LastName = "Pablo",
                Dni = "77854205V"
            };
            var customer= new Customer()
            {
                CustomerId = 1,
                Name = "Martha",
                LastName = "Pablo",
                Dni = "77854205V"

            };
            _customerRepositoryMock.Setup(x => x.Add(It.IsAny<Customer>())).Returns(customer);

            //Act
            var result = _customerServiceSut.Submit(customerRequest);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(customer.CustomerId, result.CustomerId);
        }

        [Test]
        public void Submit_WhenExceptionSavingChanges_ThenThrowException()
        {
            //Arrange
            var customerRequest = new CustomerRequest()
            {
                Name = "Martha",
                LastName = "Pablo",
                Dni = "77854205V"
            };
            var customer= new Customer()
            {
                CustomerId = 1,
                Name = "Martha",
                LastName = "Pablo",
                Dni = "77854205V"
            };
            _customerRepositoryMock.Setup(x => x.Add(It.IsAny<Customer>())).Returns(customer);
            _unitOfWorkMock.Setup(x => x.SaveChanges()).Throws(new Exception());

            //Act & Assert
            Assert.Throws<Exception>(() => _customerServiceSut.Submit(customerRequest));
        }

        [Test]
        public void GetAll_WhenData_ThenGetAllSucces()
        {
            //Arrange
            var list = new List<Customer>() {
                new Customer()
                {
                    CustomerId = 1,
                    Name = "Martha",
                    LastName = "Pablo",
                    Dni = "77854205V"
                }
            };
            _customerRepositoryMock.Setup(x => x.GetAll()).Returns(list);

            //Act
            var result = _customerServiceSut.GetAll();

            //Assert
            Assert.IsNotEmpty(result);
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetAll_WhenNoData_ThenGetAllSucces()
        {
            //Arrange
            var list = new List<Customer>();

            //Act
            _customerRepositoryMock.Setup(x => x.GetAll()).Returns(list);
            var result = _customerServiceSut.GetAll();

            //Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetAll_WhenExceptionSavingChanges_ThenThrowException()
        {
            _customerRepositoryMock.Setup(x => x.GetAll()).Throws(new Exception());
            Assert.Throws<Exception>(() => _customerServiceSut.GetAll());
        }

        [Test]
        public void Get_WhenCorrectId_ThenGetSucces()
        {
            //Arrange
            var CustomerId = 1;

            //Arrange
            var customer= new Customer()
            {
                CustomerId = 1,
                Name = "Martha",
                LastName = "Pablo",
                Dni = "77854205V"
            };

            //Act
            _customerRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(customer);
            var result = _customerServiceSut.GetCustomerById(CustomerId);

            //Assert
            Assert.AreEqual(result.CustomerId, customer.CustomerId);

        }
        [Test]
        public void Get_WhenIdNotFound_ThenGetCustomerWrong()
        {
            //Arrange
            var CustomerId = 5;
            var customer= new Customer()
            {
                CustomerId = 1,
                Name = "Martha",
                LastName = "Pablo",
                Dni = "77854205V"
            };

            //Act
            _customerRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(customer);
            var result = _customerServiceSut.GetCustomerById(CustomerId);

            //Assert
            Assert.AreNotEqual(result.CustomerId, CustomerId);

        }
        [Test]
        public void Remove_WhenHasData_ThenRemoveSuccess()
        {
            //Arrange
            var CustomerId = 1;
            var customer= new Customer()
            {
                CustomerId = 1,
                Name = "Martha",
                LastName = "Pablo",
                Dni = "77854205V"
            };
            _customerRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(customer);
            _customerRepositoryMock.Setup(x => x.Delete(It.IsAny<Customer>()));

            //Act
            var result = _customerServiceSut.Remove(CustomerId);

            //Assert
            Assert.AreEqual(true, result);
        }
    }
}
