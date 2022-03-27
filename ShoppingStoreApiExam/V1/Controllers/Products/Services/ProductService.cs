using Domain.Entites;
using Domain.Interfaces;
using ShoppingStoreApiExam.V1.Controllers.Products.ProductRequests;
using ShoppingStoreApiExam.V1.Controllers.Products.ProductResponses;
using ShoppingStoreApiExam.V1.Controllers.Products.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingStoreApiExam.V1.Controllers.Products.Services
{
    public class ProductService :IProductService
    {

        private readonly IRepository<Product> _productRepository;

        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IRepository<Product> productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public ProductResponse Submit(ProductRequest productRequest)
        {
            try
            {
                var product = new Product()
                {
                    ProductTypeId=(short)productRequest.ProductType,
                    UnitPrice = productRequest.UnitPrice
                };
                product = _productRepository.Add(product);
                _unitOfWork.SaveChanges();
                return new ProductResponse(product);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Remove(int productId)
        {
            try
            {
                var currentProduct = _productRepository.Get(productId);
                _productRepository.Delete(currentProduct);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ProductResponse> GetAll()
        {
            try
            {
                return from product in _productRepository.GetAll()
                       select new ProductResponse(product);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductResponse GetProductById(int productId)
        {
            try
            {
                var currentProduct = _productRepository.Get(productId);
                return new ProductResponse(currentProduct);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
