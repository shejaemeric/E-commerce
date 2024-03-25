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
    public class PasswordResetTokenController : ControllerBase
    {

        private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
        private readonly IMapper _mapper;
        public PasswordResetTokenController(IPasswordResetTokenRepository passwordResetTokenRepository,IMapper mapper)
        {
            _passwordResetTokenRepository = passwordResetTokenRepository;
            _mapper = mapper;
        }
        [HttpPost()]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePasswordResetToken([FromBody] PasswordResetTokenDto passwordResetTokenCreate) {
            if (passwordResetTokenCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var passwordResetTokenMap = _mapper.Map<PasswordResetToken>(passwordResetTokenCreate);

            if (!_passwordResetTokenRepository.CreatePasswordResetToken(passwordResetTokenMap)) {
                ModelState.AddModelError("", "Error Occured While Trying To Save");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }


        [HttpGet("{userId}")]
        [ProducesResponseType(200,Type = typeof(ICollection<PasswordResetToken>))]
        [ProducesResponseType(400)]
        public IActionResult GetUnexpiredPasswordTokensByUser(int userId)
        {

            var passwordResetTokens= _mapper.Map<List<PasswordResetTokenDto>>(_passwordResetTokenRepository.GetUnexpiredPasswordResetTokensByUser(userId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(passwordResetTokens);
        }


        [HttpGet("{passwordResetTokenId}")]
        [ProducesResponseType(200,Type = typeof(ICollection<PasswordResetToken>))]
        [ProducesResponseType(400)]
        public IActionResult GetOnePasswordResetToken(int passwordResetTokenId)
        {
            if(! _passwordResetTokenRepository.CheckIfPasswordResetTokenExist(passwordResetTokenId)){
                return NotFound();
            }
            var PasswordResetToken= _mapper.Map<PasswordResetTokenDto>(_passwordResetTokenRepository.GetOnePasswordResetToken(passwordResetTokenId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(PasswordResetToken);
        }

        [HttpPut("{passwordResetTokenId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePasswordResetToken(int passwordResetTokenId,[FromBody] PasswordResetTokenDto passwordResetTokenUpdate)
        {
            if (passwordResetTokenUpdate == null)
                return BadRequest(ModelState);

            if (passwordResetTokenId != passwordResetTokenUpdate.Id)
                return BadRequest(ModelState);

            if (!_passwordResetTokenRepository.CheckIfPasswordResetTokenExist(passwordResetTokenId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var passwordResetTokenMap = _mapper.Map<PasswordResetToken>(passwordResetTokenUpdate);

            if (!_passwordResetTokenRepository.UpdatePasswordResetToken(passwordResetTokenMap))
            {
                ModelState.AddModelError("", "Something went wrong updating PasswordResetToken");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


    }
}
