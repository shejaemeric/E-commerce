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
    public class ShoppingSessionRepository : IShoppingSessionRepository
    {
        private readonly DataContext _context;
        public ShoppingSessionRepository(DataContext context)
        {
            _context = context;
        }

        public bool CheckIfShoppingSessionExist(int shoppingSessionId)
        {
            return _context.ShoppingSessions.Any(ss=>ss.Id ==shoppingSessionId);
        }

        public bool CreateShoppingSession(int userId, ShoppingSession shoppingSession)
        {
            var user = _context.Users.Find(userId);
            if (user == null) {
                return false;
            }

            shoppingSession.User = user;
            _context.Add(shoppingSession);
            return Save();
        }

        public bool DeleteShoppingSession(int shoppingSessionId, int actionPeformerId, string referenceId)
        {
                try    {
                string query = " Exec  [dbo].[proc_deleteShoppingSession] "  + shoppingSessionId + "," + actionPeformerId + ", '" + referenceId + "'; ";
                    var cmd = _context.Database.ExecuteSqlRaw(query);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
            }
        }

        public ShoppingSession GetLatestShoppingSessionByUser(int userId)
        {
            return _context.ShoppingSessions.Where(c => c.User.Id == userId).OrderByDescending(ss => ss.Modified_At).FirstOrDefault();
        }

        public bool IsShoppingSessionOwner(int userId, int shoppingSessionId)
        {
            var shoppingSession = _context.ShoppingSessions.Find(shoppingSessionId);
            if (shoppingSession == null) return false;
            return shoppingSession.User.Id == userId;
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateShoppingSession( int userId, ShoppingSession shoppingSession,int actionPeformerId, string referenceId)
        {
            var user = _context.Users.Find(userId);
            if (user == null) {
                return false;
            }

            try  {
                string query = " Exec  [dbo].[proc_updateShoppingSession] "  + shoppingSession.Id + "," + actionPeformerId + ", '" + referenceId + "'; ";
                var cmd = _context.Database.ExecuteSqlRaw(query);

                shoppingSession.User = user;
                _context.Update(shoppingSession);
                return Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }


        }
    }
}
