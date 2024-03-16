

namespace E_Commerce_Api.Models
{
    public class ShoppingSession
    {
        public int Id{ get; set; }
        public decimal Total{ get; set; }

        public DateTime Created_At { get; set; }

        public DateTime Modified_At { get; set; }

        public User User { get; set; }

        public ICollection<CartItem> CartItems { get; set; }

    }
}
