using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Interfaces
{
    public interface IDiscountRepository
    {
        ICollection<Discount> GetAllDiscounts();
        Discount GetOneDiscount(int discountId);
        bool CheckIfDiscountExist(int? discountId);
        bool CreateDiscount(Discount discount);

        bool UpdateDiscount(Discount discount,int actionPeformerId, string referenceId);

        bool DeleteDiscount(int discountId, int actionPeformerId, string referenceId);
        bool Save();
    }
}
