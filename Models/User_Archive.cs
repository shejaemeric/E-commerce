

namespace E_Commerce_Api.Models
{
    public class User_Archive
    {
        public int Id{ get; set; }
        public string Username { get; set; }

         public string Password { get; set; }

        public string Name { get; set; }

        public string Telephone { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        public string Action { get; set; }

        public DateTime Peformed_At { get; set; }

        public int PeformedById { get; set; } // Foreign key


        //Can be old update or new update
        public string? Record_Type { get; set; }

        public string? Reference_Id { get; set; }
    }
}
