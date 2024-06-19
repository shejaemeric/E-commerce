using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;
using E_Commerce_Api.Data;
using E_Commerce_Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;




namespace E_Commerce_Api.Repository
{


    public class ReportRepository : IReportRepository
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _environment;

        public ReportRepository(DataContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _environment = webHostEnvironment;
        }

        public IEnumerable<AbandonedCartsReport> GetAbandonedCartsReport() {
            var carts = _context.getAllAbandonedCarts().ToList();
            var groupedByCartItem = carts.GroupBy(ss => new { ss.Id, ss.Created_At, ss.Modified_At, ss.Total,ss.UserName })
            .Select(g => new AbandonedCartsReport
            {
                Id = g.Key.Id,
                Total = g.Key.Total,
                Created_At = g.Key.Created_At,
                Modified_At = g.Key.Modified_At,
                UserName = g.Key.UserName,
                CartItems = g.Select(c => new AbandonedCartsReportSub {
                    Id = c.cartId,
                    ProductName = c.ProductName,
                    Price = c.Price,
                    Quantity = c.Quantity,
                    Sub_total = c.Sub_total,
                    ProductId = c.ProductId
                })
            });

            return groupedByCartItem;

        }

        public IEnumerable<GroupedOrderDetailsByProduct> GetSalesByProductReport() {
            var ordersDetails = _context.getAllOrdersDetails().ToList();
            var groupedByProduct = ordersDetails.GroupBy(o => new { o.ProductName,o.Price,o.Discount_percent })
            .Select(g => new GroupedOrderDetailsByProduct
            {
                ProductName = g.Key.ProductName,
                Price = g.Key.Price,
                Discount_percent = g.Key.Discount_percent ?? 0,
                Orders = g.Select(o => new GroupedOrderDetailsByProductSub{
                    Id = o.Id,
                    Status = o.Status,
                    Customer = o.Customer,
                    Created_At = o.Created_At,
                    Quantity = o.Quantity
                })

            });

            return groupedByProduct;

        }
        public List<OutOfStockProductsReport> GetOutOfStocksProductsReport() {
            var products = _context.getAllOutOfStockProducts().ToList();
            return products;
        }




        public  List<GroupedOrderDetails> GroupOrderReports(List<OrderDetailReport> orderDetails) {
            var groupedData = orderDetails
                .GroupBy(o => new { o.Id, o.Status, o.Customer,o.Created_At })
                .Select(g => new GroupedOrderDetails
                {

                    Id = g.Key.Id,
                    Status = g.Key.Status,
                    Customer = g.Key.Customer,
                    Created_At = g.Key.Created_At,
                    Products = g.Select(p => new GroupedOrderProduct
                    {
                        ProductName = p.ProductName,
                        Quantity = p.Quantity,
                        Price = p.Price,
                        Discount_percent = p.Discount_percent ?? 0,
                        Total = p.Total
                    }).ToList()
                }).Where(group => group.Products.Any()).ToList();

                return groupedData;
        }


         public IEnumerable<OrderDetailByDiscountReport> GetAllOrderDetailsHavingDiscount() {
            var ordersDetails = _context.getAllOrderDetailsHavingDiscount().ToList();


             var groupedData = ordersDetails
                .GroupBy(o => new { o.Id, o.Discount_Created_At, o.Modified_At,o.Name,o.Desc,o.Active,o.Discount_percent })
                .Select(g => new OrderDetailByDiscountReport
                {

                    Id = g.Key.Id,
                    Discount_Created_At = g.Key.Discount_Created_At,
                    Modified_At = g.Key.Modified_At,
                    Name = g.Key.Name,
                    Desc = g.Key.Desc,
                    Active = g.Key.Active,
                    Discount_percent = g.Key.Discount_percent ?? 0,
                    Orders = g.Select(p => new OrderDetailByDiscountReportSub
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        Quantity = p.Quantity,
                        Created_At = p.Created_At,
                        Status = p. Status,
                        Customer = p.Customer,
                        Price = p.Price,
                        Total = p.Total
                    }).ToList()
                }).ToList();

                return groupedData;
         }


        public ICollection<GroupedOrderDetails> GetOrderDetailsReport()
        {
            var orderDetails = _context.getAllOrdersDetails().ToList();
            return GroupOrderReports(orderDetails);
        }


        public ICollection<GroupedOrderDetails> GetMonthlyOrderDetailsReport()
        {
            var orderDetails = _context.getMonthlyOrdersDetails().ToList();
            return GroupOrderReports(orderDetails);
        }

        public ICollection<GroupedOrderDetails> GetYearlyOrderDetailsReport()
        {
            var orderDetails = _context.getYearlyOrdersDetails().ToList();
            return GroupOrderReports(orderDetails);
        }

        public ICollection<GroupedOrderDetails> GetTodaysOrderDetailsReport()
        {
            var orderDetails = _context.getTodayOrdersDetails().ToList();
            return GroupOrderReports(orderDetails);
        }

    }
}
