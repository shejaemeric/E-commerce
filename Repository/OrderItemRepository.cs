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
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly DataContext _context;
        public OrderItemRepository (DataContext context)
        {
            _context = context;
        }

        public bool CheckIfOrderItemExist(int orderItemId)
        {
            return _context.OrderItems.Any(oi => oi.Id ==orderItemId);
        }

        public ICollection<OrderItem> GetAllOrderItem()
        {
            return _context.OrderItems.OrderBy(c => c.Id).Include(p => p.Product).ToList();
        }

        public OrderItem GetOneOrderItem(int OrderItemtId)
        {
            return _context.OrderItems.Where(oi => oi.Id == OrderItemtId).Include(p => p.Product).FirstOrDefault();
        }

        public ICollection<OrderItem> GetAllOrderItemsByOrder(int orderDetailId)
        {
            return _context.OrderItems.Where(od => od.OrderDetails.Id == orderDetailId).Include(p => p.Product).ToList();
        }
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool CreateOrderItem(int productId, int orderDetailId, OrderItem orderItem)
        {
            var product = _context.Products.Find(productId);
            if ( product == null) {
                return false;
            }

            var orderDetail = _context.OrderDetails.Find(orderDetailId);
            if (orderDetail == null) {
                return false;
            }

            orderItem.Product = product;
            orderItem.OrderDetails = orderDetail;
            _context.Add(orderItem);
            return Save();
        }

        public bool UpdateOrderItem( int productId, int orderDetailId, OrderItem orderItem)
        {
            var product = _context.Products.Find(productId);
            if ( product == null) {
                return false;
            }

            var orderDetail = _context.OrderDetails.Find(orderDetailId);
            if (orderDetail == null) {
                return false;
            }

            orderItem.Product = product;
            orderItem.OrderDetails = orderDetail;
            _context.Update(orderItem);
            return Save();
        }
    }

}
