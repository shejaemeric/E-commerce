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

namespace E_Commerce_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
     [Authorize]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public OrderItemController(IProductRepository productRepository,IUserRepository userRepository,IOrderItemRepository orderItemRepository,IMapper mapper,IOrderDetailRepository orderDetailRepository)
        {
            _orderItemRepository = orderItemRepository;
            _mapper=mapper;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        [HttpPost()]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOrderItem([FromQuery] int orderDetailId,[FromQuery] int productId,[FromBody] CreateOrderItemsDto orderItemCreate) {
            if (orderItemCreate == null)
                return BadRequest(ModelState);

            if(!_productRepository.CheckIfProductExist(productId)){
                return NotFound(new {errorMessage = "Product not found"});
            }


            if(!_orderDetailRepository.CheckIfOrderDetailExist(orderDetailId)){
                return NotFound(new {errorMessage = "Order Detail not found"});
            }

            var orderItemMap = _mapper.Map<OrderItem>(orderItemCreate);

            if (!_orderItemRepository.CreateOrderItem(productId,orderDetailId,orderItemMap)) {
                ModelState.AddModelError("", "Error Occured While Trying To save");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }

        [HttpGet()]
        [ProducesResponseType(200,Type = typeof(ICollection<OrderItem>))]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager")]
        public IActionResult GetAllOrderItems()
        {

            var orderItems= _mapper.Map<List<OrderItemsDto>>(_orderItemRepository.GetAllOrderItem());

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(orderItems);
        }

        [HttpPost("{orderItemId}")]
        [ProducesResponseType(200,Type=typeof(OrderItem))]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager/Owner")]
        public IActionResult GetOneOrderItem(int orderItemId)
        {
            if(! _orderItemRepository.CheckIfOrderItemExist(orderItemId)){
                return NotFound();
            }

            var orderItem= _mapper.Map<OrderItemsDto>(_orderItemRepository.GetOneOrderItem(orderItemId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(orderItem);
        }

        [HttpPost("orderDetail/{orderDetailId}")]
        [ProducesResponseType(200,Type=typeof(ICollection<OrderItem>))]
        [ProducesResponseType(400)]
        public IActionResult GetAlllOrderItemByOrderDetail(int orderDetailId)
        {
            if(! _orderDetailRepository.CheckIfOrderDetailExist(orderDetailId)){
                return NotFound();
            }

            var orderItems= _mapper.Map<List<OrderItem>>(_orderItemRepository.GetAllOrderItemsByOrder(orderDetailId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(orderItems);
        }

        [HttpPut("{orderItemId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize(Policy = "Admin/Manager")]
        public IActionResult UpdateOrderItem(int orderItemId,[FromQuery] int orderDetailId,[FromQuery] int actionPeformerId,[FromQuery] int productId,[FromBody] CreateOrderItemsDto orderItemUpdate) {
            if (orderItemUpdate == null)
                return BadRequest(ModelState);

            if (orderItemId != orderItemUpdate.Id) {
                return BadRequest(ModelState);
            }
            if(!_productRepository.CheckIfProductExist(productId)){
                return NotFound(new {errorMessage = "Product not found"});
            }
            if(!_orderItemRepository.CheckIfOrderItemExist(orderItemId)){
                return NotFound(new {errorMessage = "Order item not found"});
            }

            if(!_orderDetailRepository.CheckIfOrderDetailExist(orderDetailId)){
                return NotFound(new {errorMessage = "Order Detail not found"});
            }

            if (!ModelState.IsValid)
                return BadRequest();

            var orderItemMap = _mapper.Map<OrderItem>(orderItemUpdate);


            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_orderItemRepository.UpdateOrderItem(productId,orderDetailId,orderItemMap,actionPeformerId,referenceId)) {
                ModelState.AddModelError("", "Error Occured While Trying To update");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{orderItemId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager")]
        public IActionResult DeleteOrderItem(int orderItemId,[FromQuery] int actionPeformerId) {
            if(!_orderItemRepository.CheckIfOrderItemExist(orderItemId)){
                return NotFound();
            }

            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
                return NotFound();
            }

            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_orderItemRepository.DeleteOrderItem(orderItemId, actionPeformerId, referenceId)) {
                ModelState.AddModelError("", "Error Occured While Trying To Delete Order Item");
                return StatusCode(500, ModelState);

            }
            return Ok("Order Item Deleted Successfully");
         }


    }
}
