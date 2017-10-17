using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangente.BLL.CategoryProduct {
    public class CategoryProducts {
        public int categoryId { get; set; }
        public string categoryName { get; set; }

        #region CategoryProductsSelectAll
        public List<CategoryProducts> CategoryProductsSelectAll() {
            var select = new DAL.CategoryProduct.CategoryProducts();
            var categoryProducts = new List<CategoryProducts>();
            DbDataReader dr = select.CategoryProductsSelectAll();
            while (dr.Read()) {
                var myCategoryProducts = new CategoryProducts {
                    categoryId = (int)dr["CategoryId"],
                    categoryName = (string)dr["CategoryName"]
                };
                categoryProducts.Add(myCategoryProducts);
            }
            return categoryProducts;
        }
        #endregion

        #region CategoryProductsSelectRow
        public CategoryProducts CategoryProductsSelectRow(int categoryId) {
            var select = new DAL.CategoryProduct.CategoryProducts();
            DbDataReader dr = select.CategoryProductsSelectRow(categoryId);
            var categoryProduct = new CategoryProducts();

            if (dr.Read()) {
                categoryProduct.categoryId = (int)dr["CategoryId"];
                categoryProduct.categoryName = (string)dr["CategoryName"];
            }
            return categoryProduct;
        }
        #endregion

        #region CategoryProductsInsertRow
        public int CategoryProductsInsertRow(int categoryId,string categoryName) {
            var insert = new DAL.CategoryProduct.CategoryProducts();
            return insert.CategoryProductsInsertRow(categoryName);
        }
        #endregion

        #region CategoryProductsUpdateRow
        public int CategoryProductsUpdateRow(int categoryId, string categoryName) {
            var update = new DAL.CategoryProduct.CategoryProducts();
            return update.CategoryProductsUpdateRow(categoryId, categoryName);
        }
        #endregion

        #region CategoryProductsDeleteRow
        public int CategoryProductsDeleteRow(int productId) {
            var delete = new DAL.CategoryProduct.CategoryProducts();
            return delete.CategoryProductsDeleteRow(productId);
        }
        #endregion

        #region CategoryProductsPatchRow
        public int CategoryProductsPatchRow(int categoryId, string categoryName) {
            var patch = new DAL.CategoryProduct.CategoryProducts();
            return patch.CategoryProductsPatchRow(categoryId, categoryName);
        }
        #endregion
    }
}
