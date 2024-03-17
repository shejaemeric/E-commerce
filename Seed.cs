using System;
using System.Linq;
using E_Commerce_Api.Models;
using E_Commerce_Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce_Api.Seed
{
    public class Seed
    {
        private readonly DataContext context;
        public Seed(DataContext context)
        {
            this.context = context;
        }
        public void SeedDataContext()
        {
                // Check if the database has been seeded
            if (context.Users.Any())
            {
                return;   // Database has been seeded
            }

            // Seed Users
            var user = new User
            {
                Username = "MRS. Jane Doe",
                Password = "password",
                Name = "John Doe",
                Telephone = "+250784339373",
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now
            };

            var user1 = new User
            {
                Username = "MR. John Doe",
                Password = "password1",
                Name = "John Doe",
                Telephone = "+250786219583",
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now
            };
            context.Users.Add(user);
            context.Users.Add(user1);
            context.SaveChanges();

            // Seed UserAddress
            var userAddress = new UserAddress
            {
                Address = "123 Main St",
                Country = "USA",
                City = "New York",
                UserId = user.Id
            };

            var userAddress1 = new UserAddress
            {
                Address = "SH 66 ST",
                Country = "Rwanda",
                City = "Kigali",
                UserId = user1.Id
            };
            context.UserAddresses.Add(userAddress);
            context.UserAddresses.Add(userAddress1);
            context.SaveChanges();

            // Seed UserPayment
            var userPayment = new UserPayment
            {
                Payment_Type = "Credit Card",
                Card_Holder_Name = "John Doe",
                Card_Number = 4444567890123456,
                CVV = 123,
                Expiration_Month = 442,
                Expiration_Year = 2026,
                Created_At = DateTime.Now,
                UserId = user.Id
            };
            var userPayment1 = new UserPayment
            {
                Payment_Type = "Master Card",
                Card_Holder_Name = "Jane Doe",
                Card_Number = 4445678901243456,
                CVV = 623,
                Expiration_Month = 1,
                Expiration_Year = 2027,
                Created_At = DateTime.Now,
                UserId = user1.Id
            };
            context.UserPayments.Add(userPayment);
            context.UserPayments.Add(userPayment1);
            context.SaveChanges();

            // Seed ShoppingSession
            var shoppingSession = new ShoppingSession
            {
                Total = 130,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                User = user
            };

            var shoppingSession1 = new ShoppingSession
            {
                Total = 90,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                User = user1
            };
            context.ShoppingSessions.Add(shoppingSession);
            context.ShoppingSessions.Add(shoppingSession1);
            context.SaveChanges();

            // Seed ProductCategory
            var productCategory = new ProductCategory
            {
                Name = "Electronics",
                Desc = "Electronics category description",
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                Deleted_At = DateTime.Now
            };
            context.ProductCategories.Add(productCategory);

            var productCategory1 = new ProductCategory
            {
                Name = "Cleaning Supplies",
                Desc = "Cleaning Supplies category description",
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                Deleted_At = DateTime.Now
            };
            context.ProductCategories.Add(productCategory);
             context.ProductCategories.Add(productCategory1);
            context.SaveChanges();

            // Seed Product
            var discount = new Discount
            {
                Name = "New Year Sale",
                Desc = "New Year Sale description",
                Active = true,
                Discount_percent = 10,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                Deleted_At = DateTime.Now
            };

            var discount1 = new Discount
            {
                Name = "Valentine Sale",
                Desc = "ValentineSale description",
                Active = false,
                Discount_percent = 5,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                Deleted_At = DateTime.Now
            };
            context.Discounts.Add(discount);
             context.Discounts.Add(discount1);
            context.SaveChanges(); // Save the discount before referencing it in Product

            // Seed Inventory
            var inventory = new Inventory
            {
                Quantity = 200,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                Deleted_At = DateTime.Now
            };
            var inventory1 = new Inventory
            {
                Quantity = 51,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                Deleted_At = DateTime.Now
            };
            context.Inventories.Add(inventory);
            context.Inventories.Add(inventory1);
            context.SaveChanges();

            // Seed Product
            var product = new Product
            {
                Name = "Laptop",
                Desc = "Laptop description",
                SKU = "ABC123",
                Price = 99,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                Deleted_At = DateTime.Now,
                ProductCategory = productCategory,
                Discount = discount, // Assign the discount to the product
                Inventory = inventory,
            };

            var product1 = new Product
            {
                Name = "NIVEA Cleaning Soap",
                Desc = "Laptop description",
                SKU = "ZZK123",
                Price = 39,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                Deleted_At = DateTime.Now,
                ProductCategory = productCategory1,
                Discount = discount1, // Assign the discount to the product
                Inventory = inventory1,
            };
            context.Products.Add(product);
             context.Products.Add(product1);
            context.SaveChanges();



            // Seed CartItem
            var cartItem = new CartItem
            {
                Quantity = 1,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                Product = product,
                ShoppingSession = shoppingSession
            };

            var cartItem1 = new CartItem
            {
                Quantity = 4,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                Product = product1,
                ShoppingSession = shoppingSession1
            };
            context.CartItems.Add(cartItem);
            context.CartItems.Add(cartItem1);
            context.SaveChanges();

            // Seed PaymentDetails
            var paymentDetails = new PaymentDetails
            {
                Amount = 100,
                Status = "Paid",
                Payment_Type = "Credit Card",
                Card_Holder_Name = "John Doe",
                Card_Number = 1234567890123456,
                CVV = 123,
                Expiration_Month = 12,
                Expiration_Year = 2025,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now
            };

            var paymentDetails1 = new PaymentDetails
            {
                Amount = 100,
                Status = "Paid",
                Payment_Type = "Credit Card",
                Card_Holder_Name = "Jane Doe",
                Card_Number = 444567890123456,
                CVV = 223,
                Expiration_Month = 1,
                Expiration_Year = 2027,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now
            };
            context.PaymentDetails.Add(paymentDetails);
            context.PaymentDetails.Add(paymentDetails1);
            context.SaveChanges();

            // Seed OrderDetails
            var orderDetails = new OrderDetails
            {
                Total = 1,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                User = user,
                PaymentDetails = paymentDetails
            };
            var orderDetails1 = new OrderDetails
            {
                Total = 4,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                User = user1,
                PaymentDetails = paymentDetails1
            };
            context.OrderDetails.Add(orderDetails);
            context.OrderDetails.Add(orderDetails1);
            context.SaveChanges();

            // Seed OrderItems
            var orderItem = new OrderItems
            {
                Quantity = 1,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                Product = product,
                OrderDetails = orderDetails
            };
            var orderItem1 = new OrderItems
            {
                Quantity = 4,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                Product = product1,
                OrderDetails = orderDetails1
            };
            context.OrderItems.Add(orderItem);
            context.OrderItems.Add(orderItem1);
            context.SaveChanges();
            }
        }
    }

