using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce_Api.Data;
using E_Commerce_Api.Dto;
using E_Commerce_Api.Interfaces;
using E_Commerce_Api.Models;
using E_Commerce_Api.Services;
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

        private readonly IEmailService _emailService;
        private readonly ITokenServices _tokenServices;


        private readonly IMapper _mapper;

        public readonly DataContext _context;



        public AuthenticationController(ITokenServices tokenService,DataContext context,ITokenServices tokenServices,IEmailService emailService,IUserRepository userRepository, IMapper mapper,IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
            _emailService = emailService;
            _context = context;
            _tokenServices = tokenServices;

        }

            [HttpPost("register")]
            [ProducesResponseType(200)]
            [ProducesResponseType(400)]

            public IActionResult Register([FromBody] CreateUserDto userRegister) {
                if(_userRepository.GetAllUsers().Any(c => c.Username == userRegister.Username)) {
                    ModelState.AddModelError("Error", "Username Already Exists");
                    return BadRequest(ModelState);
                }

                if(_userRepository.GetAllUsers().Any(c => c.Email == userRegister.Email)) {
                    ModelState.AddModelError("error", "Email Already Exists");
                    return BadRequest(ModelState);
                }


                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }
                 userRegister.Password = BCrypt.Net.BCrypt.HashPassword(userRegister.Password);
                 var userRegisterMap = _mapper.Map<User>(userRegister);
                 userRegisterMap.RoleId = 3;

                 userRegisterMap.Verified = false;

                 var token = _userRepository.CreateUser(userRegisterMap);

                 if(token == "") {
                    return BadRequest(ModelState);
                 }

                _emailService.SendVerificationEmail(userRegisterMap,token);
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

                if (!BCrypt.Net.BCrypt.Verify(userlogin.Password, user?.Password)) {
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
                [HttpGet("comfirmEmail")]
            [ProducesResponseType(200)]
            [ProducesResponseType(400)]

            public IActionResult ComfirmEmail(string UserEmail,string Token) {

                var user = _userRepository.GetAllUsers().Where(c => c.Email == UserEmail).FirstOrDefault();
                if(user == null || Token == null || Token != user.Verification_Token) {
                    ModelState.AddModelError("", "Invalid Link");
                    return NotFound(ModelState);
                }
                    user.Verification_Token = null;
                    user.VerifiedAt = DateTime.Now;
                    user.Verified = false;
                    _context.SaveChanges();
                return Ok("Verification Completed Successfully");
            }


            }
            }




