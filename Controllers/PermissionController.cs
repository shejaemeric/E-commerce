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
    public class PermissionController : ControllerBase
    {

        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;
        public PermissionController(IPermissionRepository permissionRepository,IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        [HttpPost()]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePermission([FromBody] PermissionDto permissionCreate) {
            if (permissionCreate == null)
                return BadRequest(ModelState);

            var permission = _permissionRepository.GetAllPermissions().Where(c => c.Name == permissionCreate.Name).FirstOrDefault();

            if (permission != null) {
                ModelState.AddModelError("", "Permission Already Exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var permissionMap = _mapper.Map<Permission>(permissionCreate);

            if (!_permissionRepository.CreatePermission(permissionMap)) {
                ModelState.AddModelError("", "Error Occured While Trying To Save");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }


        [HttpGet()]
        [ProducesResponseType(200,Type = typeof(ICollection<Permission>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllPermission()
        {

            var permissions= _mapper.Map<List<PermissionDto>>(_permissionRepository.GetAllPermissions());

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(permissions);
        }


        [HttpPost("{permissionId}")]
        [ProducesResponseType(200,Type = typeof(ICollection<Permission>))]
        [ProducesResponseType(400)]
        public IActionResult GetOnePermission(int permissionId)
        {
            if(! _permissionRepository.CheckIfPermissionExist(permissionId)){
                return NotFound();
            }
            var Permission= _mapper.Map<PermissionDto>(_permissionRepository.GetOnePermission(permissionId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(Permission);
        }

        [HttpPut("{permissionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePermission(int permissionId,[FromBody] PermissionDto permissionUpdate)
        {
            if (permissionUpdate == null)
                return BadRequest(ModelState);

            if (permissionId != permissionUpdate.Id)
                return BadRequest(ModelState);

            if (!_permissionRepository.CheckIfPermissionExist(permissionId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var permissionMap = _mapper.Map<Permission>(permissionUpdate);

            if (!_permissionRepository.UpdatePermission(permissionMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Permission");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


    }
}
