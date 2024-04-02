using E_Commerce_Api.Models;
namespace E_Commerce_Api.Dto
{
    public class CreateUserDto
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
    }
}
