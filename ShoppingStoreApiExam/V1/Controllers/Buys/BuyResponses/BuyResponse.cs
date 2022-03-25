using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingStoreApiExam.V1.Controllers.Buys.BuyResponses
{
    public class BuyResponse
    {
        public BuyResponse (Buy buy)
        {
            BuyId = buy.BuyId;
        }

        public int BuyId { get; set; }
    }
}
