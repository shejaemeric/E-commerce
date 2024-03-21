using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;
using E_Commerce_Api.Data;
using E_Commerce_Api.Interfaces;

namespace E_Commerce_Api.Repository
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly DataContext _context;
        public  ProductCategoryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CheckIfProductCategoryExist(int productCategoryId)
        {
            return _context.ProductCategories.Any(pc => pc.Id ==productCategoryId);
        }

        public bool CreateProductCategory(ProductCategory productCategory)
        {
            _context.Add(productCategory);
            return Save();
        }

        public ICollection<ProductCategory> GetAllProductCategories()
        {
            return _context.ProductCategories.OrderBy(pc => pc.Id).ToList();
        }

        public ProductCategory GetOneProductCategory(int productId)
        {
            return _context.ProductCategories.FirstOrDefault(pc => pc.Id == productId);
        }
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateProductCategory( ProductCategory productCategory)
        {
            _context.Update(productCategory);
            return Save();
        }
    }
}
