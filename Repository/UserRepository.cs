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
                private readonly ITokenServices _tokenServices;
        public UserRepository(DataContext context,ITokenServices tokenServices)
        {
            _context = context;
            _tokenServices = tokenServices;
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
            return _context.UserAddresses.Where(ua => ua.User.Id == userId).ToList();
        }
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public string CreateUser(User user)
        {
            user.Verification_Token = _tokenServices.GenerateToken(user.Email);
            _context.Add(user);

            if (Save()) {
                return user.Verification_Token;
            }
            return "" ;
        }

        public bool UpdateUser( User user,int actionPeformerId, string referenceId)
        {
            var oldUser = _context.Users.Where(u => u.Id == user.Id).FirstOrDefault();
            user.RoleId = oldUser.RoleId;
            _context.ChangeTracker.Clear();

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


        public bool DeleteUser(int userId, int actionPeformerId, string referenceId)
        {
            Console.WriteLine("deleting" + userId + "-" +actionPeformerId+ "-" +referenceId);
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

        public ICollection<OrderDetail> GetAllOrderByUser(int userId)
        {
            return _context.OrderDetails.Where(od => od.User.Id == userId).Include(p=>p.PaymentDetails).ToList();
        }

        public ShoppingSession GetLatestShoppingSessionByUser(int userId)
        {
            return _context.ShoppingSessions.Where(c => c.User.Id == userId).OrderByDescending(ss => ss.Modified_At).FirstOrDefault();
        }



        public bool IsUserOwner(int userId, int ownerId)
        {
            return userId == ownerId;
        }

        public ICollection<PasswordResetToken> GetUnexpiredPasswordResetTokensByUser(int userId)
        {
           return _context.PasswordResetTokens.Where(pt => pt.User.Id == userId && pt.Expiration < DateTime.Now).ToList();
        }

        public bool UpdateUserRole(int userId, int RoleId)
        {
            var user = new User { Id = userId };
            _context.Users.Attach(user);
            user.RoleId = RoleId;
            _context.Entry(user).Property(u => u.RoleId).IsModified = true;
            return Save();
        }
    }
}
