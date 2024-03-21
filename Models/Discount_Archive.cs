
using System.ComponentModel.DataAnnotations;


namespace E_Commerce_Api.Models
{
    public class Discount_Archive
    {
        public int Id{ get; set; }

        [Required]
        public string Name{ get; set; }

        [MaxLength(int.MaxValue)]
        public string? Desc { get; set; }

        public bool Active { get; set; }
        public decimal Discount_percent { get; set; }
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
