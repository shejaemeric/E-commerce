

namespace E_Commerce_Api.Models
{
    public class User
    {
        public int Id{ get; set; }
        public string Username { get; set; }

         public string Password { get; set; }

          public string Name { get; set; }

           public string Telephone { get; set; }
            public DateTime Created_At { get; set; }
             public DateTime Modified_At { get; set; }

            public UserAddress UserAddress { get; set;}

            public UserPayment UserPayment  { get; set;}
    }
}
