﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceApi.Migrations
{
    /// <inheritdoc />
    public partial class implementarchives : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartItems_Archive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(name: "Modified_At", type: "datetime2", nullable: false),
                    ProductId = table.Column<int>(name: "Product_Id", type: "int", nullable: false),
                    ShoppingSessionId = table.Column<int>(name: "ShoppingSession_Id", type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeformedAt = table.Column<DateTime>(name: "Peformed_At", type: "datetime2", nullable: false),
                    PeformedById = table.Column<int>(type: "int", nullable: false),
                    RecordType = table.Column<string>(name: "Record_Type", type: "nvarchar(max)", nullable: true),
                    ReferenceId = table.Column<string>(name: "Reference_Id", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems_Archive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Discountpercent = table.Column<decimal>(name: "Discount_percent", type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(name: "Modified_At", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discounts_Archive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Discountpercent = table.Column<decimal>(name: "Discount_percent", type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(name: "Modified_At", type: "datetime2", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeformedAt = table.Column<DateTime>(name: "Peformed_At", type: "datetime2", nullable: false),
                    PeformedById = table.Column<int>(type: "int", nullable: false),
                    RecordType = table.Column<string>(name: "Record_Type", type: "nvarchar(max)", nullable: true),
                    ReferenceId = table.Column<string>(name: "Reference_Id", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts_Archive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(name: "Modified_At", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventories_Archive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(name: "Modified_At", type: "datetime2", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeformedAt = table.Column<DateTime>(name: "Peformed_At", type: "datetime2", nullable: false),
                    PeformedById = table.Column<int>(type: "int", nullable: false),
                    RecordType = table.Column<string>(name: "Record_Type", type: "nvarchar(max)", nullable: true),
                    ReferenceId = table.Column<string>(name: "Reference_Id", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories_Archive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails_Archive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(name: "Modified_At", type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PaymentDetailsId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeformedAt = table.Column<DateTime>(name: "Peformed_At", type: "datetime2", nullable: false),
                    PeformedById = table.Column<int>(type: "int", nullable: false),
                    RecordType = table.Column<string>(name: "Record_Type", type: "nvarchar(max)", nullable: true),
                    ReferenceId = table.Column<string>(name: "Reference_Id", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails_Archive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems_Archive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(name: "Modified_At", type: "datetime2", nullable: false),
                    OrderDetailId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeformedAt = table.Column<DateTime>(name: "Peformed_At", type: "datetime2", nullable: false),
                    PeformedById = table.Column<int>(type: "int", nullable: false),
                    RecordType = table.Column<string>(name: "Record_Type", type: "nvarchar(max)", nullable: true),
                    ReferenceId = table.Column<string>(name: "Reference_Id", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems_Archive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentType = table.Column<string>(name: "Payment_Type", type: "nvarchar(max)", nullable: false),
                    CardHolderName = table.Column<string>(name: "Card_Holder_Name", type: "nvarchar(max)", nullable: true),
                    CardNumber = table.Column<long>(name: "Card_Number", type: "bigint", nullable: true),
                    CVV = table.Column<int>(type: "int", nullable: true),
                    ExpirationMonth = table.Column<int>(name: "Expiration_Month", type: "int", nullable: true),
                    ExpirationYear = table.Column<int>(name: "Expiration_Year", type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(name: "Modified_At", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentDetails_Archive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentType = table.Column<string>(name: "Payment_Type", type: "nvarchar(max)", nullable: false),
                    CardHolderName = table.Column<string>(name: "Card_Holder_Name", type: "nvarchar(max)", nullable: true),
                    CardNumber = table.Column<long>(name: "Card_Number", type: "bigint", nullable: true),
                    CVV = table.Column<int>(type: "int", nullable: true),
                    ExpirationMonth = table.Column<int>(name: "Expiration_Month", type: "int", nullable: true),
                    ExpirationYear = table.Column<int>(name: "Expiration_Year", type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(name: "Modified_At", type: "datetime2", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeformedAt = table.Column<DateTime>(name: "Peformed_At", type: "datetime2", nullable: false),
                    PeformedById = table.Column<int>(type: "int", nullable: false),
                    RecordType = table.Column<string>(name: "Record_Type", type: "nvarchar(max)", nullable: true),
                    ReferenceId = table.Column<string>(name: "Reference_Id", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetails_Archive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(name: "Modified_At", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories_Archive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(name: "Modified_At", type: "datetime2", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeformedAt = table.Column<DateTime>(name: "Peformed_At", type: "datetime2", nullable: false),
                    PeformedById = table.Column<int>(type: "int", nullable: false),
                    RecordType = table.Column<string>(name: "Record_Type", type: "nvarchar(max)", nullable: true),
                    ReferenceId = table.Column<string>(name: "Reference_Id", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories_Archive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products_Archive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(name: "Modified_At", type: "datetime2", nullable: false),
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    InventoryArchive = table.Column<int>(name: "Inventory_Archive", type: "int", nullable: false),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeformedAt = table.Column<DateTime>(name: "Peformed_At", type: "datetime2", nullable: false),
                    PeformedById = table.Column<int>(type: "int", nullable: false),
                    RecordType = table.Column<string>(name: "Record_Type", type: "nvarchar(max)", nullable: true),
                    ReferenceId = table.Column<string>(name: "Reference_Id", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products_Archive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingSessions_Archive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(name: "Modified_At", type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeformedAt = table.Column<DateTime>(name: "Peformed_At", type: "datetime2", nullable: false),
                    PeformedById = table.Column<int>(type: "int", nullable: false),
                    RecordType = table.Column<string>(name: "Record_Type", type: "nvarchar(max)", nullable: true),
                    ReferenceId = table.Column<string>(name: "Reference_Id", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingSessions_Archive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAddresses_Archive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeformedAt = table.Column<DateTime>(name: "Peformed_At", type: "datetime2", nullable: false),
                    PeformedById = table.Column<int>(type: "int", nullable: false),
                    RecordType = table.Column<string>(name: "Record_Type", type: "nvarchar(max)", nullable: true),
                    ReferenceId = table.Column<string>(name: "Reference_Id", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddresses_Archive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPayments_Archive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentType = table.Column<string>(name: "Payment_Type", type: "nvarchar(max)", nullable: false),
                    CardHolderName = table.Column<string>(name: "Card_Holder_Name", type: "nvarchar(max)", nullable: true),
                    CardNumber = table.Column<long>(name: "Card_Number", type: "bigint", nullable: true),
                    CVV = table.Column<int>(type: "int", nullable: true),
                    ExpirationMonth = table.Column<int>(name: "Expiration_Month", type: "int", nullable: true),
                    ExpirationYear = table.Column<int>(name: "Expiration_Year", type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    User = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeformedAt = table.Column<DateTime>(name: "Peformed_At", type: "datetime2", nullable: false),
                    PeformedById = table.Column<int>(type: "int", nullable: false),
                    RecordType = table.Column<string>(name: "Record_Type", type: "nvarchar(max)", nullable: true),
                    ReferenceId = table.Column<string>(name: "Reference_Id", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPayments_Archive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(name: "Modified_At", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users_Archive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(name: "Modified_At", type: "datetime2", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeformedAt = table.Column<DateTime>(name: "Peformed_At", type: "datetime2", nullable: false),
                    PeformedById = table.Column<int>(type: "int", nullable: false),
                    RecordType = table.Column<string>(name: "Record_Type", type: "nvarchar(max)", nullable: true),
                    ReferenceId = table.Column<string>(name: "Reference_Id", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_Archive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(name: "Modified_At", type: "datetime2", nullable: false),
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(name: "Modified_At", type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PaymentDetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_PaymentDetails_PaymentDetailsId",
                        column: x => x.PaymentDetailsId,
                        principalTable: "PaymentDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(name: "Modified_At", type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingSessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAddresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentType = table.Column<string>(name: "Payment_Type", type: "nvarchar(max)", nullable: false),
                    CardHolderName = table.Column<string>(name: "Card_Holder_Name", type: "nvarchar(max)", nullable: true),
                    CardNumber = table.Column<long>(name: "Card_Number", type: "bigint", nullable: true),
                    CVV = table.Column<int>(type: "int", nullable: true),
                    ExpirationMonth = table.Column<int>(name: "Expiration_Month", type: "int", nullable: true),
                    ExpirationYear = table.Column<int>(name: "Expiration_Year", type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPayments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(name: "Modified_At", type: "datetime2", nullable: false),
                    OrderDetailsId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_OrderDetails_OrderDetailsId",
                        column: x => x.OrderDetailsId,
                        principalTable: "OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(name: "Modified_At", type: "datetime2", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ShoppingSessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_ShoppingSessions_ShoppingSessionId",
                        column: x => x.ShoppingSessionId,
                        principalTable: "ShoppingSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ShoppingSessionId",
                table: "CartItems",
                column: "ShoppingSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_PaymentDetailsId",
                table: "OrderDetails",
                column: "PaymentDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_UserId",
                table: "OrderDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderDetailsId",
                table: "OrderItems",
                column: "OrderDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DiscountId",
                table: "Products",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_InventoryId",
                table: "Products",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingSessions_UserId",
                table: "ShoppingSessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPayments_UserId",
                table: "UserPayments",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "CartItems_Archive");

            migrationBuilder.DropTable(
                name: "Discounts_Archive");

            migrationBuilder.DropTable(
                name: "Inventories_Archive");

            migrationBuilder.DropTable(
                name: "OrderDetails_Archive");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "OrderItems_Archive");

            migrationBuilder.DropTable(
                name: "PaymentDetails_Archive");

            migrationBuilder.DropTable(
                name: "ProductCategories_Archive");

            migrationBuilder.DropTable(
                name: "Products_Archive");

            migrationBuilder.DropTable(
                name: "ShoppingSessions_Archive");

            migrationBuilder.DropTable(
                name: "UserAddresses");

            migrationBuilder.DropTable(
                name: "UserAddresses_Archive");

            migrationBuilder.DropTable(
                name: "UserPayments");

            migrationBuilder.DropTable(
                name: "UserPayments_Archive");

            migrationBuilder.DropTable(
                name: "Users_Archive");

            migrationBuilder.DropTable(
                name: "ShoppingSessions");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "PaymentDetails");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "ProductCategories");
        }
    }
}
