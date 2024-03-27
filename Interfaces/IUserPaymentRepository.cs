using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Interfaces
{
    public interface IUserPaymentRepository
    {

        UserPayment GetOneUserPayment(int userPaymentId);
        bool CheckIfUserPaymentExist(int userPaymentId);
        bool CreateUserPayment(int userId,UserPayment userPayment);
        bool UpdateUserPayment(int userId,UserPayment userPayment,int actionPeformerId, string referenceId);
        public ICollection<UserPayment> GetAllUserPaymentByUser(int userId);

        bool DeleteUserPayment(int paymentId, int actionPeformerId, string referenceId);
        bool Save();
    }
}
