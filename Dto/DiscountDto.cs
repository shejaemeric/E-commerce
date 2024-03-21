
using System.ComponentModel.DataAnnotations;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Dto
{
    public class DiscountDto
    {
        public int Id{ get; set; }

        [Required]
        public string Name{ get; set; }

        [MaxLength(int.MaxValue)]
        public string? Desc { get; set; }

        public bool Active { get; set; }
        public decimal Discount_percent { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }
    }
}
