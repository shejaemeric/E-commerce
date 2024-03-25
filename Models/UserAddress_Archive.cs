using System.ComponentModel.DataAnnotations;
namespace E_Commerce_Api.Models
{
    public class UserAddress_Archive
    {
        [Key]
        public int Id{ get; set; }

        public int Archive_Id{ get; set; }
        public string? Address { get; set; }

        public string? Country { get; set; }
        public string? City { get; set; }

        public int UserId { get; set; }

        public string Action { get; set; }

        public DateTime Peformed_At { get; set; }

        public int PeformedById { get; set; } // Foreign key


        //Can be old update or new update
        public string? Record_Type { get; set; }

        public string? Reference_Id { get; set; }
    }
}
