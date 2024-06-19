using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using E_Commerce_Api.Interfaces;
using E_Commerce_Api.Models;
using E_Commerce_Api.Dto;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using System.Text.Json;

namespace E_Commerce_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
        [SwaggerOperation(Summary = "Create a User Payment (Anyone)")]
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


        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<UserPayment>))]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Get All Users Payments(Admin/Manager)")]
        public IActionResult GetUserPaymentes()
        {
            var userPaymentes = _mapper.Map<List<UserPaymentDto>>(_userPaymentRepository.GetAllUserPayments());
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(userPaymentes);
        }

        [HttpPost("{UserPaymentId}")]
        [ProducesResponseType(200,Type = typeof(UserPayment))]
        [ProducesResponseType(400)]

        [Authorize(Policy = "Admin/Manager/Owner")]
        [SwaggerOperation(Summary = "Get One User Payment(Admin/Manager/Owner)")]
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



        [HttpPut("{userPaymentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        [Authorize(Policy = "Admin/Manager/Owner")]
        [SwaggerOperation(Summary = "Update One User Payment(Admin/Manager/Owner)")]
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

            var actionPeformerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
               return StatusCode(405, "User not Allowed/Authenticated");
            }

            var userPaymentMap = _mapper.Map<UserPayment>(userPaymentUpdate);

            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_userPaymentRepository.UpdateUserPayment(userId,userPaymentMap,actionPeformerId,referenceId))
            {
                ModelState.AddModelError("", "Something went wrong updating User Payment");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }




        [HttpDelete("{userPaymentId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        [Authorize(Policy = "Admin/Manager/Owner")]
        [SwaggerOperation(Summary = "Delete One User Payment(Admin/Manager/Owner)")]
        public IActionResult DeleteUserPayment(int userPaymentId) {
            if(!_userPaymentRepository.CheckIfUserPaymentExist(userPaymentId)){
                return NotFound();
            }
            var actionPeformerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
               return StatusCode(405, "User not Allowed/Authenticated");
            }

            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_userPaymentRepository.DeleteUserPayment(userPaymentId, actionPeformerId, referenceId)) {
                ModelState.AddModelError("", "Error Occured While Trying To Delete User Payment");
                return StatusCode(500, ModelState);

            }
            return Ok("User Payment Deleted Successfully");
         }

    }
}
