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

        bool UpdateUserAddress(int userId,UserAddress userAddress);
        public ICollection<UserAddress> GetAllUserAddressByUser(int userId);
    }
}
