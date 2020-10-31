using System.Linq;
using BloemenwinkelAPI.Model.Domain;
using BloemenwinkelAPI.Model.Web;
using BloemenwinkelAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BloemenwinkelAPI.Model;

namespace BloemenwinkelAPI.Controllers
{
    [ApiController]
    [Route("Bloemenwinkel")]
    public class BloemenwinkelController : ControllerBase
    {
        private readonly IBouqetRepository _bouquetRepository;
        private readonly ILogger<BloemenwinkelController> _logger;

        public BloemenwinkelController(IBouqetRepository bouqetRepository, ILogger<BloemenwinkelController> logger)
        {
            _bouquetRepository = bouqetRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllBouqets()
        {
            _logger.LogInformation("Getting all bouqets");
            var bouqets = _bouquetRepository.GetAllBouqets().Select(x => x.Convert()).ToList();
            return Ok(bouqets);
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