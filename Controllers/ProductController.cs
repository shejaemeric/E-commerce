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
    public class ProductController : ControllerBase
    {
    public readonly IMapper _mapper;
    public readonly IUserRepository _userRepository;
    public readonly IDiscountRepository _discountRepository;

    public readonly IProductCategoryRepository _productCategoryRepository;
     public readonly IInvetoryRepository _inventoryRepository;
      public readonly IProductRepository _productRepository;

        public ProductController( IMapper mapper,IInvetoryRepository inventoryRepository,IUserRepository userRepository,IDiscountRepository discountRepository,IProductCategoryRepository productCategoryRepository,IProductRepository productRepository)
        {
            _mapper=mapper; _userRepository=userRepository;
            _discountRepository=discountRepository;
            _productCategoryRepository=productCategoryRepository;
            _productRepository = productRepository;
            _inventoryRepository = inventoryRepository;
        }


        [HttpPost()]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProduct([FromQuery] int discountId,[FromQuery] int productCategoryId,[FromQuery] int InventoryId,[FromBody] ProductDto productCreate) {
            if (productCreate == null)
                return BadRequest(ModelState);

            if(!_inventoryRepository.CheckIfInvetoryExist(InventoryId)){
                return NotFound(new {errorMessage = "Inventory not found"});
            }

            if(!_productCategoryRepository.CheckIfProductCategoryExist(productCategoryId)){
                return NotFound(new {errorMessage = "Product Category not found"});
            }
            if(!_discountRepository.CheckIfDiscountExist(discountId)){
                return NotFound(new {errorMessage = "Discount not found"});
            }

            var product = _productRepository.GetAllProducts().Where(c => c.Name == productCreate.Name).FirstOrDefault();
            if (product != null) {
                ModelState.AddModelError("", "Product Already Exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(productCreate);

            if (!_productRepository.CreateProduct(discountId,InventoryId,productCategoryId,productMap)) {
                ModelState.AddModelError("", "Error Occured While Trying To save");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }


        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<Product>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsers()
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetAllProducts());
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(products);
        }



        [HttpPost("{productId}")]
        [ProducesResponseType(200,Type = typeof(Product))]
        [ProducesResponseType(400)]
        public IActionResult GetOneUser(int productId)
        {
            if(! _productRepository.CheckIfProductExist(productId)){
                return NotFound();
            }

            var product = _mapper.Map<ProductDto>(_productRepository.GetOneProduct(productId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(product);
        }

        [HttpGet("on-discount")]
        [ProducesResponseType(200,Type = typeof(ICollection<Product>))]
        [ProducesResponseType(400)]
        public IActionResult GetProductsOnDiscount()
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetProductsOnDiscount());

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(products);
        }

        [HttpPost("discount/{discountId}")]
        [ProducesResponseType(200,Type = typeof(ICollection<Product>))]
        [ProducesResponseType(400)]
        public IActionResult GetProductsByDiscount(int discountId)
        {
            if(! _discountRepository.CheckIfDiscountExist(discountId)){
                return NotFound();
            }

            var product = _mapper.Map<ProductDto>(_productRepository.GetOneProduct(discountId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(product);
        }

        [HttpGet("/category/{category_id}")]
        [ProducesResponseType(200,Type = typeof(ICollection<Product>))]
        [ProducesResponseType(400)]
        public IActionResult GetProductsByCategory(int category_id)
        {
            if(! _productCategoryRepository.CheckIfProductCategoryExist(category_id)){
                return NotFound();
            }
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetProductsByCategory(category_id));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(products);
        }

        [HttpPut("{productId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProduct(int productId,[FromQuery] int discountId,[FromQuery] int productCategoryId,[FromQuery] int InventoryId,[FromBody] ProductDto productUpdate) {
            if (productUpdate == null)
                return BadRequest(ModelState);
            if (productId != productUpdate.Id)
                return BadRequest(ModelState);

            if(!_inventoryRepository.CheckIfInvetoryExist(InventoryId)){
                return NotFound(new {errorMessage = "Inventory not found"});
            }

            if(!_productCategoryRepository.CheckIfProductCategoryExist(productCategoryId)){
                return NotFound(new {errorMessage = "Product Category not found"});
            }
            if(!_discountRepository.CheckIfDiscountExist(discountId)){
                return NotFound(new {errorMessage = "Discount not found"});

            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(productUpdate);

            if (!_productRepository.UpdateProduct(discountId,InventoryId,productCategoryId,productMap)) {
                ModelState.AddModelError("", "Error Occured While Trying To Update");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }


}
