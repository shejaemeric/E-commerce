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

        public bool DeleteProductCategory(int productCategoryId, int actionPeformerId, string referenceId)
        {
            try {
            string query = " Exec  [dbo].[proc_deleteProductCategory] "  + productCategoryId + "," + actionPeformerId + ", '" + referenceId + "'; ";
                var cmd = _context.Database.ExecuteSqlRaw(query);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
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

        public bool UpdateProductCategory( ProductCategory productCategory,int actionPeformerId, string referenceId)
        {
            try  {
                string query = " Exec  [dbo].[proc_updateProductCategory] "  + productCategory.Id + "," + actionPeformerId + ", '" + referenceId + "'; ";
                var cmd = _context.Database.ExecuteSqlRaw(query);
                _context.Update(productCategory);
                return Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
    }
}
