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
        public  ProductRepository(DataContext context)
        {
            _context = context;
        }

        public bool CheckIfProductExist(int productId)
        {
            return _context.Products.Any(u => u.Id ==productId);
        }

        public bool CreateProduct(int discountId, int inventoryId, int productCategoryId, Product product)
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

            product.Discount= discount;
            product.Inventory = inventory;
            product.ProductCategory = productCategory;
            _context.Add(product);
            return Save();
        }

        public ICollection<Product> GetAllProducts()
        {
            return _context.Products.OrderBy(p => p.Id).ToList();
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

        public bool UpdateProduct( int discountId, int inventoryId, int productCategoryId, Product product)
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

            product.Discount= discount;
            product.Inventory = inventory;
            product.ProductCategory = productCategory;
            _context.Update(product);
            return Save();
        }
    }
}
