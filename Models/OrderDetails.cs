namespace E_Commerce_Api.Models
{
    public class OrderDetails
    {
        public int Id{ get; set; }

        public int Total{ get; set; }

        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        public User User { get; set; }
        public PaymentDetails PaymentDetails { get; set; }

        public ICollection<OrderItems> OrderItems {get; set; }
    }
}
