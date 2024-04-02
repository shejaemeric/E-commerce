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
using Microsoft.AspNetCore.Authorization;

namespace E_Commerce_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IInvetoryRepository _inventoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public InventoryController(IInvetoryRepository inventoryRepository,IMapper mapper,IUserRepository userRepository)
        {
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpPost()]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager")]

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
        [Authorize(Policy = "Admin/Manager")]

        public IActionResult GetAllInventories()
        {
            var inventories = _mapper.Map<List<InventoryDto>>(_inventoryRepository.GetAllInventories());
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(inventories);
        }


        [HttpPost("{inventoryId}")]
        [ProducesResponseType(200,Type = typeof(Inventory))]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager")]
        public IActionResult GetOneInventory(int inventoryId)
        {
            if(! _inventoryRepository.CheckIfInvetoryExist(inventoryId)){
                return NotFound();
            }

            var inventory= _mapper.Map<InventoryDto>(_inventoryRepository.GetOneInventory(inventoryId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(inventory);
        }



        [HttpPut("{inventoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Policy = "Admin/Manager")]



        public IActionResult UpdateInventory(int inventoryId,[FromBody] InventoryDto inventoryUpdate,[FromQuery] int actionPeformerId)
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

            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_inventoryRepository.UpdateInventory(inventoryMap,actionPeformerId,referenceId))
            {
                ModelState.AddModelError("", "Something went wrong updating Inventory");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{inventoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager")]


        public IActionResult DeleteInventory(int inventoryId,[FromQuery] int actionPeformerId) {
            if(!_inventoryRepository.CheckIfInvetoryExist(inventoryId)){
                return NotFound();
            }

            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
                return NotFound();
            }

            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_inventoryRepository.DeleteInventory(inventoryId, actionPeformerId, referenceId)) {
                ModelState.AddModelError("", "Error Occured While Trying To Delete Inventory");
                return StatusCode(500, ModelState);

            }
            return Ok("Inventory Deleted Successfully");
         }
    }
}
