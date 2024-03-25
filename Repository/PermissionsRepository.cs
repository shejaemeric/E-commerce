using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Data;
using E_Commerce_Api.Interfaces;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Repository
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly DataContext _context;
        public  PermissionRepository(DataContext context)
        {
            _context = context;
        }
        public bool CheckIfPermissionExist(int permissionId)
        {
            return _context.Permissions.Any(p => p.Id == permissionId);
        }

        public bool CreatePermission(Permission permission)
        {
            _context.Add(permission);
            return Save();
        }

        public ICollection<Permission> GetAllPermissions()
        {
            return _context.Permissions.OrderBy(p => p.Id).ToList();
        }

        public Permission GetOnePermission(int permissionId)
        {
            return _context.Permissions.FirstOrDefault(p=>p.Id == permissionId);
        }

        public bool IsUserAllowedToPerformAction(int userId, int permissionId)
        {
                // Retrieve the roles associated with the user
                var userRoles = _context.UsersRoles
                    .Where(ur => ur.UserId == userId)
                    .Select(ur => ur.RoleId)
                    .ToList();

                // Retrieve the permissions associated with each role
                var rolePermissions = _context.RolesPermissions
                    .Where(rp => userRoles.Contains(rp.RoleId))
                    .Select(rp => rp.PermissionId)
                    .ToList();

                // Check if the permission is granted to any of the user's roles
                return rolePermissions.Contains(permissionId);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdatePermission(Permission permission)
        {
            _context.Update(permission);
            return Save();
        }
    }
}
