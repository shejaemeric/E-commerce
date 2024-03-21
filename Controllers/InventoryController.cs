using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce_Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using E_Commerce_Api.Dto;
using E_Commerce_Api.Models;
using E_Commerce_Api.Repository;

namespace E_Commerce_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInvetoryRepository _inventoryRepository;
        private readonly IMapper _mapper;
        public InventoryController(IInvetoryRepository inventoryRepository,IMapper mapper)
        {
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
        }

        [HttpPost()]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateInventory([FromBody] InventoryDto inventoryCreate) {
            if (inventoryCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var inventoryMap = _mapper.Map<Inventory>(inventoryCreate);

            if (!_inventoryRepository.CreateInvetory(inventoryMap)) {
                ModelState.AddModelError("", "Error Occured While Trying To Save");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }

        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<Inventory>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsers()
        {
            var inventories = _mapper.Map<List<InventoryDto>>(_inventoryRepository.GetAllInventories());
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(inventories);
        }

        [HttpPut("{inventoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateInventory(int inventoryId,[FromBody] InventoryDto inventoryUpdate)
        {
            if (inventoryUpdate == null)
                return BadRequest(ModelState);

            if (inventoryId != inventoryUpdate.Id)
                return BadRequest(ModelState);

            if (!_inventoryRepository.CheckIfInvetoryExist(inventoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var inventoryMap = _mapper.Map<Inventory>(inventoryUpdate);

            if (!_inventoryRepository.UpdateInventory(inventoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Inventory");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
