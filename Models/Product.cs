using System.ComponentModel.DataAnnotations;


namespace E_Commerce_Api.Models
{
    public class Product
    {
        public int Id{ get; set; }

        [Required]
        public string Name{ get; set; }

        [MaxLength(int.MaxValue)]
        public string? Desc { get; set; }

        public string? SKU { get; set; }
        public int Price { get; set; }

        public bool In_stock { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        public ICollection<CartItem> CartItems { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

        public Discount Discount { get; set; }

        public Inventory Inventory { get; set; }

        public ProductCategory ProductCategory { get; set; }
    }
}
