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
    public class PaymentDetailsController : ControllerBase
    {
        // PaymentDetail GetOnePaymentDetails(int paymentId);
        // bool CheckIfPaymentDetailExist(int paymentDetailId);

        private readonly IPaymentDetailsRepository _paymentDetailsRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public PaymentDetailsController(IUserRepository userRepository,IPaymentDetailsRepository paymentDetailsRepository,IMapper mapper)
        {
            _paymentDetailsRepository = paymentDetailsRepository;
            _mapper=mapper;
             _userRepository = userRepository;
        }


        [HttpPost()]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [SwaggerOperation(Summary = "Create One Payment Details (Anyone)")]
        public IActionResult CreatePaymentDetails([FromBody] PaymentDetailsDto paymentDetailCreate) {
            if (paymentDetailCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var paymentDetailMap = _mapper.Map<PaymentDetail>(paymentDetailCreate);

            if (!_paymentDetailsRepository.CreatePaymentDetails(paymentDetailMap)) {
                ModelState.AddModelError("", "Error Occured While Trying To Save");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }

        [HttpGet()]
        [ProducesResponseType(200,Type = typeof(ICollection<PaymentDetail>))]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager")]
        public IActionResult GetAllPaymentDetails()
        {

            var paymentDetails= _mapper.Map<List<PaymentDetailsDto>>(_paymentDetailsRepository.GetAllPaymentDetails());

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(paymentDetails);
        }

        [HttpPost("{paymentDetailId}")]
        [ProducesResponseType(200,Type = typeof(PaymentDetail))]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Get One Payment Detail (Admin/Manager)")]
        public IActionResult GetOnePaymentDetail(int paymentDetailId)
        {
            if(! _paymentDetailsRepository.CheckIfPaymentDetailExist(paymentDetailId)){
                return NotFound();
            }

            var paymentDetail= _mapper.Map<PaymentDetailsDto>(_paymentDetailsRepository.GetOnePaymentDetails(paymentDetailId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(paymentDetail);
        }


        [HttpPut("{paymentDetailId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Policy = "Admin")]
        [SwaggerOperation(Summary = "Update One Payment Detail (Admin/Manager)")]
        public IActionResult UpdatePaymentDetail(int paymentDetailId,[FromBody] PaymentDetailsDto paymentDetailUpdate)
        {
            if (paymentDetailUpdate == null)
                return BadRequest(ModelState);

            if (paymentDetailId != paymentDetailUpdate.Id)
                return BadRequest(ModelState);

            if (!_paymentDetailsRepository.CheckIfPaymentDetailExist(paymentDetailId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var actionPeformerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
               return StatusCode(405, "User not Allowed/Authenticated");
            }

            var paymentDetailMap = _mapper.Map<PaymentDetail>(paymentDetailUpdate);

            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_paymentDetailsRepository.UpdatePaymentDetails(paymentDetailMap,actionPeformerId,referenceId))
            {
                ModelState.AddModelError("", "Something went wrong updating Payment Detail");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }




    }
}
