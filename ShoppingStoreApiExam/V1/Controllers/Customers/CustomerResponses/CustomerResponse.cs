using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingStoreApiExam.V1.Controllers.Customers.CustomerResponses
{
    public class CustomerResponse
    {
        public CustomerResponse(Customer customer)
        {
            CustomerId = customer.CustomerId;
        }

        public int CustomerId { get; set; }
    }
}
