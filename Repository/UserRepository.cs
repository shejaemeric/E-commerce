using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;
using E_Commerce_Api.Data;
using E_Commerce_Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Api.Repository
{
    public class UserRepository : IUserRepository
    {
                private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public bool CheckIfUserExist(int userId)
        {
            return _context.Users.Any(u => u.Id ==userId);
        }

        public ICollection<User> GetAllUsers()
        {
            return _context.Users.OrderBy(u => u.Id).ToList();
        }

        public User GetOneUsers(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }
        public ICollection<UserPayment> GetAllUserPaymentsByUser(int userId)
        {
            return _context.UserPayments.Where(up => up.User.Id == userId).ToList();
        }

        public ICollection<UserAddress> GetAllUserAddressesByUser(int userId)
        {
            return _context.UserAddresses.OrderBy(ua => ua.User.Id == userId).ToList();
        }
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool UpdateUser( User user,int actionPeformerId, string referenceId)
        {
            try  {
                string query = " Exec  [dbo].[proc_updateUser] "  + user.Id + "," + actionPeformerId + ", '" + referenceId + "'; ";
                var cmd = _context.Database.ExecuteSqlRaw(query);
                _context.Update(user);
                return Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public ICollection<User> GetAllUsersByRole(int roleId)
        {
            return _context.UsersRoles.Where(ur => ur.RoleId == roleId).Select(u => u.User).ToList();
        }

        public ICollection<User> GetAllUsersByPermission(int permissionId)
        {
            var userRoles = _context.RolesPermissions.Where(rp => rp.PermissionId == permissionId).Select(p => p.RoleId).ToList();
            return _context.UsersRoles.Where(ur => userRoles.Contains(ur.RoleId)).Select(u => u.User).ToList();
        }

        public bool DeleteUser(int userId, int actionPeformerId, string referenceId)
        {
            try{
            string query = "Exec  [dbo].[proc_deleteUser] "  + userId + "," + actionPeformerId + ", '" + referenceId + "'; ";
                var cmd = _context.Database.ExecuteSqlRaw(query);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
