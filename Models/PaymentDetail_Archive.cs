
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Api.Models
{
    public class PaymentDetail_Archive
    {
        [Key]
        public int Id{ get; set; }

        public int Archive_Id{ get; set; }

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

        public string Action { get; set; }

        public DateTime Peformed_At { get; set; }

        public int PeformedById { get; set; } // Foreign key


        //Can be old update or new update
        public string? Record_Type { get; set; }

        public string? Reference_Id { get; set; }

    }
}
