
using System.ComponentModel.DataAnnotations;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Dto
{
    public class UserPaymentDto
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

    }
}
