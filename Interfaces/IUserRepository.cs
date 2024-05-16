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



        User GetOneUsers(int userId);

        bool CheckIfUserExist(int userId);

         bool DeleteUser(int userId, int actionPeformerId, string referenceId);

        string CreateUser(User user);

        bool UpdateUser(User user,int actionPeformerId, string referenceId);

        bool IsUserOwner(int userId,int ownerId);

        ICollection<OrderDetail> GetAllOrderByUser(int userId);

        ShoppingSession GetLatestShoppingSessionByUser(int userId);

        public ICollection<PasswordResetToken> GetUnexpiredPasswordResetTokensByUser(int userId);
        bool Save();

    }
}
