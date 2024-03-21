using E_Commerce_Api.Models;
namespace E_Commerce_Api.Dto
{
    public class UserAddressDto
    {
        public int Id{ get; set; }
        public string? Address { get; set; }

        public string? Country { get; set; }
        public string? City { get; set; }
    }
}
