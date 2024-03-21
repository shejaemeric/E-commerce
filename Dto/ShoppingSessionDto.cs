
using E_Commerce_Api.Models;
namespace E_Commerce_Api.Dto
{
    public class ShoppingSessionDto
    {
        public int Id{ get; set; }
        public decimal Total{ get; set; }

        public DateTime Created_At { get; set; }

        public DateTime Modified_At { get; set; }

    }
}
