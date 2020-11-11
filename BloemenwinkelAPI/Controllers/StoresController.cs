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

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StoreWebOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllStores()
        {
            _logger.LogInformation("Getting all stores");
            var stores = await _storeRepository.GetAllStores();//.Select(x => x.Convert()).ToList(); Commented because when adding the Async Task, the Selection is not available anymore.
            return Ok(stores);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StoreWebOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> StoreById(int id)
        {
            _logger.LogInformation("Getting store by id", id);
            var store = await _storeRepository.GetOneStoreById(id);
            return store == null ? (IActionResult)NotFound() : Ok(store.Convert());
        }

        [HttpPost]
        [ProducesResponseType(typeof(StoreWebOutput), StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateStore(StoreUpsertInput input)
        {
            _logger.LogInformation("Creating a store", input);
            var persistedStore = await _storeRepository.Insert(input.Name);
            return Created($"/stores/{persistedStore.Id}", persistedStore.Convert());
        }

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
