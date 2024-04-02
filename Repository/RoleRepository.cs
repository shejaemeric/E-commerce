using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Data;
using E_Commerce_Api.Interfaces;
using E_Commerce_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Api.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;
        public  RoleRepository(DataContext context)
        {
            _context = context;
        }
        public bool CheckIfRoleExist(int roleId)
        {
            return _context.Roles.Any(r => r.Id == roleId);
        }

        public bool CreateRole(Role role)
        {
            _context.Add(role);
            return Save();
        }

        public bool DeleteRole(int roleId,int actionPeformerId, string referenceId)
        {
            try {
                string query = " Exec  [dbo].[proc_deleteRole] "  + roleId + "," + actionPeformerId + ", '" + referenceId + "'; ";
                var cmd = _context.Database.ExecuteSqlRaw(query);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public ICollection<Role> GetAllRoles()
        {
            return  _context.Roles.OrderBy(r => r.Id).ToList();
        }

        public Role GetOneRole(int roleId)
        {
            return _context.Roles.FirstOrDefault(r => r.Id == roleId);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateRole(Role role)
        {
            _context.Update(role);
            return Save();
        }
    }
}
