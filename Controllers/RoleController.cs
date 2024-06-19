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
    public class RoleController : ControllerBase
    {

        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public RoleController(IRoleRepository roleRepository,IMapper mapper,IUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpPost()]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        [Authorize(policy:"Admin")]
        [SwaggerOperation(Summary = "Create A Role (Admin)")]
        public IActionResult CreateRole([FromBody] RoleDto roleCreate) {
            if (roleCreate == null)
                return BadRequest(ModelState);

            var role = _roleRepository.GetAllRoles().Where(c => c.Name == roleCreate.Name).FirstOrDefault();

            if (role != null) {
                ModelState.AddModelError("", "Role Already Exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var roleMap = _mapper.Map<Role>(roleCreate);

            if (!_roleRepository.CreateRole(roleMap)) {
                ModelState.AddModelError("", "Error Occured While Trying To Save");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }


        [HttpGet()]
        [ProducesResponseType(200,Type = typeof(ICollection<Role>))]
        [ProducesResponseType(400)]
        [SwaggerOperation(Summary = "Get All Roles (Admin)")]
        public IActionResult GetAllRole()
        {

            var roles= _mapper.Map<List<RoleDto>>(_roleRepository.GetAllRoles());

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(roles);
        }


        [HttpPost("{roleId}")]
        [ProducesResponseType(200,Type = typeof(ICollection<Role>))]
        [ProducesResponseType(400)]
        [Authorize(policy:"Admin")]
        [SwaggerOperation(Summary = "Create One Role By Id (Admin)")]
        public IActionResult GetOneRole(int roleId)
        {
            if(! _roleRepository.CheckIfRoleExist(roleId)){
                return NotFound();
            }
            var Role= _mapper.Map<RoleDto>(_roleRepository.GetOneRole(roleId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(Role);
        }

        [HttpPut("{roleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        [Authorize(policy:"Admin")]
        [SwaggerOperation(Summary = "Update a Role (Admin)")]
        public IActionResult UpdateRole(int roleId,[FromBody] RoleDto roleUpdate)
        {
            if (roleUpdate == null)
                return BadRequest(ModelState);

            if (roleId != roleUpdate.Id)
                return BadRequest(ModelState);

            if (!_roleRepository.CheckIfRoleExist(roleId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var roleMap = _mapper.Map<Role>(roleUpdate);

            if (!_roleRepository.UpdateRole(roleMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Role");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{roleId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin")]

        [SwaggerOperation(Summary = "Delete a Role (Admin)")]
        public IActionResult DeleteRole(int roleId) {
            if(!_roleRepository.CheckIfRoleExist(roleId)){
                return NotFound();
            }
            var actionPeformerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
               return StatusCode(405, "User not Allowed/Authenticated");
            }


            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_roleRepository.DeleteRole(roleId, actionPeformerId, referenceId)) {
                ModelState.AddModelError("", "Error Occured While Trying To Delete Role");
                return StatusCode(500, ModelState);

            }
            return Ok("Role Deleted Successfully");
         }

        [HttpGet("{roleId}/users")]
        [ProducesResponseType(200,Type = typeof(ICollection<User>))]
        [ProducesResponseType(400)]

        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Get All users Based by Role (Admin/Manager)")]
        public IActionResult GetAllUsersWithRole(int roleId)
        {
            if(!_roleRepository.CheckIfRoleExist(roleId)){
                return NotFound();
            }

            var users = _mapper.Map<List<UserDto>>(_roleRepository.GetAllUsersByRole(roleId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(users);
        }



    }
}
