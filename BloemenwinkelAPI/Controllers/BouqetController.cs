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
    [Route("bouqets")]
    public class BouqetController : ControllerBase
    {
        private readonly IBouqetRepository _bouquetRepository;
        private readonly ILogger<BouqetController> _logger;

        public BouqetController(IBouqetRepository bouqetRepository, ILogger<BouqetController> logger)
        {
            _bouquetRepository = bouqetRepository;
            _logger = logger;
        }

        /// <summary>
        /// Gets a list of all bouqets from a store given by id.
        /// </summary>
        /// <param name="storeId">The unique identifier of the store</param>
        /// <returns>A list of bouqets from the given storeid</returns>
        /// <response code="200">The list was succesfully aquired</response>
        /// <response code="404">The get function didn't find any store of storeid</response>
        [HttpGet("{storeId}/bouqets")]
        [ProducesResponseType(typeof(IEnumerable<BouqetWebOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllBouqetsFromStore(int storeId)   
        {
            _logger.LogInformation($"Getting all bouqets for store {storeId}");
            try
            {
                var bouqets = (await _bouquetRepository.GetAllBouqets(storeId)).Select(x => x.Convert()).ToList();
                return Ok(bouqets);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Gets one bouqet of bouqetid from the store of storeid.
        /// </summary>
        /// <param name="storeId">The unique identifier of the store</param>
        /// <param name="bouqetId">The unique identifier of the bouqet</param>
        /// <returns>A single bouqet of bouqetid from store of storeid</returns>
        /// <response code="200">The bouqet is succesfully aquired</response>
        /// <response code="404">The get function didn't find any store of storeid or bouqet of bouqetid</response>
        [HttpGet("{storeId}/bouqets/{bouqetId}")]
        [ProducesResponseType(typeof(BouqetWebOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOneBouqetByIdFromStore(int storeId, int bouqetId)
        {
            _logger.LogInformation($"Getting one bouqet {bouqetId} from store {storeId}");
            var bouqet = await _bouquetRepository.GetOneBouqetById(storeId, bouqetId);
            return bouqet == null ? (IActionResult)NotFound() : Ok(bouqet.Convert());
        }


        /// <summary>
        /// Creates or adds a bouqet to a store.
        /// </summary>
        /// <param name="storeId">The unique identifier of the store</param>
        /// <param name="input">The body of the store</param>
        /// <returns></returns>
        /// <response code="201">A new bouqet is created</response>
        /// <response code="404">The storeId was not found</response>
        [HttpPost("{storeId}/bouqets")]
        [ProducesResponseType(typeof(BouqetWebOutput), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddBouqetToStore(int storeId, BouqetUpsertInput input)
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

        /// <summary>
        /// Updates a bouqet
        /// </summary>
        /// <param name="storeId">The unique identifier of the store</param>
        /// <param name="bouqetId">The unique identifier of the bouqet</param>
        /// <param name="input">The body of the store</param>
        /// <returns></returns>
        /// <respons code="202">Bouqet is updated</respons>
        /// <respons code="404">Either storeId or bouqetId or both ids were not found</respons>
        [HttpPatch("{id}/bouqets/{bouqetId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateBouqetToStore(int storeId, int bouqetId, BouqetUpsertInput input)
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

        /// <summary>
        /// Deletes a bouqet from store
        /// </summary>
        /// <param name="storeId">The unique identifier of the store</param>
        /// <param name="bouqetId">The unique identifier of the bouqet</param>
        /// <returns></returns>
        /// <respons code="204">Bouqet is deleted</respons>
        /// <respons code="404">Either storeId or bouqetId or both ids were not found</respons>
        [HttpDelete("{id}/bouqets/{bouqetId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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