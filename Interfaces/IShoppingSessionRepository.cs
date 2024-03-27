using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Interfaces
{
    public interface IShoppingSessionRepository
    {
        ShoppingSession GetLatestShoppingSessionByUser(int userId);
        bool CheckIfShoppingSessionExist(int shoppingSessionId);
        bool CreateShoppingSession(int userId,ShoppingSession shoppingSession);

        bool UpdateShoppingSession(int userId,ShoppingSession shoppingSession,int actionPeformerId, string referenceId);
        bool DeleteShoppingSession(int shoppingSessionId, int actionPeformerId, string referenceId);
        bool Save();
    }
}
