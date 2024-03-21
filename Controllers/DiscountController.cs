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
    public class DiscountController : ControllerBase
    {

        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        public DiscountController(IDiscountRepository discountRepository,IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        [HttpPost()]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
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

            var discountMap = _mapper.Map<Discount>(discountUpdate);

            if (!_discountRepository.UpdateDiscount(discountMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Discount");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


    }
}
