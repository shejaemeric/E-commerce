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
        public PaymentDetailsController(IPaymentDetailsRepository paymentDetailsRepository,IMapper mapper)
        {
            _paymentDetailsRepository = paymentDetailsRepository;
            _mapper=mapper;
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
        public IActionResult UpdatePaymentDetail(int paymentDetailId,[FromBody] PaymentDetailsDto paymentDetailUpdate,[FromQuery] int actionPeformerId)
        {
            if (paymentDetailUpdate == null)
                return BadRequest(ModelState);

            if (paymentDetailId != paymentDetailUpdate.Id)
                return BadRequest(ModelState);

            if (!_paymentDetailsRepository.CheckIfPaymentDetailExist(paymentDetailId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

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
