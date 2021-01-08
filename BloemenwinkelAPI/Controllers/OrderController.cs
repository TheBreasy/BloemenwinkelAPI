using BloemenwinkelAPI.Model.Domain;
using BloemenwinkelAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BloemenwinkelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly Orderservice _orderservice;

        public OrderController(Orderservice orderservice)
        {
            _orderservice = orderservice;
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
    }
}