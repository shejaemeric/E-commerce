using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce_Api.Interfaces;
using E_Commerce_Api.Models;
using Microsoft.AspNetCore.Mvc;
using E_Commerce_Api.Dto;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace E_Commerce_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ShoppingSessionController : ControllerBase
    {

        // ShoppingSession GetLatestShoppingSessionByUser(int userId);

        public readonly IMapper _mapper;
         public readonly IUserRepository _userRepository;
        public readonly IShoppingSessionRepository _shoppingSessionRepository;
        public ShoppingSessionController(IMapper mapper,IShoppingSessionRepository shoppingSessionRepository,IUserRepository userRepository)
        {
            _mapper = mapper;
            _shoppingSessionRepository = shoppingSessionRepository;
            _userRepository = userRepository;
        }


        [HttpPost()]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [SwaggerOperation(Summary = "Create Shopping Session (Anyone)")]
        public IActionResult CreateShoppingSession([FromQuery] int userId,[FromBody] ShoppingSessionDto shoppingSessionCreate) {
            if (shoppingSessionCreate == null)
                return BadRequest(ModelState);

            if(!_userRepository.CheckIfUserExist(userId)){
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var shoppingSessionMap = _mapper.Map<ShoppingSession>(shoppingSessionCreate);

            if (!_shoppingSessionRepository.CreateShoppingSession(userId,shoppingSessionMap)) {
                ModelState.AddModelError("", "Error Occured While Trying To save");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }



        [HttpPost("{shoppingSessionId}/cartItems")]
        [ProducesResponseType(200,Type = typeof(ICollection<CartItem>))]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager/Owner")]
        [SwaggerOperation(Summary = "Get All Cart Item Selected in a Shopping Session (Admin/Manager/Owner)")]
        public IActionResult GetAllCartItemsBySession(int shoppingSessionId)
        {
            if(! _shoppingSessionRepository.CheckIfShoppingSessionExist(shoppingSessionId)){
                return NotFound();
            }
            var cartItems= _mapper.Map<List<CartItemDto>>(_shoppingSessionRepository.GetAllCartItemsBySession(shoppingSessionId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(cartItems);
        }


        [HttpPut("{shoppingSessionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Policy = "Admin/Manager/Owner")]
        [SwaggerOperation(Summary = "Update a Shopping Session (Admin/Manager/Owner)")]
        public IActionResult UpdateShoppingSession(int shoppingSessionId,[FromQuery] int userId,[FromBody] ShoppingSessionDto shoppingSessionUpdate)
        {
            if (shoppingSessionUpdate == null)
                return BadRequest(ModelState);

            if (shoppingSessionId != shoppingSessionUpdate.Id)
                return BadRequest(ModelState);

            if (!_shoppingSessionRepository.CheckIfShoppingSessionExist(shoppingSessionId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var actionPeformerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
               return StatusCode(405, "User not Allowed/Authenticated");
            }


            var shoppingSessionMap = _mapper.Map<ShoppingSession>(shoppingSessionUpdate);

            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_shoppingSessionRepository.UpdateShoppingSession(userId,shoppingSessionMap,actionPeformerId,referenceId))
            {
                ModelState.AddModelError("", "Something went wrong updating User Payment");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{shoppingSessionId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager/Owner")]

        [SwaggerOperation(Summary = "Delete a Shopping Session (Admin/Manager/Owner)")]
        public IActionResult DeleteShoppingSession(int shoppingSessionId) {
            if(!_shoppingSessionRepository.CheckIfShoppingSessionExist(shoppingSessionId)){
                return NotFound();
            }

            var actionPeformerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
               return StatusCode(405, "User not Allowed/Authenticated");
            }


            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_shoppingSessionRepository.DeleteShoppingSession(shoppingSessionId, actionPeformerId, referenceId)) {
                ModelState.AddModelError("", "Error Occured While Trying To Delete Shopping Session");
                return StatusCode(500, ModelState);

            }
            return Ok("Shopping Session Deleted Successfully");
         }


    }
}
