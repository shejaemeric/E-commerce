
using System.ComponentModel.DataAnnotations;


namespace E_Commerce_Api.Models
{
    public class PaymentDetails
    {
        public int Id{ get; set; }

        public int Amount{ get; set; }

        [Required]
        public string Status{ get; set; }

        [Required]
        public string Payment_Type{ get; set; }

        public string? Card_Holder_Name { get; set; }

         public long? Card_Number { get; set; }
        public int? CVV { get; set; }
        public int? Expiration_Month { get; set; }
        public int? Expiration_Year { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }

    }
}
