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

namespace E_Commerce_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet("user/{userId}")]
        [ProducesResponseType(200,Type = typeof(IEnumerable<UserAddress>))]
        [ProducesResponseType(400)]

        public IActionResult GetAllUserAddressByUser(int userId)
        {
            if(!_userRepository.CheckIfUserExist(userId)){
                return NotFound();
             }
            var paymentDetails = _mapper.Map<List<UserAddressDto>>(_userAddressRepository.GetAllUserAddressByUser(userId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(paymentDetails);
        }


        [HttpPut("{userAddressId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUserAddress(int userAddressId,[FromQuery] int userId,[FromBody] UserAddressDto userAddressUpdate)
        {
            if (userAddressUpdate == null)
                return BadRequest(ModelState);

            if (userAddressId != userAddressUpdate.Id)
                return BadRequest(ModelState);

            if (!_userAddressRepository.CheckIfUserAddressExist(userAddressId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var userAddressMap = _mapper.Map<UserAddress>(userAddressUpdate);

            if (!_userAddressRepository.UpdateUserAddress(userId,userAddressMap))
            {
                ModelState.AddModelError("", "Something went wrong updating User Address");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
