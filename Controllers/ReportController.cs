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
using E_Commerce_Api.reportDocuments;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;

namespace E_Commerce_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
    public readonly IMapper _mapper;
    public readonly IUserRepository _userRepository;
    public readonly IDiscountRepository _discountRepository;

    public readonly IProductCategoryRepository _productCategoryRepository;
    public readonly IInvetoryRepository _inventoryRepository;
    public readonly IProductRepository _productRepository;
    public readonly IReportRepository _reportRepository;
    public readonly IOrderDetailRepository _orderDetailRepository;
    public readonly IWebHostEnvironment _environment;


        public ReportController(IWebHostEnvironment environment,IOrderDetailRepository orderDetailRepository,IReportRepository reportRepository, IMapper mapper,IInvetoryRepository inventoryRepository,IUserRepository userRepository,IDiscountRepository discountRepository,IProductCategoryRepository productCategoryRepository,IProductRepository productRepository)
        {
            _mapper=mapper; _userRepository=userRepository;
            _discountRepository=discountRepository;
            _productCategoryRepository=productCategoryRepository;
            _productRepository = productRepository;
            _inventoryRepository = inventoryRepository;
            _reportRepository = reportRepository;
            _orderDetailRepository = orderDetailRepository;
            _environment = environment;
        }


        [HttpGet("Orders")]
        [ProducesResponseType(200,Type=typeof(object))]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Get All Order Report (Admin/Manager)")]
        public IActionResult GetAllOrderReport()
        {
            var Orders = _reportRepository.GetOrderDetailsReport();
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(Orders);
        }


        [HttpGet("Orders/monthly")]
        [ProducesResponseType(200,Type=typeof(object))]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Get All monthly Orders Report (Admin/Manager)")]
        public IActionResult GetAllTodaysReport()
        {
            var Orders = _reportRepository.GetTodaysOrderDetailsReport();
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(Orders);
        }

        [HttpGet("Orders/today")]
        [ProducesResponseType(200,Type=typeof(object))]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Get All Monthly Orders Report (Admin/Manager)")]
        public IActionResult GetMonthlyTodaysReport()
        {
            var Orders = _reportRepository.GetMonthlyOrderDetailsReport();
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(Orders);
        }

        [HttpGet("Orders/yearly")]
        [ProducesResponseType(200,Type=typeof(object))]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Get All Today's Orders Report (Admin/Manager)")]
        public IActionResult GetAllYearlyReport()
        {
            var Orders = _reportRepository.GetYearlyOrderDetailsReport();
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(Orders);
        }




        [HttpGet("Orders/Products")]
        [ProducesResponseType(200,Type=typeof(object))]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Get All Orders grouped by products Report (Admin/Manager)")]
        public IActionResult GetAllOrderDetailsByProduct()
        {
            var Orders = _reportRepository.GetSalesByProductReport();
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(Orders);
        }


        [HttpGet("Orders/Discounts")]
        [ProducesResponseType(200,Type=typeof(object))]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Get All Orders grouped by Discounts Report (Admin/Manager)")]
        public IActionResult GetAllOrderDetailsByDiscount()
        {
            var Orders = _reportRepository.GetAllOrderDetailsHavingDiscount();
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(Orders);
        }


        [HttpGet("Carts/Abandoned")]
        [ProducesResponseType(200,Type=typeof(object))]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Get All abandoned Carts Report (Admin/Manager)")]
        public IActionResult GetAllAbandonedCarts()
        {
            var Orders = _reportRepository.GetAbandonedCartsReport();
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(Orders);
        }


        [HttpGet("Products/OutOfStock")]
        [ProducesResponseType(200,Type=typeof(object))]
        [ProducesResponseType(400)]
        [Authorize(Policy = "Admin/Manager")]
        [SwaggerOperation(Summary = "Get All Products Out Of Stock Report (Admin/Manager)")]
        public IActionResult GetAllOutOfStockProduct()
        {
            var Orders = _reportRepository.GetOutOfStocksProductsReport();
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            return Ok(Orders);
        }

    }





}
