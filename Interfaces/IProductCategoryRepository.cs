using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Interfaces
{
    public interface IProductCategoryRepository
    {

        ICollection<ProductCategory> GetAllProductCategories();

        ProductCategory GetOneProductCategory(int productId);

        bool CheckIfProductCategoryExist(int productCategoryId);

        bool CreateProductCategory(ProductCategory productCategory);

        bool UpdateProductCategory(ProductCategory productCategory,int actionPeformerId, string referenceId);
        bool DeleteProductCategory(int productCategoryId, int actionPeformerId, string referenceId);
        bool Save();
    }
}
