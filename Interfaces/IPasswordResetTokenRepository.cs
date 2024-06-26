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



        bool DeletePasswordResetToken(PasswordResetToken passwordResetToken);

        bool IsPasswordResetOwner(int passwordResetTokenId,int userId);

    }
}
