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
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _environment;
        public ProductRepository(DataContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _environment = webHostEnvironment;
        }

        public bool CheckIfProductExist(int productId)
        {
            return _context.Products.Any(u => u.Id ==productId);
        }

        public bool CreateProduct(int inventoryId, int productCategoryId, Product product,int? discountId = null)
        {


            var inventory = _context.Inventories.Find(inventoryId);
            if (inventory == null) {
                return false;
            }
            var productCategory = _context.ProductCategories.Find(productCategoryId);
            if (productCategory == null) {
                return false;
            }

            if (discountId != null) {
                var discount = _context.Discounts.Find(discountId);
                 product.Discount= discount;
            }


            product.Inventory = inventory;
            product.Is_deleted = false;
            product.Deleted_At = DateTime.Now;

            product.ProductCategory = productCategory;
            _context.Add(product);
            return Save();
        }

        public bool DeleteProduct(int productId, int actionPeformerId, string referenceId)
        {
            try {
                string query = " Exec  [dbo].[proc_deleteProduct] "  + productId + "," + actionPeformerId + ", '" + referenceId + "'; ";
                var cmd = _context.Database.ExecuteSqlRaw(query);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public ICollection<Product> GetAllProducts()
        {
            return _context.Products.Where(p=>p.Is_deleted == false).OrderBy(p => p.Id).ToList();
        }


        public Product GetOneProduct(int productId)
        {
            return _context.Products.FirstOrDefault(p => p.Id == productId);
        }

        public ICollection<Product> GetProductsByCategory(int categoryId)
        {
            return _context.Products.Where(p => p.ProductCategory.Id == categoryId).ToList();
        }

        public ICollection<Product> GetProductsByDiscount(int discountId)
        {
            return _context.Products.Where(p => p.Discount.Id == discountId).ToList();
        }

        public ICollection<Product> GetProductsOnDiscount()
        {
            return _context.Products.Where(p => p.Discount != null).ToList();
        }
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateProduct( int discountId, int inventoryId, int productCategoryId, Product product,int actionPeformerId, string referenceId)
        {
            var discount = _context.Discounts.Find(discountId);
            if (discount == null) {
                return false;
            }
            var inventory = _context.Inventories.Find(inventoryId);
            if (inventory == null) {
                return false;
            }
            var productCategory = _context.ProductCategories.Find(productCategoryId);
            if (productCategory == null) {
                return false;
            }

            try  {
                string query = " Exec  [dbo].[proc_updateProduct] "  + productCategory.Id + "," + actionPeformerId + ", '" + referenceId + "'; ";
                var cmd = _context.Database.ExecuteSqlRaw(query);

                product.Discount= discount;
                product.Inventory = inventory;
                product.ProductCategory = productCategory;
                _context.Update(product);
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
