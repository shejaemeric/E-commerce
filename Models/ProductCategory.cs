
using System.ComponentModel.DataAnnotations;


namespace E_Commerce_Api.Models
{
    public class ProductCategory
    {

        public int Id{ get; set; }

        [Required]
        public string Name{ get; set; }

        [MaxLength(int.MaxValue)]
        public string? Desc { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
