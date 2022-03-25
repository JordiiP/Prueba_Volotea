using Domain.Entites;
using Domain.Interfaces;
using ShoppingStoreApiExam.V1.Controllers.Customers.CustomerRequests;
using ShoppingStoreApiExam.V1.Controllers.Customers.CustomerResponses;
using ShoppingStoreApiExam.V1.Controllers.Customers.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingStoreApiExam.V1.Controllers.Customers.Service
{
    public class CustomerService:ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;

        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IRepository<Customer> customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public CustomerResponse Submit(CustomerRequest customerRequest)
        {
            try
            {
                var customer = new Customer()
                {
                    Name = customerRequest.Name,
                    LastName = customerRequest.LastName,
                    Dni =customerRequest.Dni
                };
                customer = _customerRepository.Add(customer);
                _unitOfWork.SaveChanges();
                //Guardar un Log de tipo INFO
                return new CustomerResponse(customer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Remove(int customerId)
        {
            try
            {
                var currentCustomer = _customerRepository.Get(customerId);
                _customerRepository.Delete(currentCustomer);
                if (_unitOfWork.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<CustomerResponse> GetAll()
        {
            try
            {
                return from cutomer in _customerRepository.GetAll()
                       select new CustomerResponse(cutomer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CustomerResponse GetBuyById(int customerId)
        {
            try
            {
                var currentCustomer = _customerRepository.Get(customerId);
                return new CustomerResponse(currentCustomer);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
