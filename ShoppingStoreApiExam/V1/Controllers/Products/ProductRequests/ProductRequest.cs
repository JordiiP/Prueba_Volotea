using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingStoreApiExam.V1.Controllers.Products.ProductRequests
{
    public class ProductRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public ProductTypeEnum ProductType { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }
}
