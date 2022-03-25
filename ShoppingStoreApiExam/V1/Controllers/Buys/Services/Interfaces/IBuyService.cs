using ShoppingStoreApiExam.V1.Controllers.Buys.BuyRequests;
using ShoppingStoreApiExam.V1.Controllers.Buys.BuyResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingStoreApiExam.V1.Controllers.Buys.Services.Interfaces
{
    public interface IBuyService
    {
        BuyResponse Submit(BuyRequest buyRequest);

        IEnumerable<BuyResponse> GetAll();

        BuyResponse GetBuyById(int buyId);

        bool Remove(int buyId);
    }
}

