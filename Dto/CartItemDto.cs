
using E_Commerce_Api.Models;
namespace E_Commerce_Api.Dto
{
    public class CartItemDto
    {
        public int Id{ get; set; }

        public int Quantity{ get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

       // public Product Product { get; set; }

    }
}
