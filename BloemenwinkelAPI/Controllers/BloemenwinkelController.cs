using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using Microsoft.Extensions.Logging;

namespace BloemenwinkelAPI.Controllers
{
    [ApiController]
    [Route("Bloemenwinkel")]
    public class BloemenwinkelController : ControllerBase
    {
        private readonly IInMemoryDatabase _database;
        private readonly ILogger<BloemenwinkelController> _logger;

        public BloemenwinkelController(IInMemoryDatabase database, ILogger<BloemenwinkelController> logger)
        {
            _logger = logger;
            //_database = database;
        }

        [HttpGet]
        public IActionResult BouquetSales()
        {
            return null;
        }

        [HttpGet]
        public IActionResult BouquetSalesStore()
        {
            return null;
        }

        [HttpGet]
        public IActionResult TurnoverStore()
        {
            return null;
        }

        [HttpGet]
        public IActionResult ComparisonStoreSales()
        {
            return null;
        }

        [HttpGet]
        public IActionResult RegisterBouquetSale()
        {
            return null;
        }


    }
}