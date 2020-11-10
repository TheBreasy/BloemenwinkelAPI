using System.Linq;
using BloemenwinkelAPI.Model;
using BloemenwinkelAPI.Model.Web;
using BloemenwinkelAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BloemenwinkelAPI.Controllers
{
    [ApiController]
    [Route("BloemenwinkelAPI/Model/Store")]
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
        public IActionResult GetAllStores()
        {
            _logger.LogInformation("Getting all stores");
            var stores = _storeRepository.GetAllStores().Select(x => x.Convert()).ToList();
            return Ok(stores);
        }

        [HttpGet("{id}")]
        public IActionResult StoreById(int id)
        {
            _logger.LogInformation("Getting store by id", id);
            var store = _storeRepository.GetOneStoreById(id);
            return store == null ? (IActionResult)NotFound() : Ok(store.Convert());
        }

        [HttpPost]
        public IActionResult CreateStore(StoreUpsertInput input)
        {
            _logger.LogInformation("Creating a store", input);
            var persistedStore = _storeRepository.Insert(input.Name);
            return Created($"/stores/{persistedStore.Id}", persistedStore.Convert());
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateStore(int id, StoreUpsertInput input)
        {
            _logger.LogInformation("Updating a store", input);
            
            try
            {
                var store = _storeRepository.Update(id, input.Name);
                return Accepted(store.Convert());
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGarage(int id)
        {
            _logger.LogInformation("Deleting a garage", id);
            try
            {
                _storeRepository.Delete(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
