

namespace E_Commerce_Api.Models
{
    public class User
    {
        public int Id{ get; set; }
        public string Username { get; set; }

        public string? Verification_Token { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Telephone { get; set; }

        public DateTime? VerifiedAt { get; set; }

        public bool? Verified { get; set; }


        public bool Is_active { get; set; }
        public DateTime Last_login { get; set; }
            public DateTime Created_At { get; set; }
             public DateTime Modified_At { get; set; }

            public int RoleId { get; set; } // Foreign key
            public Role Role { get; set;}


             public ICollection<PasswordResetToken> PasswordResetTokens { get; set; }

            public ICollection<UserAddress> UserAddresses { get; set;}

            public ICollection< UserPayment> UserPayments  { get; set;}

            public ICollection< ShoppingSession> ShoppingSessions { get; set;}

            public ICollection< OrderDetail> OrderDetails  { get; set;}

    }
}
