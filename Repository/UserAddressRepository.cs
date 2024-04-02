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

        public bool UpdateUserAddress( int userId, UserAddress userAddress,int actionPeformerId, string referenceId)
        {
            var user = _context.Users.Find(userId);
            if (user == null) {
                return false;
            }
            try  {
                string query = " Exec  [dbo].[proc_updateUserAddress] "  + userAddress.Id + "," + actionPeformerId + ", '" + referenceId + "'; ";
                var cmd = _context.Database.ExecuteSqlRaw(query);

                userAddress.User = user;
                _context.Update(userAddress);
                return Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }


        }

        public bool DeleteUserAddress(int addressId, int actionPeformerId, string referenceId)
        {
            try {
                string query = " Exec  [dbo].[proc_deleteUserAddress] "  + addressId + "," + actionPeformerId + ", '" + referenceId + "'; ";
                var cmd = _context.Database.ExecuteSqlRaw(query);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool IsUserAddressOwner(int userId, int addressId)
        {
            var address = _context.UserAddresses.Find(addressId);
            if (address == null) return false;
            return address.User.Id == userId;
        }
    }
}
