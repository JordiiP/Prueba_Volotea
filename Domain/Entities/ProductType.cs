using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class ProductType
    {
        public ProductType()
        {
            Product = new HashSet<Product>();
        }

        public int ProductTypeId { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
