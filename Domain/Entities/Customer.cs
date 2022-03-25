using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class Customer
    {
        public Customer()
        {
            Buy = new HashSet<Buy>();
        }

        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Dni { get; set; }

        public virtual ICollection<Buy> Buy { get; set; }
    }
}
