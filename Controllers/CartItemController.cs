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
    public class CartItemController : ControllerBase
    {
        // ICollection<CartItem> GetAllCartItemsBySession(int sessionId);

        // CartItem GetOneCartItem(int cartItemId);
        // bool CheckIfCartItemExist(int cartItemId);

        private readonly ICartItemRepository _cartItemRepository;
        private readonly IShoppingSessionRepository _shoppingSessionRepository;


        private readonly IProductRepository _productRepository;


        private readonly IMapper _mapper;
        public CartItemController(IShoppingSessionRepository shoppingSessionRepository,ICartItemRepository cartItemRepository,IProductRepository productRepository,IMapper mapper)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
            _shoppingSessionRepository = shoppingSessionRepository;
            _productRepository = productRepository;
        }

        [HttpPost()]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCartItem([FromQuery] int productId,[FromQuery] int shoppingSessionId,[FromBody] CartItemDto cartItemCreate) {
            if (cartItemCreate == null)
                return BadRequest(ModelState);

            if(!_productRepository.CheckIfProductExist(productId)){
                return NotFound(new {errorMessage = "Product not found"});
            }


            if(!_shoppingSessionRepository.CheckIfShoppingSessionExist(shoppingSessionId)){
                return NotFound(new {errorMessage = "Shopping Session not found"});
            }

            var cartItemMap = _mapper.Map<CartItem>(cartItemCreate);

            if (!_cartItemRepository.CreateCartItem(productId,shoppingSessionId,cartItemMap)) {
                ModelState.AddModelError("", "Error Occured While Trying To save");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }

        [HttpPost("session/{sessionId}")]
        [ProducesResponseType(200,Type = typeof(ICollection<CartItem>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllCartItemsBySession(int sessionId)
        {
            if(! _shoppingSessionRepository.CheckIfShoppingSessionExist(sessionId)){
                return NotFound();
            }
            var cartItems= _mapper.Map<List<CartItemDto>>(_cartItemRepository.GetAllCartItemsBySession(sessionId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(cartItems);
        }

        [HttpPost("{cartItemId}")]
        [ProducesResponseType(200,Type = typeof(CartItem))]
        [ProducesResponseType(400)]
        public IActionResult GetOneCartItem(int cartItemId)
        {
            if(! _cartItemRepository.CheckIfCartItemExist(cartItemId)){
                return NotFound();
            }
            var orderDetails= _mapper.Map<CartItemDto>(_cartItemRepository.GetOneCartItem(cartItemId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(orderDetails);
        }

        [HttpPut("{cartItemId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCartItem(int cartItemId,[FromQuery] int productId,[FromQuery] int shoppingSessionId,[FromBody] CartItemDto cartItemUpdate) {
            if (cartItemUpdate == null)
                return BadRequest(ModelState);

            if (cartItemId != cartItemUpdate.Id)
                return BadRequest(ModelState);


            if(!_cartItemRepository.CheckIfCartItemExist(cartItemId)){
                return NotFound(new {errorMessage = "Cart item not found"});
            }

            if(!_productRepository.CheckIfProductExist(productId)){
                return NotFound(new {errorMessage = "Product not found"});
            }

            if(!_shoppingSessionRepository.CheckIfShoppingSessionExist(shoppingSessionId)){
                return NotFound(new {errorMessage = "Shopping Session not found"});
            }

            var cartItemMap = _mapper.Map<CartItem>(cartItemUpdate);

            if (!_cartItemRepository.UpdateCartItem(productId,shoppingSessionId,cartItemMap)) {
                ModelState.AddModelError("", "Error Occured While Trying To Update");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
