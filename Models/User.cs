

namespace E_Commerce_Api.Models
{
    public class User
    {
        public int Id{ get; set; }
        public string Username { get; set; }

         public string Password { get; set; }

          public string Name { get; set; }

           public string Telephone { get; set; }


        public bool Is_active { get; set; }
        public DateTime Last_login { get; set; }
            public DateTime Created_At { get; set; }
             public DateTime Modified_At { get; set; }

             public ICollection<PasswordResetToken> PasswordResetTokens { get; set; }

             public ICollection<UserRole> UserRoles { get; set; }

            public ICollection<UserAddress> UserAddresses { get; set;}

            public ICollection< UserPayment> UserPayments  { get; set;}

            public ICollection< ShoppingSession> ShoppingSessions { get; set;}

            public ICollection< OrderDetail> OrderDetails  { get; set;}

    }
}
