using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Interfaces
{
    public interface IPasswordResetTokenRepository
    {

        bool Save();

        bool CreatePasswordResetToken(PasswordResetToken passwordResetToken);

        PasswordResetToken GetOnePasswordResetToken(int passwordResetTokenId);

        bool CheckIfPasswordResetTokenExist(int passwordResetTokenId);


        bool UpdatePasswordResetToken(PasswordResetToken passwordResetToken);

        public ICollection<PasswordResetToken> GetUnexpiredPasswordResetTokensByUser(int userId);

    }
}
