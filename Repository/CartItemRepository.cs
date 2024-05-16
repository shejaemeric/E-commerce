using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Data;
using E_Commerce_Api.Models;
using E_Commerce_Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Api.Repository
{
    public class CartItemRepository : ICartItemRepository
    {

        private readonly DataContext _context;
        public CartItemRepository(DataContext context)
        {
            _context = context;
        }

        public bool CheckIfCartItemExist(int cartItemId)
        {
            return _context.CartItems.Any(ci => ci.Id == cartItemId);
        }

        public bool CreateCartItem(int productId, int shoppingSessionId, CartItem cartItem)
        {
            var product = _context.Products.Find(productId);
            if ( product == null) {
                return false;
            }

            var shoppingSession = _context.ShoppingSessions.Find(shoppingSessionId);
            if (shoppingSession == null) {
                return false;
            }
            cartItem.Product = product;
            cartItem.ShoppingSession = shoppingSession;
            _context.Add(cartItem);
            return Save();
        }




        public CartItem GetOneCartItem(int cartItemId)
        {
            return _context.CartItems.Where(ci => ci.Id == cartItemId).Include(p=>p.Product).FirstOrDefault();
        }
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateCartItem( int productId, int shoppingSessionId, CartItem cartItem,int actionPeformerId, string referenceId)
        {
            var product = _context.Products.Find(productId);
            if ( product == null) {
                return false;
            }

            var shoppingSession = _context.ShoppingSessions.Find(shoppingSessionId);
            if (shoppingSession == null) {
                return false;
            }

            try  {
                string query = " Exec  [dbo].[proc_updateCartItem] "  + cartItem.Id + "," + actionPeformerId + ", '" + referenceId + "'; ";
                var cmd = _context.Database.ExecuteSqlRaw(query);
                cartItem.Product = product;
                cartItem.ShoppingSession = shoppingSession;
                _context.Update(cartItem);
                return Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool DeleteCartItem(int cartItemId, int actionPeformerId, string referenceId)
        {
            try {
                string query = " Exec  [dbo].[proc_deleteCartItem] "  + cartItemId + "," + actionPeformerId + ", '" + referenceId + "'; ";
                var cmd = _context.Database.ExecuteSqlRaw(query);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool IsCartOwner(int userId, int cartItemId)
        {
            var cartItem = _context.CartItems.Find(cartItemId);
            if (cartItem == null) return false;
            return cartItem.ShoppingSession.User.Id == userId;
        }
    }
}
