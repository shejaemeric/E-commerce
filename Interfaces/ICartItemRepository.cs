using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Interfaces
{
public interface ICartItemRepository
{
        ICollection<CartItem> GetAllCartItemsBySession(int sessionId);

        CartItem GetOneCartItem(int cartItemId);
        bool CheckIfCartItemExist(int cartItemId);
        bool CreateCartItem(int productId,int shoppingSessionId,CartItem cartItem);
        bool UpdateCartItem(int productId,int shoppingSessionId,CartItem cartItem,int actionPeformerId, string referenceId);

        bool DeleteCartItem(int cartItemId, int actionPeformerId, string referenceId);
        bool IsCartOwner(int userId,int cartItemId);
        bool Save();
}
}
