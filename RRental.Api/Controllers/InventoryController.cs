using System;
using System.Collections.Generic;
using System.Web.Http;
using RRental.Api.Interfaces;
using RRental.Api.Models;

namespace RRental.Api.Controllers
{
    [Route("api/[controller]")]
    public class InventoryController : ApiController
    {
        private IInventoryRepository _inventoryRepository;

        public InventoryController(IInventoryRepository inventoryRepository) =>
            _inventoryRepository = inventoryRepository;

        [HttpGet]
        public IEnumerable<Inventory> Get() => _inventoryRepository.GetAllInventoryItems();

        [HttpGet]
        public Inventory Get(Guid id) => _inventoryRepository[id];

        [HttpPost]
        public Inventory Post([FromBody] Inventory item) =>
            _inventoryRepository.AddItem(new Inventory()
            {
                Id = new Guid(),
                Name = item.Name,
                Type = item.Type
            });

        [HttpPut]
        public Inventory Put([FromBody] Inventory item) => _inventoryRepository.UpdateInventory(item);

        //[HttpPatch("{id}")]
        //public StatusCodeResult Patch(int id, [FromBody]JsonPatchDocument<Inventory> patch)
        //{
        //    Inventory item = Get(id);
        //    if (res != null)
        //    {
        //        patch.ApplyTo(res);
        //        return Ok();
        //    }
        //    return NotFound();
        //}

        [HttpDelete]
        public void Delete(Guid id) => _inventoryRepository.DeleteItem(id);
    }
}
