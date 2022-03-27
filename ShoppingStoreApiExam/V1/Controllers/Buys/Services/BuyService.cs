using Domain.Entites;
using Domain.Interfaces;
using ShoppingStoreApiExam.V1.Controllers.Buys.BuyRequests;
using ShoppingStoreApiExam.V1.Controllers.Buys.BuyResponses;
using ShoppingStoreApiExam.V1.Controllers.Buys.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingStoreApiExam.V1.Controllers.Buys.Services
{
    public class BuyService : IBuyService
    {
        private readonly IRepository<Buy> _buyRepository;

        private readonly IUnitOfWork _unitOfWork;

        public BuyService(IRepository<Buy> buyRepository, IUnitOfWork unitOfWork)
        {
            _buyRepository = buyRepository;
            _unitOfWork = unitOfWork;
        }

        public BuyResponse Submit(BuyRequest buyRequest)
        {
            try
            {
                var buy = new Buy()
                {
                    CustomerId=buyRequest.CustomerId,
                    ProductId=buyRequest.ProductId,
                    Quantity=buyRequest.Quantity,
                };
                buy = _buyRepository.Add(buy);
                _unitOfWork.SaveChanges();
                return new BuyResponse(buy);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Remove(int buyId)
        {
            try
            {
                var currentBuy = _buyRepository.Get(buyId);
                _buyRepository.Delete(currentBuy);
                var unitOfWorkResponse = _unitOfWork.SaveChanges();
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<BuyResponse> GetAll()
        {
            try
            {
                return from buy in _buyRepository.GetAll() 
                       select new BuyResponse(buy);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BuyResponse GetBuyById(int buyId)
        {
            try
            {
                var currentBuy = _buyRepository.Get(buyId);
                return new BuyResponse(currentBuy);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
