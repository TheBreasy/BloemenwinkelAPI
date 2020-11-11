using System.Linq;
using BloemenwinkelAPI.Model.Domain;
using BloemenwinkelAPI.Model.Web;
using BloemenwinkelAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BloemenwinkelAPI.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

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
        [ProducesResponseType(typeof(IEnumerable<BouqetWebOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllBouqetsFromStore(int storeId)   
        {
            _logger.LogInformation($"Getting all bouqets for store {storeId}");
            try
            {
                var bouqets = await _bouquetRepository.GetAllBouqets(storeId);//.Select(x => x.Convert()).ToList; Commented because when adding the Async Task, the Selection is not available anymore.
                return Ok(bouqets);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("{storeId}/bouqets/{bouqetId}")]
        [ProducesResponseType(typeof(BouqetWebOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOneBouqetByIdFromStore(int storeId, int bouqetId)
        {
            _logger.LogInformation($"Getting one bouqet {bouqetId} from store {storeId}");
            var bouqet = await _bouquetRepository.GetOneBouqetById(storeId, bouqetId);
            return bouqet == null ? (IActionResult)NotFound() : Ok(bouqet.Convert());
        }

        //Allows the user to register a bouquet sale.
        [HttpPost("{storeId}/bouqets")]
        [ProducesResponseType(typeof(BouqetWebOutput), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RegisterBouquetSale(int storeId, BouqetUpsertInput input)
        {
            _logger.LogInformation($"Creating a bouqet for store {storeId}");
            try
            {
                var persistedBouqet = await _bouquetRepository.Insert(storeId, input.Name, input.Price, input.Description);
                return Created($"/stores/{storeId}/bouqets/{persistedBouqet.Id}", persistedBouqet.Convert());
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPatch("{id}/bouqets/{bouqetId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateBouqet(int storeId, int bouqetId, BouqetUpsertInput input)
        {
            _logger.LogInformation($"Updating bouqet {bouqetId} for store {storeId}");
            try
            {
                await _bouquetRepository.Update(storeId, bouqetId, input.Name, input.Price);
                return Accepted();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}/bouqets/{bouqetId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteBouqetFromStore(int storeId, int bouqetId)
        {
            _logger.LogInformation($"Deleting bouqet {bouqetId} from store {storeId}");
            try
            {
                await _bouquetRepository.Delete(storeId, bouqetId);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}