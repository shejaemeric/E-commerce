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
using System.Collections.ObjectModel;
using Swashbuckle.AspNetCore.Annotations;

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
    public readonly IWebHostEnvironment _environment;


        public ProductController(IWebHostEnvironment environment, IMapper mapper,IInvetoryRepository inventoryRepository,IUserRepository userRepository,IDiscountRepository discountRepository,IProductCategoryRepository productCategoryRepository,IProductRepository productRepository)
        {
            _mapper=mapper; _userRepository=userRepository;
            _discountRepository=discountRepository;
            _productCategoryRepository=productCategoryRepository;
            _productRepository = productRepository;
            _inventoryRepository = inventoryRepository;
            _environment = environment;
        }


        [HttpPost()]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Create a Product (Admin/Manager)")]
        public IActionResult CreateProduct([FromQuery] int productCategoryId,[FromQuery] int InventoryId,[FromBody] ProductDto productCreate,[FromQuery] int? discountId) {
            if (productCreate == null)
                return BadRequest(ModelState);

            if(!_inventoryRepository.CheckIfInvetoryExist(InventoryId)){
                return NotFound(new {errorMessage = "Inventory not found"});
            }

            if(!_productCategoryRepository.CheckIfProductCategoryExist(productCategoryId)){
                return NotFound(new {errorMessage = "Product Category not found"});
            }
            if (discountId != null){
                if (!_discountRepository.CheckIfDiscountExist(discountId)){
                    return NotFound(new {errorMessage = "Discount not found"});
                }
            }

            var product = _productRepository.GetAllProducts().Where(c => c.Name == productCreate.Name).FirstOrDefault();
            if (product != null) {
                ModelState.AddModelError("", "Product Already Exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(productCreate);

            if (!_productRepository.CreateProduct(InventoryId,productCategoryId,productMap,discountId)) {
                ModelState.AddModelError("", "Error Occured While Trying To save");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }


        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<ProductDto>))]
        [ProducesResponseType(400)]
        [SwaggerOperation(Summary = "Get All Products (Anyone)")]
        public IActionResult GetAllProducts()
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetAllProducts());
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(products);
        }

        [HttpPost("{productId}")]
        [ProducesResponseType(200,Type = typeof(ProductOneDto))]
        [ProducesResponseType(400)]
        [SwaggerOperation(Summary = "Get One Product (Anyone)")]
        public IActionResult GetOneProduct(int productId)
        {
            if(! _productRepository.CheckIfProductExist(productId)){
                return NotFound();
            }

            var product = _mapper.Map<ProductOneDto>(_productRepository.GetOneProduct(productId));

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            // string imageUrl = string.Empty;
            // string hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            // try {
            //     string dirPath = _environment.WebRootPath + "/Uploads/products/" + productId;
            //     string[] files = Directory.GetFiles(dirPath, product.Name + "-featured" + ".*");
            //     if (files.Length > 0) product.FeaturedImage = hostUrl + "/Uploads/products/"+productId+"/"+ Path.GetFileName(files[0]);
            //  } catch {
            //     Console.WriteLine("An Error Occured");
            //  }

            string imageUrl = string.Empty;
            string hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try {
                string dirPath = _environment.WebRootPath + "/Uploads/products/" + productId;
                string[] files = Directory.GetFiles(dirPath);
                if (files.Length > 0) product.Images = files;
             } catch {
                Console.WriteLine("An Error Occured");
             }

            return Ok(product);
        }

        [HttpGet("on-discount")]
        [ProducesResponseType(200,Type = typeof(ICollection<Product>))]
        [ProducesResponseType(400)]
        [SwaggerOperation(Summary = "Get Products On Discount (Anyone)")]
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
        [SwaggerOperation(Summary = "Get Products By Discount (Anyone)")]
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

        [HttpGet("category/{category_id}")]
        [ProducesResponseType(200,Type = typeof(ICollection<Product>))]
        [ProducesResponseType(400)]
        [SwaggerOperation(Summary = "Get Products By Discount (Anyone)")]
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
        [Authorize(Policy = "Admin/Manager/Owner")]
        [SwaggerOperation(Summary = "Update a Product (Admin/Manager/Owner)")]
        public IActionResult UpdateProduct(int productId,[FromQuery] int discountId,[FromQuery] int productCategoryId,[FromQuery] int actionPeformerId,[FromQuery] int InventoryId,[FromBody] ProductDto productUpdate) {
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

            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_productRepository.UpdateProduct(discountId,InventoryId,productCategoryId,productMap,actionPeformerId,referenceId)) {
                ModelState.AddModelError("", "Error Occured While Trying To Update");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        [HttpDelete("{productId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager/Owner")]
        [SwaggerOperation(Summary = "Delete a Product (Admin/Manager/Owner)")]
        public IActionResult DeleteProduct(int productId,[FromQuery] int actionPeformerId) {
            if(!_productRepository.CheckIfProductExist(productId)){
                return NotFound();
            }

            if(!_userRepository.CheckIfUserExist(actionPeformerId)){
                return NotFound();
            }

            Guid guid = Guid.NewGuid();
            string referenceId = guid.ToString();

            if (!_productRepository.DeleteProduct(productId, actionPeformerId, referenceId)) {
                ModelState.AddModelError("", "Error Occured While Trying To Delete Product");
                return StatusCode(500, ModelState);

            }
            return Ok("Product Deleted Successfully");
         }


        [HttpPost("{productId}/featuredImage/upload")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Upload Featured Image / saved On Server (Admin/Manager)")]
        public async Task<IActionResult> UploadFeaturedImage(int productId, IFormFile featuredImage)
        {
            try {

                var product = _productRepository.GetOneProduct(productId);

                if (product == null) {
                    var message = new { message = "product Doesn't Exist" };
                    return NotFound(message);
                }

                string productImagesFolder = _environment.WebRootPath + "/Uploads/products/" + productId;

                if (!System.IO.Directory.Exists(productImagesFolder)) {
                    System.IO.Directory.CreateDirectory(productImagesFolder);
                }


                var splitName = featuredImage.FileName.Split(".");
                var name = product.Name + "-featured." + splitName[splitName.Length-1];
                string imagePath = productImagesFolder + "/" + name;

                if (!System.IO.File.Exists(imagePath)) {
                    System.IO.File.Delete(imagePath);
                }
                using (FileStream stream = System.IO.File.Create(imagePath)) {
                    await featuredImage.CopyToAsync(stream);
                    string homePath = $"{this.Request.Scheme}://{this.Request.Host}";
                    var result = new { message = "Successfully Uploaded", file = homePath+"/Uploads/products/"+productId+"/"+name };
                    return Ok(result);
                }
            } catch  {
                return StatusCode(500, "An Error Occured While Uploading");
            }
        }

        [HttpPost("{productId}/images/upload")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Upload other images of a Product (Admin/Manager/Owner)")]
        public async Task<IActionResult> UploadOtherImages(int productId, IFormFileCollection images)
        {
            try {

                var product = _productRepository.GetOneProduct(productId);

                if (product == null) {
                    var message = new { message = "product Doesn't Exist" };
                    return NotFound(message);
                }

                string productImagesFolder = _environment.WebRootPath + "/Uploads/products/" + productId;

                if (!System.IO.Directory.Exists(productImagesFolder)) {
                    System.IO.Directory.CreateDirectory(productImagesFolder);
                }

                var status = new {
                    Uploaded = new Collection<object>(),
                    Failed = new List<string>()

                };


                for (int i = 0; i<images.Count;i++) {
                    var splitName = images[i].FileName.Split(".");
                    var name = product.Name + "-images-" + i + "." + splitName[splitName.Length-1];
                    string imagePath = productImagesFolder + "/" + name;

                    if (!System.IO.File.Exists(imagePath)) {
                        System.IO.File.Delete(imagePath);
                    }
                    try {
                        using (FileStream stream = System.IO.File.Create(imagePath))
                        {
                            await images[i].CopyToAsync(stream);
                            string homePath = $"{this.Request.Scheme}://{this.Request.Host}";
                            var addedImage = new
                            {
                                name = images[i].FileName,
                                link = homePath+"/Uploads/products/"+productId+"/"+name
                            };
                            status.Uploaded.Add(addedImage);
                        }

                    } catch {

                        status.Failed.Add(images[i].FileName);
                    }
                }
            return Ok(new { result = status });
            } catch  {
                return StatusCode(500, "An Error Occured While Uploading");
            }
        }


    }


}
