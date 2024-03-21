using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce_Api.Interfaces;
using E_Commerce_Api.Models;
using E_Commerce_Api.Data;
using Microsoft.AspNetCore.Mvc;
using E_Commerce_Api.Dto;


namespace E_Commerce_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost()]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDto userCreate) {
            if (userCreate == null)
                return BadRequest(ModelState);


            var user = _userRepository.GetAllUsers().Where(c => c.Username == userCreate.Username).FirstOrDefault();

            if (user != null) {
                ModelState.AddModelError("", "User Already Exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(userCreate);

            if (!_userRepository.CreateUser(userMap)) {
                ModelState.AddModelError("", "Error Occured While Trying To Save");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }

        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<User>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetAllUsers());
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(users);
        }



        [HttpPost("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetOneUser(int userId)
        {
            if(! _userRepository.CheckIfUserExist(userId)){
                return NotFound();
            }

            var user = _mapper.Map<UserDto>(_userRepository.GetOneUsers(userId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(user);
        }


        [HttpGet("{userId}/addresses")]
        [ProducesResponseType(200,Type = typeof(ICollection<UserAddress>))]
        [ProducesResponseType(400)]

        public IActionResult GetAddress(int userId)
        {
            if(!_userRepository.CheckIfUserExist(userId)){
                return NotFound();
            }

            var userAdresses = _mapper.Map<List<UserAddressDto>>(_userRepository.GetAllUserAddressesByUser(userId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(userAdresses);
        }

        [HttpGet("{userId}/payments")]
        [ProducesResponseType(200,Type = typeof(ICollection<UserPayment>))]
        [ProducesResponseType(400)]

        public IActionResult GetPaymentDetails(int userId)
        {
            if(!_userRepository.CheckIfUserExist(userId)){
                return NotFound();
            }

            var paymentDetails = _mapper.Map<List<UserPaymentDto>>(_userRepository.GetAllUserPaymentsByUser(userId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(paymentDetails);
        }


        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId,[FromBody] UserDto userUpdate)
        {
            if (userUpdate == null)
                return BadRequest(ModelState);

            if (userId != userUpdate.Id)
                return BadRequest(ModelState);

            if (!_userRepository.CheckIfUserExist(userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var userMap = _mapper.Map<User>(userUpdate);

            if (!_userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong updating User");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


    }
}
