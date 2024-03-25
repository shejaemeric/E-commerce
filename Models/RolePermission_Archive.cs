using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Models
{
    public class RolePermission_Archive
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }


        public string Action { get; set; }

        public DateTime Peformed_At { get; set; }

           public int PeformedById { get; set; } // Foreign key


        //Can be old update or new update
        public string? Record_Type { get; set; }

        public string? Reference_Id { get; set; }
    }
}
