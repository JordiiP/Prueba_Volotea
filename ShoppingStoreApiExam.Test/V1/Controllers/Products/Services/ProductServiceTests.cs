using Domain.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entites;
using ShoppingStoreApiExam.V1.Controllers.Products.Services.Interfaces;
using ShoppingStoreApiExam.V1.Controllers.Products.Services;
using ShoppingStoreApiExam.V1.Controllers.Products.ProductRequests;
using Domain.Enums;

namespace ShoppingStoreApiExam.Test.V1.Controllers.Products.Services
{
    class ProductServiceTests
    {
        private Mock<IRepository<Product>> _productRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;

        private IProductService _productServiceSut;

        [SetUp]
        public void SetUp()
        {
            _productRepositoryMock = new Mock<IRepository<Product>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _productServiceSut = new ProductService(_productRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Test]
        public void Submit_WhenProductRequestHasCorrectInfo_ThenSubmitSuccess()
        {
            //Arrange
            var productRquest = new ProductRequest()
            {
                ProductId = 1,
                ProductType = ProductTypeEnum.Manzanas,
                UnitPrice = 2
            };
            var product = new Product()
            {
                ProductId = 1,
                ProductTypeId = 5,
                UnitPrice = 1,
            };
            _productRepositoryMock.Setup(x => x.Add(It.IsAny<Product>())).Returns(product);

            //Act
            var result = _productServiceSut.Submit(productRquest);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(product.ProductId, result.ProductId);
        }

        [Test]
        public void Submit_WhenExceptionSavingChanges_ThenThrowException()
        {
            //Arrange
            var productRquest = new ProductRequest()
            {
                ProductId = 1,
                ProductType = ProductTypeEnum.Manzanas,
                UnitPrice = 2
            };
            var product = new Product()
            {
                ProductId = 1,
                ProductTypeId = 5,
                UnitPrice = 1,
            };
            _productRepositoryMock.Setup(x => x.Add(It.IsAny<Product>())).Returns(product);
            _unitOfWorkMock.Setup(x => x.SaveChanges()).Throws(new Exception());

            //Act & Assert
            Assert.Throws<Exception>(() => _productServiceSut.Submit(productRquest));
        }

        [Test]
        public void GetAll_WhenData_ThenGetAllSucces()
        {
            //Arrange
            var list = new List<Product>() {
                new Product()
                {
                    ProductId = 1,
                    ProductTypeId = 5,
                    UnitPrice = 1,
                }
            };
            _productRepositoryMock.Setup(x => x.GetAll()).Returns(list);

            //Act
            var result = _productServiceSut.GetAll();

            //Assert
            Assert.IsNotEmpty(result);
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetAll_WhenNoData_ThenGetAllSucces()
        {
            //Arrange
            var list = new List<Product>();

            //Act
            _productRepositoryMock.Setup(x => x.GetAll()).Returns(list);
            var result = _productServiceSut.GetAll();

            //Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetAll_WhenExceptionSavingChanges_ThenThrowException()
        {
            _productRepositoryMock.Setup(x => x.GetAll()).Throws(new Exception());
            Assert.Throws<Exception>(() => _productServiceSut.GetAll());
        }

        [Test]
        public void Get_WhenCorrectId_ThenGetSucces()
        {
            //Arrange
            var ProductId = 1;

            //Arrange

            var product = new Product()
            {
                ProductId = 1,
                ProductTypeId = 5,
                UnitPrice = 1,
            };

            //Act
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(product);
            var result = _productServiceSut.GetProductById(ProductId);

            //Assert
            Assert.AreEqual(result.ProductId, product.ProductId);

        }
        [Test]
        public void Get_WhenIdNotFound_ThenGetProductWrong()
        {
            //Arrange
            var ProductId = 5;
            var product = new Product()
            {
                ProductId = 1,
                ProductTypeId = 5,
                UnitPrice = 1,
            };

            //Act
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(product);
            var result = _productServiceSut.GetProductById(ProductId);

            //Assert
            Assert.AreNotEqual(result.ProductId, ProductId);

        }
        [Test]
        public void Remove_WhenHasData_ThenRemoveSuccess()
        {
            //Arrange
            var ProductId = 1;
            var product = new Product()
            {
                ProductId = 1,
                ProductTypeId = 5,
                UnitPrice = 1,
            };
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(product);
            _productRepositoryMock.Setup(x => x.Delete(It.IsAny<Product>()));

            //Act
            var result = _productServiceSut.Remove(ProductId);

            //Assert
            Assert.AreEqual(true, result);
        }
    }
}
