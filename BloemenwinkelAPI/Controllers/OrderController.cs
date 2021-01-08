using BloemenwinkelAPI.Model.Domain;
using BloemenwinkelAPI.Repositories;
using BloemenwinkelAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloemenwinkelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly Orderservice _orderservice;
        private readonly IOrderRepository _orderRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<OrderController> _logger;

        public OrderController(Orderservice orderservice, ILogger<OrderController> logger, IOrderRepository orderRepository, IMemoryCache memoryCache)
        {
            _orderservice = orderservice;
            _orderRepository = orderRepository;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Order>> Get() =>
            _orderservice.Get();

        [HttpGet("{id:length(24)}", Name = "GetOrder")]
        public ActionResult<Order> Get(int id)
        {
            var order = _orderservice.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPost]
        public ActionResult<Order> Create(Order order)
        {
            _orderservice.Create(order);

            return CreatedAtRoute("GetOrder", new { id = order.Id.ToString() }, order);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(int id, Order orderIn)
        {
            var order = _orderservice.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            _orderservice.Update(id, orderIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(int id)
        {
            var order = _orderservice.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            _orderservice.Remove(order.Id);

            return NoContent();
        }




        /*[HttpGet("Overview")]
        public async Task<IActionResult> GetSalesOverview()
        {
            var sales = await GetAllOrderFromCacheAsync();
            var overview = from sale in sales
                           group sale by sale.Bouquet_id into bouquetOverview
                           select new
                           {
                               Bouquet_id = bouquetOverview.Key,
                               TotalAmountSold = bouquetOverview.Sum(x => x.Amount),
                           };

            overview = overview.OrderByDescending(bouquetOverview => bouquetOverview.TotalAmountSold);
            return Ok(overview);
        }*/




    }
}