using System.Linq;
using BloemenwinkelAPI.Model.Domain;
using BloemenwinkelAPI.Model.Web;
using BloemenwinkelAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BloemenwinkelAPI.Model;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BloemenwinkelAPI.Controllers
{
    [ApiController]
    [Route("stores")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderRepository orderRepository, ILogger<OrderController> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        [HttpGet("{storeId}/order")]
        [ProducesResponseType(typeof(IEnumerable<OrderWebOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllOrdersForStore(int storeId)
        {
            _logger.LogInformation($"Getting all orders for store {storeId}");
            try
            {
                var orders = await _orderRepository.GetAllOrders(storeId);//.Select(x => x.Convert()).ToList(); Commented because when adding the Async Task, the Selection is not available anymore.
                return Ok(orders);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("{storeId}/orders/{orderId}")]
        [ProducesResponseType(typeof(OrderWebOutput), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOneOrderByIdFromStore(int storeId, int orderId)
        {
            _logger.LogInformation($"Getting one order {orderId} from store {storeId}");
            var order = await _orderRepository.GetOneOrderById(storeId, orderId);
            return order == null ? (IActionResult)NotFound() : Ok(order.Convert());
        }

        [HttpPost("{storeId}/bouqets")]
        [ProducesResponseType(typeof(OrderWebOutput), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddOrderToStore(int storeId, int bouqetId, OrderUpsertInput input)
        {
            _logger.LogInformation($"Creating an order for store {storeId}");
            try
            {
                var persistedOrder = await _orderRepository.Insert(storeId, bouqetId, input.Amount);
                return Created($"/stores/{storeId}/orders/{persistedOrder.Id}", persistedOrder.Convert());
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPatch("{id}/orders/{orderId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateOrderInStore(int storeId, int bouqetId, int orderId, OrderUpsertInput input)
        {
            _logger.LogInformation($"Updating order {orderId} for store {storeId}");
            try
            {
                await _orderRepository.Update(storeId, bouqetId, orderId, input.Amount);
                return Accepted();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}/orders/{orderId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteOrderFromStore(int storeId, int orderId)
        {
            _logger.LogInformation($"Deleting order {orderId} from store {storeId}");
            try
            {
                await _orderRepository.Delete(storeId, orderId);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }

        //Loops over stores in array, to compare them with eachother and sorts the bouquet sales from high to low. (under construction)
        [HttpGet]
        public IActionResult BouquetSales()
        {
            /* _logger.LogInformation($"Getting highest bouquet sales");
            try
            {
                var allStores = _storeRepository.GetAllStores();
                for (int i = 0; i < allStores.Count(); i++)
                {
                    return null;
                }
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }*/
            return null;
        }

        //Shows the best selling bouquets per store. (under construction)
        [HttpGet]
        public IActionResult BouquetSalesPerStore(int storeId)
        {
            /* _logger.LogInformation($"Getting bouquet sales from store {storeId}");
            var order = _orderRepository.
            return order == null ? (IActionResult)NotFound() : Ok(order.Convert());*/
            return null;
        }

        //Shows the user an overview of the highest turnover of all stores. (under construction)
        [HttpGet]
        public IActionResult TurnoverStores()
        {
            /* _logger.LogInformation($"Getting turnover from all stores");
             var allStores = _storeRepository.GetAllStores();
             allStores.ToString();
             allStores[1];*/
            return null;
        }

        //Shows the user an overview of which store has the most sales per region. (under construction)
        [HttpGet]
        public IActionResult ComparisonStoreSales()
        {
            return null;
        }
    }
}
