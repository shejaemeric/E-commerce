using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Data;
using E_Commerce_Api.Interfaces;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Repository
{
    public class PasswordResetTokenRepository : IPasswordResetTokenRepository
    {

        private readonly DataContext _context;
        public  PasswordResetTokenRepository(DataContext context)
        {
            _context = context;
        }

        public bool CheckIfPasswordResetTokenExist(int passwordResetTokenId)
        {
            return _context.PasswordResetTokens.Any(pt => pt.Id == passwordResetTokenId);
        }

        public bool CreatePasswordResetToken(PasswordResetToken passwordResetToken)
        {
            _context.Add(passwordResetToken);
            return Save();
        }

        public PasswordResetToken GetOnePasswordResetToken(int passwordResetTokenId)
        {
            return _context.PasswordResetTokens.FirstOrDefault(pt => pt.Id == passwordResetTokenId);
        }

        public ICollection<PasswordResetToken> GetUnexpiredPasswordResetTokensByUser(int userId)
        {
           return _context.PasswordResetTokens.Where(pt => pt.User.Id == userId && pt.Expiration < DateTime.Now).ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdatePasswordResetToken(PasswordResetToken passwordResetToken)
        {
            _context.Update(passwordResetToken);
            return Save();

        }
    }
}
