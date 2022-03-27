using Domain.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entites;
using ShoppingStoreApiExam.V1.Controllers.Buys.Services.Interfaces;
using ShoppingStoreApiExam.V1.Controllers.Buys.Services;
using ShoppingStoreApiExam.V1.Controllers.Buys.BuyRequests;

namespace ShoppingStoreApiExam.Test.V1.Controllers.Buys.Services
{
    class BuyServiceTests
    {

        private Mock<IRepository<Buy>> _buyRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;

        private IBuyService _buyServiceSut;

        [SetUp]
        public void SetUp()
        {
            _buyRepositoryMock = new Mock<IRepository<Buy>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _buyServiceSut = new BuyService(_buyRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Test]
        public void Submit_WhenBuyRequestHasCorrectInfo_ThenSubmitSuccess()
        {
            //Arrange
            var buyRquest = new BuyRequest()
            {
                CustomerId = 5,
                ProductId = 1,
                Quantity = 10
            };
            var buy = new Buy()
            {
                BuyId = 1,
                CustomerId = 5,
                ProductId=1,
                Quantity=10,
                TotalPrice=5,
            };
            _buyRepositoryMock.Setup(x => x.Add(It.IsAny<Buy>())).Returns(buy);

            //Act
            var result = _buyServiceSut.Submit(buyRquest);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(buy.BuyId, result.BuyId);
        }

        [Test]
        public void Submit_WhenExceptionSavingChanges_ThenThrowException()
        {
            //Arrange
            var buyRquest = new BuyRequest()
            {
                CustomerId = 5,
                ProductId = 1,
                Quantity = 10
            };
            var buy = new Buy()
            {
                BuyId = 1,
                CustomerId = 5,
                ProductId = 1,
                Quantity = 10,
                TotalPrice = 5,
            };
            _buyRepositoryMock.Setup(x => x.Add(It.IsAny<Buy>())).Returns(buy);
            _unitOfWorkMock.Setup(x => x.SaveChanges()).Throws(new Exception());

            //Act & Assert
            Assert.Throws<Exception>(() => _buyServiceSut.Submit(buyRquest));
        }

        [Test]
        public void GetAll_WhenData_ThenGetAllSucces()
        {
            //Arrange
            var list = new List<Buy>() {
                new Buy()
                {
                    BuyId = 1,
                    CustomerId = 5,
                    ProductId = 1,
                    Quantity = 10,
                    TotalPrice = 5,
                }
            };
            _buyRepositoryMock.Setup(x => x.GetAll()).Returns(list);

            //Act
            var result = _buyServiceSut.GetAll();

            //Assert
            Assert.IsNotEmpty(result);
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetAll_WhenNoData_ThenGetAllSucces()
        {
            //Arrange
            var list = new List<Buy>();

            //Act
            _buyRepositoryMock.Setup(x => x.GetAll()).Returns(list);
            var result = _buyServiceSut.GetAll();

            //Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetAll_WhenExceptionSavingChanges_ThenThrowException()
        {
            _buyRepositoryMock.Setup(x => x.GetAll()).Throws(new Exception());
            Assert.Throws<Exception>(() => _buyServiceSut.GetAll());
        }

        [Test]
        public void Get_WhenCorrectId_ThenGetSucces()
        {
            //Arrange
            var buyId = 1;

            //Arrange
           
            var buy = new Buy()
            {
                BuyId = buyId,
                CustomerId = 5,
                ProductId = 1,
                Quantity = 10,
                TotalPrice = 5,
            };

            //Act
            _buyRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(buy);
            var result = _buyServiceSut.GetBuyById(buyId);

            //Assert
            Assert.AreEqual(result.BuyId, buy.BuyId);

        }
        [Test]
        public void Get_WhenIdNotFound_ThenGetBuyWrong()
        {
            //Arrange
            var buyId = 1;
            var buy = new Buy()
            {
                BuyId = 3,
                CustomerId = 5,
                ProductId = 1,
                Quantity = 10,
                TotalPrice = 5,
            };

            //Act
            _buyRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(buy);
            var result = _buyServiceSut.GetBuyById(buyId);

            //Assert
            Assert.AreNotEqual(result.BuyId, buyId);

        }
        [Test]
        public void Remove_WhenHasData_ThenRemoveSuccess()
        {
            //Arrange
            var buyId = 1;
            var buy = new Buy()
            {
                BuyId = 1,
                CustomerId = 5,
                ProductId = 1,
                Quantity = 10,
                TotalPrice = 5,
            };
            _buyRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(buy);
            _buyRepositoryMock.Setup(x => x.Delete(It.IsAny<Buy>()));

            //Act
            var result = _buyServiceSut.Remove(buyId);

            //Assert
            Assert.AreEqual(true, result);
        }
    }
}
