using System.ComponentModel.DataAnnotations;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Dto
{
    public class ProductDto{
        public int Id{ get; set; }

        [Required]
        public string Name{ get; set; }

        [MaxLength(int.MaxValue)]
        public string? Desc { get; set; }

        public string? SKU { get; set; }
        public int Price { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }


    }
}
