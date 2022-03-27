using ShoppingStoreApiExam.V1.Controllers.Customers.CustomerRequests;
using ShoppingStoreApiExam.V1.Controllers.Customers.CustomerResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingStoreApiExam.V1.Controllers.Customers.Service.Interfaces
{
    public interface ICustomerService
    {
        CustomerResponse Submit(CustomerRequest customerRequest);

        IEnumerable<CustomerResponse> GetAll();

        CustomerResponse GetCustomerById(int customerId);

        bool Remove(int customerId);
    }
}
