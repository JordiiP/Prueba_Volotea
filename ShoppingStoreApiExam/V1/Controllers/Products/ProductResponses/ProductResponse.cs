using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingStoreApiExam.V1.Controllers.Products.ProductResponses
{
    public class ProductResponse
    {
        public ProductResponse(Product product)
        {
            ProductId = product.ProductId;
        }

        public int ProductId { get; set; }
    }
}
