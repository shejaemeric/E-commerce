using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce_Api.Dto;
using E_Commerce_Api.Interfaces;
using E_Commerce_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace E_Commerce_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;


        private readonly IMapper _mapper;

        public AuthenticationController(IUserRepository userRepository, IMapper mapper,IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;

        }

            [HttpPost("register")]
            [ProducesResponseType(200)]
            [ProducesResponseType(400)]

            public IActionResult Register([FromBody] CreateUserDto userRegister) {
                if (_userRepository.GetAllUsers().Any(c => c.Username == userRegister.Username)) {
                    ModelState.AddModelError("", "Username Already Exists");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }
                 userRegister.Password = BCrypt.Net.BCrypt.HashPassword(userRegister.Password);
                 var userRegisterMap = _mapper.Map<User>(userRegister);
                 userRegisterMap.RoleId = 3;

                 if(!_userRepository.CreateUser(userRegisterMap)) {
                    return BadRequest(ModelState);
                 }

                  return Ok(GenerateJwtToken(userRegisterMap));
            }

            [HttpPost("login")]
            [ProducesResponseType(200)]
            [ProducesResponseType(400)]

            public IActionResult Login([FromBody] UserLoginDto userlogin) {
                if (!_userRepository.GetAllUsers().Any(c => c.Username == userlogin.Username)) {
                    ModelState.AddModelError("", "User Not Found");
                    return NotFound(ModelState);
                }
                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                 }
                 var user = _userRepository.GetAllUsers().Where(c => c.Username == userlogin.Username).FirstOrDefault();

                 if (!BCrypt.Net.BCrypt.Verify(userlogin.Password, user.Password)) {
                    ModelState.AddModelError("", "Username or Password is incorrect");
                    return BadRequest(ModelState);
                 }
                 return Ok(GenerateJwtToken(user));
            }

            private string GenerateJwtToken(User user)
            {
            string[] roles = new string[4];
            roles[1] = "Admin";
            roles[2] = "Manager";
            roles[3] = "User";
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(user.Username);
            Console.WriteLine(user.RoleId);

            Console.WriteLine(_configuration);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, (user.Id).ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, roles[user.RoleId]),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:ExpirationInDays"]));
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: expires,
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            }

}
