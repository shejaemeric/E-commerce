using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Models
{
    public class PasswordResetToken
    {
         public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime Created_At { get; set; }
    }
}
