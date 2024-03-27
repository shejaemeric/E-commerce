using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce_Api.Interfaces;
using E_Commerce_Api.Models;
using Microsoft.AspNetCore.Mvc;
using E_Commerce_Api.Dto;

namespace E_Commerce_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet("{UserId}")]
        [ProducesResponseType(200,Type = typeof(ShoppingSession))]
        [ProducesResponseType(400)]

        public IActionResult GetLatestShoppingSession(int UserId)
        {
            if(!_userRepository.CheckIfUserExist(UserId)){
                return NotFound();
             }
            var session = _mapper.Map<ShoppingSessionDto>(_shoppingSessionRepository.GetLatestShoppingSessionByUser(UserId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(session);
        }

        [HttpPut("{shoppingSessionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateShoppingSession(int shoppingSessionId,[FromQuery] int userId,[FromQuery] int actionPeformerId,[FromBody] ShoppingSessionDto shoppingSessionUpdate)
        {
            if (shoppingSessionUpdate == null)
                return BadRequest(ModelState);

            if (shoppingSessionId != shoppingSessionUpdate.Id)
                return BadRequest(ModelState);

            if (!_shoppingSessionRepository.CheckIfShoppingSessionExist(shoppingSessionId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

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
        public IActionResult DeleteOrderDetails(int shoppingSessionId,[FromQuery] int actionPeformerId) {
            if(!_shoppingSessionRepository.CheckIfShoppingSessionExist(shoppingSessionId)){
                return NotFound();
            }
            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
                return NotFound();
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
