using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;

namespace E_Commerce_Api.Interfaces
{
    public interface IReportRepository
    {
        ICollection<GroupedOrderDetails> GetOrderDetailsReport();

        ICollection<GroupedOrderDetails> GetMonthlyOrderDetailsReport();
        ICollection<GroupedOrderDetails> GetYearlyOrderDetailsReport();
        ICollection<GroupedOrderDetails> GetTodaysOrderDetailsReport();
        public IEnumerable<GroupedOrderDetailsByProduct> GetSalesByProductReport();
        public IEnumerable<OrderDetailByDiscountReport> GetAllOrderDetailsHavingDiscount();
        public IEnumerable<AbandonedCartsReport> GetAbandonedCartsReport();
        public List<OutOfStockProductsReport> GetOutOfStocksProductsReport();
    }
}
