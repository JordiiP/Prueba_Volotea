using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingStoreApiExam.V1.Controllers.Buys.BuyRequests;
using ShoppingStoreApiExam.V1.Controllers.Buys.BuyResponses;
using ShoppingStoreApiExam.V1.Controllers.Buys.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingStoreApiExam.V1.Controllers.Buys
{
    [Route("api/v1/buy")]
    [ApiController]
    public class BuyController
    {
        private readonly IBuyService _buyService;

        public BuyController(IBuyService buyService)
        {
            _buyService = buyService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<BuyResponse> Submit([FromBody] BuyRequest buyRequest)
        {
            var result = _buyService.Submit(buyRequest);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<BuyResponse>> GetAll()
        {
            var result = _buyService.GetAll();
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet("{buyId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<BuyResponse> GetBuy(int buyId)
        {
            var result = _buyService.GetBuyById(buyId);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpDelete("{buyId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<BuyResponse> Remove(int buyId)
        {
            var result = _buyService.Remove(buyId);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
