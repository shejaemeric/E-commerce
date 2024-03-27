using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Api.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(int.MaxValue)]
        public string Desc { get; set; }

        public string SKU { get; set; }
        public int Price { get; set; }

        public bool In_stock { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        public ICollection<CartItem> CartItems { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

        public int? DiscountId { get; set; }
        public int? InventoryId { get; set; }
        public int? ProductCategoryId { get; set; }

        [ForeignKey(nameof(DiscountId))]
        public Discount Discount { get; set; }

        [ForeignKey(nameof(InventoryId))]
        public Inventory Inventory { get; set; }

        [ForeignKey(nameof(ProductCategoryId))]
        public ProductCategory ProductCategory { get; set; }
    }
}
