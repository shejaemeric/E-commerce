
namespace E_Commerce_Api.Models
{
    public class CartItem
    {
        public int Id{ get; set; }

        public int Quantity{ get; set; }

        public decimal Sub_total{ get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        public Product Product { get; set; }

        public ShoppingSession ShoppingSession { get; set; }

    }
}
