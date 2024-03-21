using System.ComponentModel.DataAnnotations;


namespace E_Commerce_Api.Models
{
    public class Product_Archive
    {
        public int Id{ get; set; }

        [Required]
        public string Name{ get; set; }

        [MaxLength(int.MaxValue)]
        public string? Desc { get; set; }

        public string? SKU { get; set; }
        public int Price { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        public int DiscountId { get; set; }

        public int Inventory_Archive { get; set; }

        public int ProductCategoryId { get; set; }

        public string Action { get; set; }

        public DateTime Peformed_At { get; set; }

        public int PeformedById { get; set; } // Foreign key


        //Can be old update or new update
        public string? Record_Type { get; set; }

        public string? Reference_Id { get; set; }
    }
}
