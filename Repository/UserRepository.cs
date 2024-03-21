using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;
using E_Commerce_Api.Data;
using E_Commerce_Api.Interfaces;

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

        public bool UpdateUser( User user)
        {
            _context.Update(user);
            return Save();
        }
    }
}
