using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetAllProducts();
        ICollection<Product> GetProductsByCategory(int categoryId);

        ICollection<Product> GetProductsOnDiscount();

        ICollection<Product> GetProductsByDiscount(int discountId);

        Product GetOneProduct(int productId);

        bool CheckIfProductExist(int productId);
        bool CreateProduct(int discountId,int inventoryId,int productCategoryId,Product product);

         bool UpdateProduct(int discountId,int inventoryId,int productCategoryId,Product product,int actionPeformerId, string referenceId);

         bool DeleteProduct(int productId, int actionPeformerId, string referenceId);
        bool Save();
    }
}
