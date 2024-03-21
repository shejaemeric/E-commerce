using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using E_Commerce_Api.Interfaces;
using E_Commerce_Api.Models;
using E_Commerce_Api.Dto;

namespace E_Commerce_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserPaymentController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IUserPaymentRepository _userPaymentRepository;
        public readonly IUserRepository _userRepository;

        public UserPaymentController(IMapper mapper, IUserPaymentRepository userPaymentRepository,IUserRepository userRepository)
        {
            _mapper = mapper;
            _userPaymentRepository = userPaymentRepository;
            _userRepository = userRepository;

        }


        [HttpPost()]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUserPayment([FromQuery] int userId,[FromBody] UserPaymentDto userPaymentCreate) {
            if (userPaymentCreate == null)
                return BadRequest(ModelState);

            if(!_userRepository.CheckIfUserExist(userId)){
                return NotFound();
            }

            var userPayment = _userPaymentRepository.GetAllUserPaymentByUser(userId).Where(c => c.Card_Number == userPaymentCreate.Card_Number).FirstOrDefault();
            if (userPayment != null) {
                ModelState.AddModelError("", "User Payment Already Exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userPaymentMap = _mapper.Map<UserPayment>(userPaymentCreate);

            if (!_userPaymentRepository.CreateUserPayment(userId,userPaymentMap)) {
                ModelState.AddModelError("", "Error Occured While Trying To save");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }

        [HttpPut("{userPaymentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUserPayment(int userPaymentId,[FromQuery] int userId,[FromBody] UserPaymentDto userPaymentUpdate)
        {
            if (userPaymentUpdate == null)
                return BadRequest(ModelState);

            if (userPaymentId != userPaymentUpdate.Id)
                return BadRequest(ModelState);

            if (!_userPaymentRepository.CheckIfUserPaymentExist(userPaymentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var userPaymentMap = _mapper.Map<UserPayment>(userPaymentUpdate);

            if (!_userPaymentRepository.UpdateUserPayment(userId,userPaymentMap))
            {
                ModelState.AddModelError("", "Something went wrong updating User Payment");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPost("{UserPaymentId}")]
        [ProducesResponseType(200,Type = typeof(UserPayment))]
        [ProducesResponseType(400)]

        public IActionResult GetOneUserPayment(int UserPaymentId)
        {
            if(!_userPaymentRepository.CheckIfUserPaymentExist(UserPaymentId)){
                return NotFound();
             }
            var paymentDetail = _mapper.Map<UserPaymentDto>(_userPaymentRepository.GetOneUserPayment(UserPaymentId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(paymentDetail);
        }


        [HttpGet("user/{userId}")]
        [ProducesResponseType(200,Type = typeof(UserPayment))]
        [ProducesResponseType(400)]

        public IActionResult GetAllUserPaymentByUser(int userId)
        {
            if(!_userRepository.CheckIfUserExist(userId)){
                return NotFound();
             }
            var paymentDetails = _mapper.Map<List<UserPaymentDto>>(_userPaymentRepository.GetAllUserPaymentByUser(userId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(paymentDetails);
        }
    }
}
