using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingStoreApiExam.V1.Controllers.Products.ProductRequests;
using ShoppingStoreApiExam.V1.Controllers.Products.ProductResponses;
using ShoppingStoreApiExam.V1.Controllers.Products.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingStoreApiExam.V1.Controllers.Products
{
    [Route("api/v1/product")]
    [ApiController]
    public class ProductController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<ProductResponse> Submit([FromBody] ProductRequest productRequest)
        {
            var result = _productService.Submit(productRequest);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductResponse>> GetAll()
        {
            var result = _productService.GetAll();
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ProductResponse> GetCustomer(int productId)
        {
            var result = _productService.GetProductById(productId);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpDelete("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ProductResponse> Remove(int productId)
        {
            var result = _productService.Remove(productId);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
