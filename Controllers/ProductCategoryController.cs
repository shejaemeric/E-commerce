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

        //     ICollection<ProductCategory> GetAllProductCategories();

        // ProductCategory GetOneProductCategory(int productId);

        // bool CheckIfProductCategoryExist(int productCategoryId);

    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IMapper _mapper;
        public ProductCategoryController(IMapper mapper,IProductCategoryRepository productCategoryRepository)
        {
            _mapper = mapper;
            _productCategoryRepository = productCategoryRepository;
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

            var productCategoryMap = _mapper.Map<ProductCategory>(productCategoryUpdate);

            if (!_productCategoryRepository.UpdateProductCategory(productCategoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating ProductCategory");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
