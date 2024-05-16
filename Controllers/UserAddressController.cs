using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce_Api.Interfaces;
using E_Commerce_Api.Models;
using Microsoft.AspNetCore.Mvc;
using E_Commerce_Api.Dto;
using E_Commerce_Api.Repository;
using Microsoft.AspNetCore.Authorization;

namespace E_Commerce_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserAddressController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IUserAddressRepository _userAddressRepository;
        public readonly IUserRepository _userRepository;

        public UserAddressController(IMapper mapper, IUserAddressRepository userAddressRepository,IUserRepository userRepository)
        {
            _mapper = mapper;
            _userAddressRepository = userAddressRepository;
            _userRepository = userRepository;
        }

        [HttpPost()]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUserAddress([FromQuery] int userId,[FromBody] UserAddressDto userAddressCreate) {
            if (userAddressCreate == null)
                return BadRequest(ModelState);

            if(!_userRepository.CheckIfUserExist(userId)){
                return NotFound();
            }

            var userAddress = _userAddressRepository.GetAllUserAddressByUser(userId).Where(c => c.Address == userAddressCreate.Address).FirstOrDefault();
            if (userAddress != null) {
                ModelState.AddModelError("", "User Adress Already Exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userAddressMap = _mapper.Map<UserAddress>(userAddressCreate);

            if (!_userAddressRepository.CreateUserAddress(userId,userAddressMap)) {
                ModelState.AddModelError("", "Error Occured While Trying To save");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }


        [HttpGet("{UserAddressId}")]
        [ProducesResponseType(200,Type = typeof(UserAddress))]
        [ProducesResponseType(400)]

        [Authorize(Policy = "Admin/Manager/Owner")]
        public IActionResult GetOneUserAddress(int UserAddressId)
        {
            if(!_userAddressRepository.CheckIfUserAddressExist(UserAddressId)){
                return NotFound();
             }
            var userDetail = _mapper.Map<UserAddressDto>(_userAddressRepository.GetOneUserAddress(UserAddressId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(userDetail);
        }


        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<UserAddress>))]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager")]
        public IActionResult GetUserAddresses()
        {
            var userAddresses = _mapper.Map<List<UserAddressDto>>(_userAddressRepository.GetAllUserAddresses());
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(userAddresses);
        }



        [HttpPut("{userAddressId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Policy = "Admin/Manager/Owner")]
        public IActionResult UpdateUserAddress(int userAddressId,[FromQuery] int userId,[FromQuery] int actionPeformerId,[FromBody] UserAddressDto userAddressUpdate)
        {

            if (userAddressId != userAddressUpdate.Id) {
                return BadRequest(ModelState);
            }
            if (userAddressUpdate == null)
                return BadRequest(ModelState);

            if (userAddressId != userAddressUpdate.Id)
                return BadRequest(ModelState);

            if (!_userAddressRepository.CheckIfUserAddressExist(userAddressId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var userAddressMap = _mapper.Map<UserAddress>(userAddressUpdate);

            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_userAddressRepository.UpdateUserAddress(userId,userAddressMap,actionPeformerId,referenceId))
            {
                ModelState.AddModelError("", "Something went wrong updating User Address");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{userAddressId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager/Owner")]

        public IActionResult DeleteUserAddress(int userAddressId,[FromQuery] int actionPeformerId) {
            if(!_userAddressRepository.CheckIfUserAddressExist(userAddressId)){
                return NotFound();
            }

            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
                return NotFound();
            }

            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_userAddressRepository.DeleteUserAddress(userAddressId, actionPeformerId, referenceId)) {
                ModelState.AddModelError("", "Error Occured While Trying To Delete User Address");
                return StatusCode(500, ModelState);

            }
            return Ok("User Address Deleted Successfully");
         }

    }
}
