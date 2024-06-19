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
    public class DiscountController : ControllerBase
    {

        private readonly IDiscountRepository _discountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public DiscountController(IDiscountRepository discountRepository,IMapper mapper,IUserRepository userRepository)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpPost()]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Create a discount (Admin/Manager)")]
        public IActionResult CreateDiscount([FromBody] DiscountDto discountCreate) {
            if (discountCreate == null)
                return BadRequest(ModelState);

            var discount = _discountRepository.GetAllDiscounts().Where(c => c.Name == discountCreate.Name).FirstOrDefault();

            if (discount != null) {
                ModelState.AddModelError("", "Discount Already Exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var discountMap = _mapper.Map<Discount>(discountCreate);

            if (!_discountRepository.CreateDiscount(discountMap)) {
                ModelState.AddModelError("", "Error Occured While Trying To Save");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }


        [HttpGet()]
        [ProducesResponseType(200,Type = typeof(ICollection<Discount>))]
        [ProducesResponseType(400)]
        [SwaggerOperation(Summary = "Get All Discounts (Anyone)")]
        public IActionResult GetAllDiscount()
        {

            var discounts= _mapper.Map<List<DiscountDto>>(_discountRepository.GetAllDiscounts());

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(discounts);
        }


        [HttpPost("{discountId}")]
        [ProducesResponseType(200,Type = typeof(ICollection<Discount>))]
        [ProducesResponseType(400)]
        [SwaggerOperation(Summary = "Get One Discount (Anyone)")]
        public IActionResult GetOneDiscount(int discountId)
        {
            if(! _discountRepository.CheckIfDiscountExist(discountId)){
                return NotFound();
            }
            var Discount= _mapper.Map<DiscountDto>(_discountRepository.GetOneDiscount(discountId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(Discount);
        }

        [HttpPut("{discountId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Update One Discount (Admin/Manager)")]
        public IActionResult UpdateDiscount(int discountId,[FromBody] DiscountDto discountUpdate)
        {
            if (discountUpdate == null)
                return BadRequest(ModelState);

            if (discountId != discountUpdate.Id)
                return BadRequest(ModelState);

            if (!_discountRepository.CheckIfDiscountExist(discountId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var actionPeformerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
               return StatusCode(405, "User not Allowed/Authenticated");
            }

            var discountMap = _mapper.Map<Discount>(discountUpdate);

            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_discountRepository.UpdateDiscount(discountMap,actionPeformerId,referenceId))
            {
                ModelState.AddModelError("", "Something went wrong updating Discount");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{discountId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Delete One Discount (Admin/Manager)")]
        public IActionResult DeleteDiscount(int discountId,[FromQuery] int actionPeformerId) {
            if(!_discountRepository.CheckIfDiscountExist(discountId)){
                return NotFound();
            }

            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
                return NotFound();
            }

            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_discountRepository.DeleteDiscount(discountId, actionPeformerId, referenceId)) {
                ModelState.AddModelError("", "Error Occured While Trying To Delete Discount");
                return StatusCode(500, ModelState);

            }
            return Ok("Discount Deleted Successfully");
         }


    }
}
