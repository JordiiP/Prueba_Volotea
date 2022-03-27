using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingStoreApiExam.V1.Controllers.Customers.CustomerRequests;
using ShoppingStoreApiExam.V1.Controllers.Customers.CustomerResponses;
using ShoppingStoreApiExam.V1.Controllers.Customers.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingStoreApiExam.V1.Controllers.Customers
{
    [Route("api/v1/customer")]
    [ApiController]
    public class CustomerController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<CustomerResponse> Submit([FromBody] CustomerRequest customerRequest)
        {
            var result = _customerService.Submit(customerRequest);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CustomerResponse>> GetAll()
        {
            var result = _customerService.GetAll();
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CustomerResponse> GetCustomer(int customerId)
        {
            var result = _customerService.GetCustomerById(customerId);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpDelete("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CustomerResponse> Remove(int customerId)
        {
            var result = _customerService.Remove(customerId);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
