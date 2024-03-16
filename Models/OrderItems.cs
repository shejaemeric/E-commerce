

namespace E_Commerce_Api.Models
{
    public class OrderItems
    {
        public int Id{ get; set; }

        public int Quantity{ get; set; }

        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        public OrderDetails OrderDetails { get; set; }

        public Product Product { get; set; }
    }
}
