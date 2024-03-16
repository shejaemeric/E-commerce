
namespace E_Commerce_Api.Models
{
    public class UserAddress
    {
        public int Id{ get; set; }
        public string? Address { get; set; }

        public string? Country { get; set; }
        public string? City { get; set; }

        public User User { get; set; }
    }
}
