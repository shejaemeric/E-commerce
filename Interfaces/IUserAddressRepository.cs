using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Interfaces
{
    public interface IUserAddressRepository
    {

bool Save();

        UserAddress GetOneUserAddress(int userAddressId);

        bool CheckIfUserAddressExist(int userAddressId);

        bool CreateUserAddress(int userId,UserAddress userAddress);

        bool UpdateUserAddress(int userId,UserAddress userAddress,int actionPeformerId, string referenceId);

        bool DeleteUserAddress(int addressId, int actionPeformerId, string referenceId);

        bool IsUserAddressOwner(int userId,int addressId);
        public ICollection<UserAddress> GetAllUserAddressByUser(int userId);
        public ICollection<UserAddress> GetAllUserAddresses();
    }
}
