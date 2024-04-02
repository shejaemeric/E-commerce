using System;
using System.Linq;
using E_Commerce_Api.Models;
using E_Commerce_Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using System.Net.NetworkInformation;

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

            //Seed roles

            var roles = new[]
            {
                new Role { Name = "admin", Description = "Administrator" },
                new Role { Name = "manager", Description = "Manager" },
                new Role { Name = "user", Description = "User" }
            };

            context.Roles.AddRange(roles);
            context.SaveChanges();

            //add permissions

            var permissions = new[]
            {
              // Customer/Normal User Permissions
                new Permission { Name = "view_products", Description = "Allows browsing and viewing available products." },
                new Permission { Name = "add_to_cart", Description = "Enables adding products to the shopping cart." },
                new Permission { Name = "place_order", Description = "Grants the ability to place orders for products." },
                new Permission { Name = "manage_account", Description = "Allows users to view and modify their account details." },

                // Manager Permissions
                new Permission { Name = "add_product", Description = "Permission to add new products to the inventory." },
                new Permission { Name = "edit_product", Description = "Permission to edit existing product details." },
                new Permission { Name = "delete_product", Description = "Permission to delete products from the inventory." },
                new Permission { Name = "manage_orders", Description = "Grants access to view, modify, and manage orders." },
                new Permission { Name = "view_payments", Description = "Allows viewing payment details." },
                new Permission { Name = "update_order_status", Description = "Permission to update the status of orders." },

                // Admin Permissions
                new Permission { Name = "manage_users", Description = "Allows adding, editing, and deleting user accounts." },
                new Permission { Name = "manage_products", Description = "Enables adding, editing, and deleting products from the inventory." },
                new Permission { Name = "manage_orders_admin", Description = "Grants the ability to view, modify, and manage orders." },
                new Permission { Name = "manage_payments", Description = "Allows managing payment details, such as viewing, editing, or deleting payments." },
                new Permission { Name = "manage_settings", Description = "Allows modifying system settings and configurations." },
                new Permission { Name = "view_reports", Description = "Permission to access and generate reports on user activity, sales, etc." },
                new Permission { Name = "update_inventory", Description = "Permission to update inventory levels for products." },
                new Permission { Name = "manage_discounts", Description = "Permission to manage discounts, including adding, editing, or deleting discounts." },
                new Permission { Name = "manage_categories", Description = "Allows managing product categories, such as adding, editing, or deleting categories." }
        };

            context.Permissions.AddRange(permissions);
            context.SaveChanges();

            var RolePermissions = new[] {
            new RolePermission{
                Permission = permissions[0],
                Role = roles[0],
             },
            new RolePermission {
                Permission = permissions[1],
                Role = roles[0],
            },
            new RolePermission {
                Permission = permissions[2],
                Role = roles[0],
            },
            new RolePermission {
                Permission = permissions[3],
                Role = roles[0],
            },


            new RolePermission{
                Permission = permissions[4],
                Role = roles[1],
             },
            new RolePermission {
                Permission = permissions[5],
                Role = roles[1],
            },
            new RolePermission {
                Permission = permissions[6],
                Role = roles[1],
            },
            new RolePermission {
                Permission = permissions[7],
                Role = roles[1],
            },new RolePermission{
                Permission = permissions[8],
                Role = roles[1],
             },
            new RolePermission {
                Permission = permissions[9],
                Role = roles[1],
            },

            //
            new RolePermission{
                Permission = permissions[10],
                Role = roles[2],
             },
            new RolePermission {
                Permission = permissions[11],
                Role = roles[2],
            },
            new RolePermission {
                Permission = permissions[12],
                Role = roles[2],
            },
            new RolePermission {
                Permission = permissions[13],
                Role = roles[2],
            },new RolePermission{
                Permission = permissions[14],
                Role = roles[2],
             },
            new RolePermission {
                Permission = permissions[15],
                Role = roles[2],
            },
            new RolePermission {
                Permission = permissions[16],
                Role = roles[2],
            },new RolePermission{
                Permission = permissions[17],
                Role = roles[2],
             },
            new RolePermission {
                Permission = permissions[18],
                Role = roles[2],
            },
        };

            context.RolesPermissions.AddRange(RolePermissions);
            context.SaveChanges();


            // Seed Users
            var user = new User
            {
                Username = "MRS. Jane Doe",
                Password = "password",
                Name = "John Doe",
                Telephone = "+250784339373",
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                Is_active = true,
                Last_login = DateTime.Now,
                RoleId = 2
            };

            var user1 = new User
            {
                Username = "MR. John Doe",
                Password = "password1",
                Name = "John Doe",
                Telephone = "+250786219583",
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                Is_active = true,
                Last_login = DateTime.Now,
                RoleId = 1
            };
            context.Users.Add(user);
            context.Users.Add(user1);
            context.SaveChanges();




            // Seed password reset

            var resetTokens = new[] {
            new PasswordResetToken{
                User = user,
                Token=" eyJjbGllbnRfaWQiOiJZekV6TUdkb01ISm5PSEJpT0cxaWJEaHlOVEE9IiwicmVzcG9uc2VfdHlwZSI6ImNvZGUiLCJzY29wZSI6ImludHJvc2NwZWN0X3Rva2VucywgcmV2b2tlX3Rva2VucyIsImlzcyI6ImJqaElSak0xY1hwYWEyMXpkV3RJU25wNmVqbE1iazQ0YlRsTlpqazNkWEU9Iiwic3ViIjoiWXpFek1HZG9NSEpuT0hCaU9HMWliRGh5TlRBPSIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0Ojg0NDMve3RpZH0ve2FpZH0vb2F1dGgyL2F1dGhvcml6ZSIsImp0aSI6IjE1MTYyMzkwMjIiLCJleHAiOiIyMDIxLTA1LTE3VDA3OjA5OjQ4LjAwMCswNTQ1In0",
                Expiration = DateTime.Now.AddDays(1),
                Created_At = DateTime.Now
            },
            new PasswordResetToken{
                User = user1,
                Token=" eyJjbGllbnRfaWQiOiJZekV6TUdkb01ISm5PSEJpT0cxaWJEaHlOVEE9IiwicmVzcG9uc2VfdHlwZSI6ImNvZGUiLCJzY29wZSI6ImludHJvc2NwZWN0X3Rva2VucywgcmV2b2tlX3Rva2VucyIsImlzcyI6ImJqaElSak0xY1hwYWEyMXpkV3RJU25wNmVqbE1iazQ0YlRsTlpqazNkWEU9Iiwic3ViIjoiWXpFek1HZG9NSEpuT0hCaU9HMWliRGh5TlRBPSIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0Ojg0NDMve3RpZH0ve2FpZH0vb2F1dGgyL2F1dGhvcml6ZSIsImp0aSI6IjE1MTYyMzkwMjIiLCJleHAiOiIyMDIxLTA1LTE3VDA3OjA5OjQ4LjAwMCswNTQ1In0",
                Expiration = DateTime.Now.AddDays(1),
                Created_At = DateTime.Now
            }
        };

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
                User= user
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
                User = user1
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
                Modified_At = DateTime.Now
            };
            context.ProductCategories.Add(productCategory);

            var productCategory1 = new ProductCategory
            {
                Name = "Cleaning Supplies",
                Desc = "Cleaning Supplies category description",
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now
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

            };

            var discount1 = new Discount
            {
                Name = "Valentine Sale",
                Desc = "ValentineSale description",
                Active = false,
                Discount_percent = 5,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now
            };
            context.Discounts.Add(discount);
             context.Discounts.Add(discount1);
            context.SaveChanges(); // Save the discount before referencing it in Product

            // Seed Inventory
            var inventory = new Inventory
            {
                Quantity = 200,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now
            };
            var inventory1 = new Inventory
            {
                Quantity = 51,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now
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
                ProductCategory = productCategory,
                Discount = discount, // Assign the discount to the product
                Inventory = inventory,
                In_stock = true,
                Is_deleted = false,
                 Deleted_At = DateTime.Now,
            };

            var product1 = new Product
            {
                Name = "NIVEA Cleaning Soap",
                Desc = "Laptop description",
                SKU = "ZZK123",
                Price = 39,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                ProductCategory = productCategory1,
                Discount = discount1, // Assign the discount to the product
                Inventory = inventory1,
                In_stock = true,
                Is_deleted = false,
                Deleted_At = DateTime.Now,
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
                ShoppingSession = shoppingSession,
                Sub_total=100
            };

            var cartItem1 = new CartItem
            {
                Quantity = 4,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                Product = product1,
                ShoppingSession = shoppingSession1,
                Sub_total=20
            };
            context.CartItems.Add(cartItem);
            context.CartItems.Add(cartItem1);
            context.SaveChanges();

            // Seed PaymentDetails
            var paymentDetails = new PaymentDetail
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

            var paymentDetails1 = new PaymentDetail
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
            var orderDetails = new OrderDetail
            {
                Total = 1,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                User = user,
                PaymentDetails = paymentDetails,
                Status = "completed"
            };
            var orderDetails1 = new OrderDetail
            {
                Total = 4,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                User = user1,
                PaymentDetails = paymentDetails1,
                Status = "pending"
            };
            context.OrderDetails.Add(orderDetails);
            context.OrderDetails.Add(orderDetails1);
            context.SaveChanges();

            // Seed OrderItems
            var orderItem = new OrderItem
            {
                Quantity = 1,
                Created_At = DateTime.Now,
                Modified_At = DateTime.Now,
                Product = product,
                OrderDetails = orderDetails
            };
            var orderItem1 = new OrderItem
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

