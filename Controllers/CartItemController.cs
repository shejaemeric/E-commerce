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
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IShoppingSessionRepository _shoppingSessionRepository;


        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;


        private readonly IMapper _mapper;
        public CartItemController(IShoppingSessionRepository shoppingSessionRepository,ICartItemRepository cartItemRepository,IProductRepository productRepository,IUserRepository userRepository,IMapper mapper)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
            _shoppingSessionRepository = shoppingSessionRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        [HttpPost()]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [SwaggerOperation(Summary = "Create a Cart Item (Anyone)")]

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


        [HttpPost("{cartItemId}")]
        [ProducesResponseType(200,Type = typeof(CartItem))]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager/Owner")]
        [SwaggerOperation(Summary = "Get One Cart Item (Admin/Manager/Owner)")]
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
        [Authorize(Policy = "Admin/Manager/Owner")]
        [SwaggerOperation(Summary = "Update One Cart Item (Admin/Manager/Owner)")]

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

            var actionPeformerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
               return StatusCode(405, "User not Allowed/Authenticated");
            }

            var cartItemMap = _mapper.Map<CartItem>(cartItemUpdate);

            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_cartItemRepository.UpdateCartItem(productId,shoppingSessionId,cartItemMap,actionPeformerId,referenceId)) {
                ModelState.AddModelError("", "Error Occured While Trying To Update");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{cartItemId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager/Owner")]
        [SwaggerOperation(Summary = "Delete One Cart Item (Admin/Manager/Owner)")]
        public IActionResult DeleteCartItem(int cartItemId,[FromQuery] int actionPeformerId) {
            if(!_cartItemRepository.CheckIfCartItemExist(cartItemId)){
                return NotFound();
            }

            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
                return NotFound();
            }

            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_cartItemRepository.DeleteCartItem(cartItemId, actionPeformerId, referenceId)) {
                ModelState.AddModelError("", "Error Occured While Trying To Delete Cart Item");
                return StatusCode(500, ModelState);

            }
            return Ok("Cart Item Deleted Successfully");
         }

    }
}
