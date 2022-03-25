using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class Buy
    {
        public int BuyId { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal? TotalPrice { get; set; }


        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }
    }
}
