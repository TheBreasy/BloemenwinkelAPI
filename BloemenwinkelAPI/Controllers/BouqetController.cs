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
    [Route("stores")]
    public class BouqetController : ControllerBase
    {
        private readonly IBouqetRepository _bouquetRepository;
        private readonly ILogger<BouqetController> _logger;

        public BouqetController(IBouqetRepository bouqetRepository, ILogger<BouqetController> logger)
        {
            _bouquetRepository = bouqetRepository;
            _logger = logger;
        }

        [HttpGet("{storeId}/bouqets")]
        /*[ProducesResponseType(typeof(IEnumerable<BouqetWebOutput>), StatusCodes.Status200OK)]*/
        public IActionResult GetAllBouqetsFromStore(int storeId)
        {
            _logger.LogInformation($"Getting all bouqets for store {storeId}");
            try
            {
                return Ok(_bouquetRepository.GetAllBouqets(storeId).Select(x => x.Convert()).ToList());
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("{storeId}/bouqets/{bouqetId}")]
        public IActionResult GetOneBouqetByIdFromStore(int storeId, int bouqetId)
        {
            _logger.LogInformation($"Getting one bouqet {bouqetId} from store {storeId}");
            var bouqet = _bouquetRepository.GetOneBouqetById(storeId, bouqetId);
            return bouqet == null ? (IActionResult)NotFound() : Ok(bouqet.Convert());
        }

        [HttpPost("{storeId}/bouqets")]
        public IActionResult AddBouqetToStore(int storeId, BouqetUpsertInput input)
        {
            _logger.LogInformation($"Creating a bouqet for store {storeId}");
            try
            {
                var persistedBouqet = _bouquetRepository.Insert(storeId, input.Name, input.Price, input.Description);
                return Created($"/stores/{storeId}/bouqets/{persistedBouqet.Id}", persistedBouqet.Convert());
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPatch("{id}/bouqets/{bouqetId}")]
        public IActionResult UpdateBouqet(int storeId, int bouqetId, BouqetUpsertInput input)
        {
            _logger.LogInformation($"Updating bouqet {bouqetId} for store {storeId}");
            try
            {
                _bouquetRepository.Update(storeId, bouqetId, input.Name, input.Price);
                return Accepted();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}/bouqets/{bouqetId}")]
        public IActionResult DeleteBouqetFromStore(int storeId, int bouqetId)
        {
            _logger.LogInformation($"Deleting bouqet {bouqetId} from store {storeId}");
            try
            {
                _bouquetRepository.Delete(storeId, bouqetId);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet]
        public IActionResult BouquetSales()
        {
            return null;
        }

        [HttpGet]
        public IActionResult BouquetSalesPerStore()
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