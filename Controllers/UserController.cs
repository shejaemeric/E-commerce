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
using Microsoft.AspNetCore.Authorization;


namespace E_Commerce_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository,IMapper mapper,IRoleRepository roleRepository,IPermissionRepository permissionRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _permissionRepository = permissionRepository;
            _roleRepository = roleRepository;
        }

        [HttpPost()]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        [Authorize(Policy = "Admin/Manager")]
        public IActionResult CreateUser([FromBody] CreateUserDto userCreate) {
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
            userMap.RoleId = 3;


            if (!_userRepository.CreateUser(userMap)) {
                ModelState.AddModelError("", "Error Occured While Trying To Save");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }

        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<User>))]
        [ProducesResponseType(400)]


        [Authorize(Policy = "Admin/Manager")]
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

        [Authorize(Policy = "Admin/Manager/Owner")]
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

        [Authorize(Policy = "Admin/Manager/Owner")]
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

        [Authorize(Policy = "Admin/Manager/Owner")]
        public IActionResult GetPaymentDetailsByUser(int userId)
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
        [Authorize(Policy = "Admin/Manager/Owner")]
        public IActionResult UpdateUser(int userId,[FromBody] CreateUserDto userUpdate,[FromQuery] int actionPeformerId)
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

            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_userRepository.UpdateUser(userMap,actionPeformerId,referenceId))
            {
                ModelState.AddModelError("", "Something went wrong updating User");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        [Authorize(Policy = "Admin/Manager/Owner")]
        public IActionResult DeleteUser(int actionPeformerId, int userId) {
            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
                return NotFound();
            }

            if(!_userRepository.CheckIfUserExist(userId)){
                return NotFound();
            }

            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_userRepository.DeleteUser(userId,actionPeformerId, referenceId)) {
                ModelState.AddModelError("", "Error Occured While Trying To Delete User");
                return StatusCode(500, ModelState);

            }
            return Ok("User Deleted Successfully");
         }

        [HttpGet("roles/{roleId}")]
        [ProducesResponseType(200,Type = typeof(ICollection<User>))]
        [ProducesResponseType(400)]

        [Authorize(policy:"AdminOnly")]
        [Authorize(policy:"ManagerOnly")]

        [Authorize(Policy = "Admin/Manager")]
        public IActionResult GetAllUsersWithRole(int roleId)
        {
            if(!_roleRepository.CheckIfRoleExist(roleId)){
                return NotFound();
            }

            var users = _mapper.Map<List<UserDto>>(_userRepository.GetAllUsersByRole(roleId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(users);
        }


        [HttpGet("permissions/{permissionId}")]
        [ProducesResponseType(200,Type = typeof(ICollection<User>))]
        [ProducesResponseType(400)]

        [Authorize(Policy = "Admin/Manager")]

        public IActionResult GetAllUsersWithPermission(int permissionId)
        {
            if(!_permissionRepository.CheckIfPermissionExist(permissionId)){
                return NotFound();
            }

            var users = _mapper.Map<List<UserDto>>(_userRepository.GetAllUsersByPermission(permissionId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(users);
        }

    }
}
