using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloemenwinkelAPI.Model;
using BloemenwinkelAPI.Model.Web;
using BloemenwinkelAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BloemenwinkelAPI.Controllers
{
    [ApiController]
    [Route("stores")]
    public class StoresController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ILogger<StoresController> _logger;

        public StoresController(IStoreRepository storeRepository, ILogger<StoresController> logger)
        {
            _storeRepository = storeRepository;
            _logger = logger;
        }

        /// <summary>
        /// Gets a list of all the stores.
        /// </summary>
        /// <returns>A list of all stores.</returns>
        /// <response code="200">The list of stores was succesfully aquired</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StoreWebOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllStores()
        {
            _logger.LogInformation("Getting all stores");
            var stores = (await _storeRepository.GetAllStores()).Select(x => x.Convert()).ToList();
            return Ok(stores);
        }

        /// <summary>
        /// Get a store for the given storeId
        /// </summary>
        /// <param name="id">The unique identifier of the store</param>
        /// <returns>The store that matches the storeid</returns>
        /// <response code="200">The store is succesfully aquired</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StoreWebOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> StoreById(int id)
        {
            _logger.LogInformation("Getting store by id", id);
            var store = await _storeRepository.GetOneStoreById(id);
            return store == null ? (IActionResult) NotFound() : Ok(store.Convert());
        }

        /// <summary>
        /// Creates a new store
        /// </summary>
        /// <param name="input">The body of the store</param>
        /// <returns></returns>
        /// <response code="201">A new store is created</response>
        [HttpPost]
        [ProducesResponseType(typeof(StoreWebOutput), StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateStore(StoreUpsertInput input)
        {
            _logger.LogInformation("Creating a store", input);
            var persistedStore = await _storeRepository.Insert(input.Name);
            return Created($"/stores/{persistedStore.Id}", persistedStore.Convert());
        }

        /// <summary>
        /// Updates an existing store by id
        /// </summary>
        /// <param name="id">The unique identifier from the store</param>
        /// <param name="input">The body of the store</param>
        /// <returns></returns>
        /// <response code="202">The store is succesfully updated</response>
        /// <response code="404">The id was not found</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateStore(int id, StoreUpsertInput input)
        {
            _logger.LogInformation("Updating a store", input);
            
            try
            {
                var store = await _storeRepository.Update(id, input.Name, input.Address, input.Region);
                return Accepted(store.Convert());
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Deletes an existing store by id
        /// </summary>
        /// <param name="id">The unique identifier of the store</param>
        /// <returns></returns>
        /// <response code="202">The store is succesfully deleted</response>
        /// <response code="404">The given id was not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteStore(int id)
        {
            _logger.LogInformation("Deleting a store", id);
            try
            {
                await _storeRepository.Delete(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
