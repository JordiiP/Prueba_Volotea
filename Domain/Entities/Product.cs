using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class Product
    {
        public Product()
        {
            Buy = new HashSet<Buy>();
        }

        public int ProductId { get; set; }

        public int? ProductTypeId { get; set; }

        public decimal? UnitPrice { get; set; }

        public virtual ProductType ProductType { get; set; }

        public virtual ICollection<Buy> Buy { get; set; }
    }
}
