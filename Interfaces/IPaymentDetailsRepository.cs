using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Interfaces{
    public interface IPaymentDetailsRepository
    {
        ICollection<PaymentDetail> GetAllPaymentDetails();
        PaymentDetail GetOnePaymentDetails(int paymentId);
        bool CheckIfPaymentDetailExist(int paymentDetailId);

        bool CreatePaymentDetails(PaymentDetail paymentDetail);

        bool UpdatePaymentDetails(PaymentDetail paymentDetail,int actionPeformerId, string referenceId);
        bool Save();
    }
}
