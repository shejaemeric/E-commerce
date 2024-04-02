using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Interfaces
{
    public interface IPermissionRepository
    {

        public bool Save();

        Permission GetOnePermission(int permissionId);

        public bool CheckIfPermissionExist(int permissionId);

        public bool CreatePermission(Permission permission);

        public bool UpdatePermission(Permission permission);

        public ICollection<Permission> GetAllPermissions();

        bool DeletePermission(int permissionId,int actionPeformerId, string referenceId);

        // public bool IsUserAllowedToPerformAction(int userId, int permissionId);

    }
}
