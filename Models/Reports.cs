
using System.ComponentModel.DataAnnotations;


namespace E_Commerce_Api.Models
{



    public class OutOfStockProductsReport {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Desc { get; set; }

        public int Price { get; set; }

        public required string SKU { get; set; }

        public int ProductCategoryId { get; set; }
        public DateTime Created_At { get; set; }

    }



    public class AbandonedCartsReport
    {
        public int Id { get; set; }

        public decimal Total { get; set; }
        public required string UserName { get; set; }
        public DateTime Created_At { get; set; }

        public DateTime Modified_At { get; set; }

        public IEnumerable<AbandonedCartsReportSub> CartItems { get; set; }
    }

    public class AbandonedCartsReportSub
    {
        public int Id { get; set; }


        public required string ProductName { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public decimal Sub_total { get; set; }

        public int ProductId { get; set; }
    }

    public class AbandonedCarts {
        public int Id { get; set; }

        public decimal Total { get; set; }
        public DateTime Created_At { get; set; }

        public DateTime Modified_At { get; set; }
        public int cartId { get; set; }
        public required string UserName { get; set; }

        public required string ProductName { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public decimal Sub_total { get; set; }

        public int ProductId { get; set; }
    }





    public class GroupedOrderDetailsByProduct
    {
        public int Price { get; set; }
        public string ProductName { get; set; }

        public decimal? Discount_percent { get; set; }
        public IEnumerable<GroupedOrderDetailsByProductSub> Orders { get; set; }

    }

    public class GroupedOrderDetailsByProductSub {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Customer { get; set; }
        public int Quantity { get; set; }
        public DateTime Created_At { get; set; }
    }


    public class GroupedOrderDetails
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Customer { get; set; }
        public DateTime Created_At { get; set; }
        public List<GroupedOrderProduct> Products { get; set; }
    }

    public class GroupedOrderProduct
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        public decimal? Discount_percent { get; set; }

        public int Total { get; set; }
    }
    public class OrderDetailReport
    {
        public int Id { get; set; }
        public DateTime Created_At { get; set; }
        public required string Status { get; set; }
        public required string Customer { get; set; }
        public required string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public decimal? Discount_percent { get; set; }
        public int Total { get; set; }
    }

public class OrderDetailByDiscount
    {
        public int OrderId { get; set; }

        public DateTime Created_At { get; set; }
        public required string Status { get; set; }
        public required string Customer { get; set; }
        public required string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }
        public int Id { get; set; }

        public DateTime Discount_Created_At { get; set; }
        public DateTime Modified_At { get; set; }
        public required string Name { get; set; }
        public required string Desc { get; set; }
        public Boolean Active { get; set; }

         public decimal? Discount_percent { get; set; }
    }

    public class OrderDetailByDiscountReport {
        public int Id { get; set; }
        public DateTime Discount_Created_At { get; set; }
        public DateTime Modified_At { get; set; }
        public required string Name { get; set; }
        public required string Desc { get; set; }
        public Boolean Active { get; set; }
        public decimal? Discount_percent { get; set; }

        public IEnumerable<OrderDetailByDiscountReportSub> Orders { get; set; }
    }

    public class OrderDetailByDiscountReportSub {
        public int Id { get; set; }
        public DateTime Created_At { get; set; }
        public required string Status { get; set; }
        public required string Customer { get; set; }
        public required string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }
    }

}
