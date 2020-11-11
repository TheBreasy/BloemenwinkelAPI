using System.Linq;
using BloemenwinkelAPI.Model.Domain;
using BloemenwinkelAPI.Model.Web;
using BloemenwinkelAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BloemenwinkelAPI.Model;
using Microsoft.AspNetCore.Http;

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
        public IActionResult GetAllOrdersForStore(int storeId)
        {
            _logger.LogInformation($"Getting all orders for store {storeId}");
            try
            {
                return Ok(_orderRepository.GetAllOrders(storeId).Select(x => x.Convert()).ToList());
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("{storeId}/orders/{orderId}")]
        public IActionResult GetOneOrderByIdFromStore(int storeId, int orderId)
        {
            _logger.LogInformation($"Getting one order {orderId} from store {storeId}");
            var order = _orderRepository.GetOneOrderById(storeId, orderId);
            return order == null ? (IActionResult)NotFound() : Ok(order.Convert());
        }

        [HttpPost("{storeId}/bouqets")]
        public IActionResult AddOrderToStore(int storeId, int bouqetId, OrderUpsertInput input)
        {
            _logger.LogInformation($"Creating an order for store {storeId}");
            try
            {
                var persistedOrder = _orderRepository.Insert(storeId, bouqetId, input.Amount);
                return Created($"/stores/{storeId}/orders/{persistedOrder.Id}", persistedOrder.Convert());
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPatch("{id}/orders/{orderId}")]
        public IActionResult UpdateOrderInStore(int storeId, int bouqetId, int orderId, OrderUpsertInput input)
        {
            _logger.LogInformation($"Updating order {orderId} for store {storeId}");
            try
            {
                _orderRepository.Update(storeId, bouqetId, orderId, input.Amount);
                return Accepted();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}/orders/{orderId}")]
        public IActionResult DeleteOrderFromStore(int storeId, int orderId)
        {
            _logger.LogInformation($"Deleting order {orderId} from store {storeId}");
            try
            {
                _orderRepository.Delete(storeId, orderId);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
