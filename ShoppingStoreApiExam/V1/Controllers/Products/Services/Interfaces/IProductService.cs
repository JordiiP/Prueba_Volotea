using ShoppingStoreApiExam.V1.Controllers.Products.ProductRequests;
using ShoppingStoreApiExam.V1.Controllers.Products.ProductResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingStoreApiExam.V1.Controllers.Products.Services.Interfaces
{
    public interface IProductService
    {
        ProductResponse Submit(ProductRequest productRequest);

        IEnumerable<ProductResponse> GetAll();

        ProductResponse GetProductById(int productRequest);

        bool Remove(int buyId);
    }
}
