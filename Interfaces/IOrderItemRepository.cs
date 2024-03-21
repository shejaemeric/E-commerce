using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Interfaces
{
    public interface IOrderItemRepository
    {
        ICollection<OrderItem> GetAllOrderItem();
        OrderItem GetOneOrderItem(int OrderItemtId);
        bool CheckIfOrderItemExist(int orderItemId);

        ICollection<OrderItem> GetAllOrderItemsByOrder(int orderDetailId);

        bool CreateOrderItem(int productId,int orderDetailId,OrderItem orderItem);

         bool UpdateOrderItem(int productId,int orderDetailId,OrderItem orderItem);
        bool Save();
    }
}
