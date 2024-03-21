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

        public ICollection<CartItem> GetAllCartItemsBySession(int sessionId)
        {
            return _context.CartItems.Where(ci => ci.ShoppingSession.Id == sessionId).Include(p=>p.Product).ToList();
        }

        public CartItem GetOneCartItem(int cartItemId)
        {
            return _context.CartItems.Where(ci => ci.Id == cartItemId).Include(p=>p.Product).FirstOrDefault();
        }
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateCartItem( int productId, int shoppingSessionId, CartItem cartItem)
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
            _context.Update(cartItem);
            return Save();
        }
    }
}
