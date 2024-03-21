using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;
using E_Commerce_Api.Data;
using E_Commerce_Api.Interfaces;

namespace E_Commerce_Api.Repository
{
    public class PaymentDetailsRepository : IPaymentDetailsRepository
    {
        private readonly DataContext _context;
        public PaymentDetailsRepository(DataContext context)
        {
            _context = context;
        }

        public bool CheckIfPaymentDetailExist(int paymentDetailId)
        {
            return _context.PaymentDetails.Any(u => u.Id ==paymentDetailId);
        }

        public bool CreatePaymentDetails(PaymentDetail paymentDetail)
        {
            _context.Add(paymentDetail);
            return Save();
        }

        public ICollection<PaymentDetail> GetAllPaymentDetails()
        {
            return _context.PaymentDetails.OrderBy(u => u.Id).ToList();
        }

        public PaymentDetail GetOnePaymentDetails(int paymentId)
        {
            return _context.PaymentDetails.FirstOrDefault(u => u.Id == paymentId);
        }
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdatePaymentDetails( PaymentDetail paymentDetail)
        {
            _context.Update(paymentDetail);
            return Save();
        }
    }
}
