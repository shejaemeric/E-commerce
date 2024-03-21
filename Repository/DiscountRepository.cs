using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;
using E_Commerce_Api.Data;
using E_Commerce_Api.Interfaces;

namespace E_Commerce_Api.Repository
{
    public class DiscountRepository:IDiscountRepository
    {

        private readonly DataContext _context;
        public DiscountRepository(DataContext context)
        {
            _context = context;
        }

        public bool CheckIfDiscountExist(int discountId)
        {
            return _context.Discounts.Any(d => d.Id == discountId);
        }

        ICollection<Discount> IDiscountRepository.GetAllDiscounts()
        {
            return _context.Discounts.OrderBy(u => u.Id).ToList();
        }


        Discount IDiscountRepository.GetOneDiscount(int discountId)
        {
            return _context.Discounts.FirstOrDefault(d => d.Id == discountId);
        }
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool CreateDiscount(Discount discount)
        {
            _context.Add(discount);
            return Save();
        }

        public bool UpdateDiscount( Discount discount)
        {
            _context.Update(discount);
            return Save();
        }
    }
}
