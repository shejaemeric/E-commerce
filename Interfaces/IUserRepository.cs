using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetAllUsers();

        ICollection<UserAddress> GetAllUserAddressesByUser(int userId);

        ICollection<UserPayment> GetAllUserPaymentsByUser(int userId);

        public ICollection<User> GetAllUsersByRole(int roleId);

        public ICollection<User> GetAllUsersByPermission(int permissionId);
        User GetOneUsers(int userId);

        bool CheckIfUserExist(int userId);

         bool DeleteUser(int userId, int actionPeformerId, string referenceId);

        bool CreateUser(User user);

        bool UpdateUser(User user,int actionPeformerId, string referenceId);
        bool Save();

    }
}
