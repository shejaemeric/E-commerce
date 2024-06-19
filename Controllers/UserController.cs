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
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;


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
        public UserController(IUserRepository userRepository, IMapper mapper, IRoleRepository roleRepository, IPermissionRepository permissionRepository)
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
        [SwaggerOperation(Summary = "Create a User (Admin/Manager)")]
        public IActionResult CreateUser([FromBody] CreateUserDto userCreate)
        {
            if (userCreate == null)
            {
                return BadRequest(ModelState);
            }

            var user = _userRepository.GetAllUsers().Where(c => c.Username == userCreate.Username).FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("", "Username Already Exists");
                return StatusCode(422, ModelState);
            }
            user = _userRepository.GetAllUsers().Where(c => c.Email == userCreate.Email).FirstOrDefault();
                        if (user != null)
            {
                ModelState.AddModelError("", "Email Already Exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(userCreate);
            userMap.RoleId = 3;

            var token = _userRepository.CreateUser(userMap);
            if (token == "")
            {
                ModelState.AddModelError("", "Error Occured While Trying To Save");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(400)]


        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Get All Users (Admin/Manager)")]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetAllUsers());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(users);
        }



        [HttpPost("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        [Authorize(Policy = "Admin/Manager/Owner")]
        [SwaggerOperation(Summary = "Get Only one User by Id (Admin/Manager/Owner)")]
        public IActionResult GetOneUser(int userId)
        {

            if (!_userRepository.CheckIfUserExist(userId))
            {
                return NotFound();
            }

            var user = _mapper.Map<UserDto>(_userRepository.GetOneUsers(userId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }


        [HttpGet("{userId}/addresses")]
        [ProducesResponseType(200, Type = typeof(ICollection<UserAddress>))]
        [ProducesResponseType(400)]

        [Authorize(Policy = "Admin/Manager/Owner")]
        [SwaggerOperation(Summary = "Get Addresses of a User (Admin/Manager/Owner)")]
        public IActionResult GetAddress(int userId)
        {
            if (!_userRepository.CheckIfUserExist(userId))
            {
                return NotFound();
            }

            var userAdresses = _mapper.Map<List<UserAddressDto>>(_userRepository.GetAllUserAddressesByUser(userId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(userAdresses);
        }

        [HttpGet("{userId}/payments")]
        [ProducesResponseType(200, Type = typeof(ICollection<UserPayment>))]
        [ProducesResponseType(400)]

        [Authorize(Policy = "Admin/Manager/Owner")]
        [SwaggerOperation(Summary = "Get Payments infos of a User (Admin/Manager/Owner)")]
        public IActionResult GetPaymentDetailsByUser(int userId)
        {
            if (!_userRepository.CheckIfUserExist(userId))
            {
                return NotFound();
            }

            var paymentDetails = _mapper.Map<List<UserPaymentDto>>(_userRepository.GetAllUserPaymentsByUser(userId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(paymentDetails);
        }

        [HttpPost("{userId}/orderdetails")]
        [Authorize(Policy = "Admin/Manager/Owner")]
        [ProducesResponseType(200, Type = typeof(ICollection<OrderDetail>))]
        [ProducesResponseType(400)]
        [SwaggerOperation(Summary = "Get All Order Details from a User (Admin/Manager/Owner)")]
        public IActionResult GetAllOrderDetailsByUser(int userId)
        {
            if (!_userRepository.CheckIfUserExist(userId))
            {
                return NotFound();
            }
            var orderDetails = _mapper.Map<List<OrderDetailsDto>>(_userRepository.GetAllOrderByUser(userId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(orderDetails);
        }

        [HttpGet("{UserId}/latestShoppingSession")]
        [ProducesResponseType(200, Type = typeof(ShoppingSession))]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager/Owner")]
        [SwaggerOperation(Summary = "Get Latest Shopping Session of a Users (Admin/Manager/Owner)")]

        public IActionResult GetLatestShoppingSession(int UserId)
        {
            if (!_userRepository.CheckIfUserExist(UserId))
            {
                return NotFound();
            }
            var session = _mapper.Map<ShoppingSessionDto>(_userRepository.GetLatestShoppingSessionByUser(UserId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(session);
        }


        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Policy = "Admin/Manager/Owner")]
        [SwaggerOperation(Summary = "Update a user (Admin/Manager/Owner) ")]
        public IActionResult UpdateUser(int userId, [FromBody] CreateUserDto userUpdate)
        {
            if (userUpdate == null)
                return BadRequest(ModelState);

            if (userId != userUpdate.Id)
                return BadRequest(ModelState);

            var actionPeformerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
               return StatusCode(405, "User not Allowed/Authenticated");
            }

            var user = _userRepository.GetAllUsers().Where(c => c.Username ==  userUpdate.Username).FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("", "Username Already Exists");
                return StatusCode(422, ModelState);
            }
            user = _userRepository.GetAllUsers().Where(c => c.Email ==  userUpdate.Email).FirstOrDefault();
                        if (user != null)
            {
                ModelState.AddModelError("", "Email Already Exists");
                return StatusCode(422, ModelState);
            }



            var existingUser = _userRepository.GetOneUsers(userId); // Method to get the user by ID
            if (existingUser == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            _mapper.Map(userUpdate, existingUser);

            var userMap = _mapper.Map<User>(userUpdate);

            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_userRepository.UpdateUser(userMap, actionPeformerId, referenceId))
            {
                ModelState.AddModelError("", "Something went wrong updating User");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpPut("{userId}/user-role")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Change User's Role (Admin/Manager) ")]
        public IActionResult UpdateUserRole(int userId,[FromBody]int roleId)
        {
            if (!_roleRepository.CheckIfRoleExist(roleId))
                return NotFound();

            if (!_userRepository.CheckIfUserExist(userId))
                return NotFound();

            var actionPeformerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
               return StatusCode(405, "User not Allowed/Authenticated");
            }


            if (!_userRepository.UpdateUserRole(userId,roleId))
            {
                ModelState.AddModelError("", "Something went wrong updating User Role");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }



        [HttpDelete("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        [Authorize(Policy = "Admin/Manager/Owner")]
        [SwaggerOperation(Summary = "Delete a user (Admin/Manager/Owner)")]
        public IActionResult DeleteUser(int userId)
        {


            if (!_userRepository.CheckIfUserExist(userId))
            {
                return NotFound();
            }
            var actionPeformerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
               return StatusCode(405, "User not Allowed/Authenticated");
            }


            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_userRepository.DeleteUser(userId, actionPeformerId, referenceId))
            {
                ModelState.AddModelError("", "Error Occured While Trying To Delete User");
                return StatusCode(500, ModelState);

            }
            return Ok("User Deleted Successfully");
        }

        //     [HttpGet("{userId}/password-reset-token/unexpired")]
        //     [ProducesResponseType(200,Type = typeof(ICollection<PasswordResetToken>))]
        //     [ProducesResponseType(400)]
        //     [Authorize(Policy = "Admin/Manager/Owner")]
        //     [SwaggerOperation(Summary = "Get Get Unexpired Password Tokens of a User (Admin/Manager/Owner) ❌")]
        //     public IActionResult GetUnexpiredPasswordTokensByUser(int userId)
        //     {

        //         var passwordResetTokens= _mapper.Map<List<PasswordResetTokenDto>>(_userRepository.GetUnexpiredPasswordResetTokensByUser(userId));

        //         if (!ModelState.IsValid){
        //             return BadRequest(ModelState);
        //         }
        //         return Ok(passwordResetTokens);
        //     }


        // }
    }
}
