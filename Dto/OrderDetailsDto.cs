using E_Commerce_Api.Models;
namespace E_Commerce_Api.Dto
{
    public class OrderDetailsDto
    {
        public int Id{ get; set; }

        public int Total{ get; set; }

        public  string Status { get; set; }

        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

    }
}
