using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Interfaces
{
    public interface IOrderDetailRepository
    {

        ICollection<OrderDetail> GetAllOrderByUser(int userId);
        OrderDetail GetOneOrderDetail(int orderId);

        ICollection<OrderDetail> GetAllOrders();
        bool CheckIfOrderDetailExist(int orderDetailId);

        bool CreateOrderDetail(int userId,int paymentDetailId,OrderDetail orderDetail);

        bool UpdateOrderDetail(int userId,int paymentDetailId,OrderDetail orderDetail);

        string DeleteOrderDetail(int orderDetailId, int actionPeformerId, string referenceId);
        bool Save();
    }
}
