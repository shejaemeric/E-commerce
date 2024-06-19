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
    public class OrderDetailController : ControllerBase
    {

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
        [SwaggerOperation(Summary = "Create an Order Detail (Anyone)")]
        public IActionResult CreateOrderDetail([FromQuery] int paymentDetailId,[FromQuery] int userId,[FromBody] OrderDetailsDto orderDetailCreate) {

            if (orderDetailCreate == null)
                return BadRequest(ModelState);

            if(!_paymentDetailsRepository.CheckIfPaymentDetailExist(paymentDetailId)){
                return NotFound(new {errorMessage = "Payment details not found"});
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
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Get All Order Details (Admin/Manager)")]
        public IActionResult GetAllOrderDetails()
        {

            var orderDetails= _mapper.Map<List<OrderDetailsDto>>(_orderDetailRepository.GetAllOrders());

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(orderDetails);
        }



        [HttpPost("{orderDetailId}")]
        [ProducesResponseType(200,Type = typeof(OrderDetail))]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager/Owner")]
        [SwaggerOperation(Summary = "Get One Order Details (Admin/Manager/Owner)")]
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

        [HttpPost("{orderDetailId}/orderItems")]
        [ProducesResponseType(200,Type=typeof(ICollection<OrderItem>))]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager/Owner")]
        [SwaggerOperation(Summary = "Get All Order Items from Order Details (Admin/Manager/Owner)")]
        public IActionResult GetAlllOrderItemByOrderDetail(int orderDetailId)
        {
            if(! _orderDetailRepository.CheckIfOrderDetailExist(orderDetailId)){
                return NotFound();
            }

            var orderItems= _mapper.Map<List<OrderItem>>(_orderDetailRepository.GetAllOrderItemsByOrder(orderDetailId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(orderItems);
        }

        [HttpPut("{orderDetailId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Update One Order Detail (Admin/Manager)")]
        public IActionResult UpdateOrderDetail(int orderDetailId,[FromQuery] int paymentDetailId,[FromQuery] int userId,[FromBody] OrderDetailsDto orderDetailUpdate) {

            if (orderDetailId != orderDetailUpdate.Id) {
                return BadRequest(ModelState);
            }

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


            var actionPeformerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
               return StatusCode(405, "User not Allowed/Authenticated");
            }

            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            var orderDetailMap = _mapper.Map<OrderDetail>(orderDetailUpdate);


            if (!_orderDetailRepository.UpdateOrderDetail(userId,paymentDetailId,orderDetailMap,actionPeformerId,referenceId)) {
                ModelState.AddModelError("", "Error Occured While Trying To Update");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{orderDetailId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Delete One Order Detail (Admin/Manager)")]
        public IActionResult DeleteOrderDetails(int orderDetailId,[FromQuery] int actionPeformerId) {
            if(!_orderDetailRepository.CheckIfOrderDetailExist(orderDetailId)){
                return NotFound();
            }
            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
                return NotFound();
            }

            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_orderDetailRepository.DeleteOrderDetail(orderDetailId, actionPeformerId, referenceId)) {
                ModelState.AddModelError("", "Error Occured While Trying To Delete");
                return StatusCode(500, ModelState);

            }
            return Ok("Order Detail deleted successfully");
         }
    }
}
