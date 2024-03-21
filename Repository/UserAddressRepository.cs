using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;
using E_Commerce_Api.Data;
using E_Commerce_Api.Interfaces;

namespace E_Commerce_Api.Repository
{
    public class UserAddressRepository : IUserAddressRepository
    {
        private readonly DataContext _context;
        public UserAddressRepository(DataContext context)
        {
            _context = context;
        }

        public bool CheckIfUserAddressExist(int userAddressId)
        {
            return _context.UserAddresses.Any(ua => ua.Id ==userAddressId);
        }

        public bool CreateUserAddress(int userId, UserAddress userAddress)
        {
            var user = _context.Users.Find(userId);
            if (user == null) {
                return false;
            }

            userAddress.User = user;
            _context.Add(userAddress);
            return Save();
        }

        public ICollection<UserAddress> GetAllUserAddressByUser(int userId)
        {
            return _context.UserAddresses.Where(ua => ua.User.Id == userId).ToList();
        }

        public UserAddress GetOneUserAddress(int userAddressId)
        {
            return _context.UserAddresses.FirstOrDefault(ua => ua.Id == userAddressId);
        }
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateUserAddress( int userId, UserAddress userAddress)
        {
            var user = _context.Users.Find(userId);
            if (user == null) {
                return false;
            }

            userAddress.User = user;
            _context.Update(userAddress);
            return Save();
        }
    }
}
