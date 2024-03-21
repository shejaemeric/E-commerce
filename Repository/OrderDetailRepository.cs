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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly DataContext _context;
        public OrderDetailRepository (DataContext context)
        {
            _context = context;
        }

        public bool CheckIfOrderDetailExist(int orderDetailId)
        {
            return _context.OrderDetails.Any(od => od.Id ==orderDetailId);
        }

        public bool CreateOrderDetail(int paymentDetailId,int userId, OrderDetail orderDetail)
        {
            var paymentDetail = _context.PaymentDetails.Find(paymentDetailId);

            if ( paymentDetail == null) {
                return false;
            }

            var user = _context.Users.Find(userId);

            if ( user == null) {
                return false;
            }
            orderDetail.PaymentDetails = paymentDetail;
            orderDetail.User = user;
            _context.Add(orderDetail);
            return Save();
        }

        public ICollection<OrderDetail> GetAllOrderByUser(int userId)
        {
            return _context.OrderDetails.Where(od => od.User.Id == userId).Include(p=>p.PaymentDetails).ToList();
        }


        public ICollection<OrderDetail> GetAllOrders()
        {
            return _context.OrderDetails.OrderBy(c => c.Id).Include(p=>p.PaymentDetails).ToList();
        }

        public OrderDetail GetOneOrderDetail(int orderId)
        {
            return _context.OrderDetails.Where(c => c.Id == orderId).Include(p=>p.PaymentDetails).FirstOrDefault();
        }
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateOrderDetail( int userId, int paymentDetailId, OrderDetail orderDetail)
        {
                        var paymentDetail = _context.PaymentDetails.Find(paymentDetailId);

            if ( paymentDetail == null) {
                return false;
            }

            var user = _context.Users.Find(userId);

            if ( user == null) {
                return false;
            }
            orderDetail.PaymentDetails = paymentDetail;
            orderDetail.User = user;
            _context.Update(orderDetail);
            return Save();
        }
    }
}
