
using System.ComponentModel.DataAnnotations;


namespace E_Commerce_Api.Models
{
    public class UserPayment
    {

        public int Id{ get; set; }

        [Required]
        public string Payment_Type{ get; set; }

        public string? Card_Holder_Name { get; set; }

         public long? Card_Number { get; set; }
        public int? CVV { get; set; }
        public int? Expiration_Month { get; set; }
        public int? Expiration_Year { get; set; }
        public DateTime Created_At { get; set; }

        public User User { get; set; }

    }
}
