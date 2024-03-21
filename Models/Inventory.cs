namespace E_Commerce_Api.Models
{
    public class Inventory
    {

        public int Id{ get; set; }

        public int Quantity{ get; set; }


        public DateTime Created_At { get; set; }


        public DateTime Modified_At { get; set; }


        public ICollection<Product> Products { get; set; }
    }
}
