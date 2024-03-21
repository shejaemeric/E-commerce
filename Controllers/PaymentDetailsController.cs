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
        public IActionResult GetAllPaymentDetails()
        {

            var paymentDetails= _mapper.Map<List<PaymentDetailsDto>>(_paymentDetailsRepository.GetAllPaymentDetails());

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(paymentDetails);
        }

        [HttpPost("{paymentId}")]
        [ProducesResponseType(200,Type = typeof(PaymentDetail))]
        [ProducesResponseType(400)]
        public IActionResult GetOnePaymentDetail(int paymentId)
        {
            if(! _paymentDetailsRepository.CheckIfPaymentDetailExist(paymentId)){
                return NotFound();
            }

            var paymentDetail= _mapper.Map<PaymentDetailsDto>(_paymentDetailsRepository.GetOnePaymentDetails(paymentId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(paymentDetail);
        }


        [HttpPut("{paymentDetailId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
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

            var paymentDetailMap = _mapper.Map<PaymentDetail>(paymentDetailUpdate);

            if (!_paymentDetailsRepository.UpdatePaymentDetails(paymentDetailMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Payment Detail");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }




    }
}
