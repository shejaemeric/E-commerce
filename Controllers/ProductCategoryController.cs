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

    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public ProductCategoryController(IMapper mapper,IProductCategoryRepository productCategoryRepository,IUserRepository userRepository)
        {
            _mapper = mapper;
            _productCategoryRepository = productCategoryRepository;
            _userRepository = userRepository;
        }

        [HttpPost()]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProductCategory([FromBody] ProductCategoryDto productCategoryCreate) {
            if (productCategoryCreate == null)
                return BadRequest(ModelState);


            var productCategory = _productCategoryRepository.GetAllProductCategories().Where(c => c.Name.Trim().ToLower() == productCategoryCreate.Name.Trim().ToLower()).FirstOrDefault();

            if (productCategory != null) {
                ModelState.AddModelError("", "Product Category Already Exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productCategoryMap = _mapper.Map<ProductCategory>(productCategoryCreate);

            if (!_productCategoryRepository.CreateProductCategory(productCategoryMap)) {
                ModelState.AddModelError("", "Error Occured While Trying To Save");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }

        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<ProductCategoryDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllProductCategories()
        {
            var productCategories = _mapper.Map<List<ProductCategoryDto>>(_productCategoryRepository.GetAllProductCategories());
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(productCategories);
        }



        [HttpPost("{productId}")]
        [ProducesResponseType(200,Type = typeof(ProductCategory))]
        [ProducesResponseType(400)]
        public IActionResult GetOneProductCategory(int productId)
        {
            if(! _productCategoryRepository.CheckIfProductCategoryExist(productId)){
                return NotFound();
            }

            var productCategory = _mapper.Map<ProductCategoryDto>(_productCategoryRepository.GetOneProductCategory(productId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(productCategory);
        }

        [HttpPut("{productCategoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProductCategory(int productCategoryId,[FromBody] ProductCategoryDto productCategoryUpdate,[FromQuery] int actionPeformerId)
        {
            if (productCategoryUpdate == null)
                return BadRequest(ModelState);

            if (productCategoryId != productCategoryUpdate.Id)
                return BadRequest(ModelState);

            if (!_productCategoryRepository.CheckIfProductCategoryExist(productCategoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var productCategoryMap = _mapper.Map<ProductCategory>(productCategoryUpdate);

            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_productCategoryRepository.UpdateProductCategory(productCategoryMap,actionPeformerId,referenceId))
            {
                ModelState.AddModelError("", "Something went wrong updating ProductCategory");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{productCategoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult DeleteProductCategory(int productCategoryId,[FromQuery] int actionPeformerId) {
            if(!_productCategoryRepository.CheckIfProductCategoryExist(productCategoryId)){
                return NotFound();
            }

            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
                return NotFound();
            }


            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_productCategoryRepository.DeleteProductCategory(productCategoryId, actionPeformerId, referenceId)) {
                ModelState.AddModelError("", "Error Occured While Trying To Delete Product Category");
                return StatusCode(500, ModelState);

            }
            return Ok("Product Category Deleted Successfully");
         }
    }
}
