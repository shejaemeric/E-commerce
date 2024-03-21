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
    public class OrderDetailController : ControllerBase
    {

        // ICollection<OrderDetail> GetAllOrderByUser(int userId);
        // OrderDetail GetOneOrder(int orderId);

        // ICollection<OrderDetail> GetAllOrderDetailsByOrder(int orderId);
        // bool CheckIfOrderDetailExist(int orderDetailId);

        private readonly IOrderDetailRepository _orderDetailRepository;

        private readonly IUserRepository _userRepository;

        private readonly IPaymentDetailsRepository _paymentDetailsRepository;
        private readonly IMapper _mapper;
        public OrderDetailController(IMapper mapper,IOrderDetailRepository orderDetailRepository,IUserRepository userRepository,IPaymentDetailsRepository paymentDetailsRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper=mapper;
            _userRepository = userRepository;
            _paymentDetailsRepository = paymentDetailsRepository;
        }

        [HttpPost()]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOrderDetail([FromQuery] int paymentDetailId,[FromQuery] int userId,[FromBody] OrderDetailsDto orderDetailCreate) {
            if (orderDetailCreate == null)
                return BadRequest(ModelState);

            if(!_paymentDetailsRepository.CheckIfPaymentDetailExist(paymentDetailId)){
                return NotFound(new {errorMessage = "Product not found"});
            }

            if(!_userRepository.CheckIfUserExist(userId)){
                return NotFound(new {errorMessage = "User not found"});
            }

            var orderDetailMap = _mapper.Map<OrderDetail>(orderDetailCreate);

            if (!_orderDetailRepository.CreateOrderDetail(userId,paymentDetailId,orderDetailMap)) {
                ModelState.AddModelError("", "Error Occured While Trying To save");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }

        [HttpGet()]
        [ProducesResponseType(200,Type = typeof(ICollection<OrderDetail>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllOrderDetails()
        {

            var orderDetails= _mapper.Map<List<OrderDetailsDto>>(_orderDetailRepository.GetAllOrders());

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(orderDetails);
        }


        [HttpPost("user/{userId}")]
        [ProducesResponseType(200,Type = typeof(ICollection<OrderDetail>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllOrderDetailsByUser(int userId)
        {
            if(! _userRepository.CheckIfUserExist(userId)){
                return NotFound();
            }
            var orderDetails= _mapper.Map<List<OrderDetailsDto>>(_orderDetailRepository.GetAllOrderByUser(userId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(orderDetails);
        }


        [HttpPost("{orderDetailId}")]
        [ProducesResponseType(200,Type = typeof(OrderDetail))]
        [ProducesResponseType(400)]
        public IActionResult GetOneOrderDetail(int orderDetailId)
        {
            if(! _orderDetailRepository.CheckIfOrderDetailExist(orderDetailId)){
                return NotFound();
            }

            var orderDetail= _mapper.Map<OrderDetailsDto>(_orderDetailRepository.GetOneOrderDetail(orderDetailId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(orderDetail);
        }

        [HttpPut("{orderDetailId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateOrderDetail(int orderDetailId,[FromQuery] int paymentDetailId,[FromQuery] int userId,[FromBody] OrderDetailsDto orderDetailUpdate) {
            if (orderDetailUpdate == null)
                return BadRequest(ModelState);
            if(orderDetailUpdate.Id == orderDetailId)

            if(!_orderDetailRepository.CheckIfOrderDetailExist(orderDetailId)){
                return NotFound(new {errorMessage = "Order Detail not found"});
            }

            if(!_paymentDetailsRepository.CheckIfPaymentDetailExist(paymentDetailId)){
                return NotFound(new {errorMessage = "Payment Detail not found"});
            }

            if(!_userRepository.CheckIfUserExist(userId)){
                return NotFound(new {errorMessage = "User not found"});
            }
            if (!ModelState.IsValid)
                return BadRequest();

            var orderDetailMap = _mapper.Map<OrderDetail>(orderDetailUpdate);

            if (!_orderDetailRepository.UpdateOrderDetail(userId,paymentDetailId,orderDetailMap)) {
                ModelState.AddModelError("", "Error Occured While Trying To Update");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
