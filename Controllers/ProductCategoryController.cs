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
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Create a Product Category (Admin/Manager)")]
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
        [SwaggerOperation(Summary = "Get All Product Category (Anyone)")]
        public IActionResult GetAllProductCategories()
        {
            var productCategories = _mapper.Map<List<ProductCategoryDto>>(_productCategoryRepository.GetAllProductCategories());
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(productCategories);
        }



        [HttpPost("{productCategoryId}")]
        [ProducesResponseType(200,Type = typeof(ProductCategory))]
        [ProducesResponseType(400)]
        [SwaggerOperation(Summary = "Get One Product Category (Anyone)")]
        public IActionResult GetOneProductCategory(int productCategoryId)
        {
            if(! _productCategoryRepository.CheckIfProductCategoryExist(productCategoryId)){
                return NotFound();
            }

            var productCategory = _mapper.Map<ProductCategoryDto>(_productCategoryRepository.GetOneProductCategory(productCategoryId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(productCategory);
        }

        [HttpPut("{productCategoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Update one Product Category (Admin/Manager)")]
        public IActionResult UpdateProductCategory(int productCategoryId,[FromBody] ProductCategoryDto productCategoryUpdate)
        {
            if (productCategoryUpdate == null)
                return BadRequest(ModelState);

            if (productCategoryId != productCategoryUpdate.Id)
                return BadRequest(ModelState);

            if (!_productCategoryRepository.CheckIfProductCategoryExist(productCategoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();


            var actionPeformerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
               return StatusCode(405, "User not Allowed/Authenticated");
            }

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
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Delete one Product Category (Admin/Manager)")]
        public IActionResult DeleteProductCategory(int productCategoryId) {
            if(!_productCategoryRepository.CheckIfProductCategoryExist(productCategoryId)){
                return NotFound();
            }

            var actionPeformerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
               return StatusCode(405, "User not Allowed/Authenticated");
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
