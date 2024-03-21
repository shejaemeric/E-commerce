using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;
using E_Commerce_Api.Data;
using E_Commerce_Api.Interfaces;

namespace E_Commerce_Api.Repository
{
    public class UserPaymentRepository : IUserPaymentRepository
    {
        private readonly DataContext _context;
        public UserPaymentRepository(DataContext context)
        {
            _context = context;
        }

        public bool CheckIfUserPaymentExist(int userPaymentId)
        {
            return _context.UserPayments.Any(up => up.Id ==userPaymentId);
        }

        public bool CreateUserPayment(int userId, UserPayment userPayment)
        {
            var user = _context.Users.Find(userId);
            if (user == null) {
                return false;
            }

            userPayment.User = user;
            _context.Add( userPayment);
            return Save();
        }

        public UserPayment GetOneUserPayment(int userPaymentId)
        {
            return _context.UserPayments.FirstOrDefault(up => up.Id == userPaymentId);
        }

        public ICollection<UserPayment> GetAllUserPaymentByUser(int userId)
        {
            return _context.UserPayments.Where(up => up.User.Id == userId).ToList();
        }
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateUserPayment( int userId, UserPayment userPayment)
        {
            var user = _context.Users.Find(userId);
            if (user == null) {
                return false;
            }

            userPayment.User = user;
            _context.Update(userPayment);
            return Save();
        }
    }
}
