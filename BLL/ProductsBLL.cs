using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Tangente.DAL;
using System.Data.Common;
using Microsoft.Build.Framework.XamlTypes;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Tangente.BLL.Product {
    public class Products {
        public int productId { get; set; }
        public string productName { get; set; }
        public DateTime lastModified { get; set; }
        public string categoryName { get; set; }
        // [IgnoreDataMember] is used to make this field disapear from the json result
        public int categoryId { get; set; }

        #region ProductsSelectAll
        public List<Products> ProductsSelectAll() {
            DAL.Product.Products select = new DAL.Product.Products();
            List<Products> productsList = new List<Products>();
            DbDataReader dr = select.ProductsSelectAll();
            while (dr.Read()) {
                var myProducts = new Products {
                    productId = (int)dr["ProductId"],
                    productName = (string)dr["ProductName"],
                    lastModified = (DateTime)dr["LastModified"],
                    categoryName = (string)dr["CategoryName"]
                };
                productsList.Add(myProducts);
            }
            return productsList;
        }

        #endregion
        #region ProductsSelectRow
        public Products ProductsSelectRow(int productId) {
            var select = new DAL.Product.Products();
            DbDataReader dr = select.ProductsSelectRow(productId);
            var product = new Products();

            if (dr.Read()) {
                product.productId = (int)dr["ProductId"];
                product.productName = (string)dr["ProductName"];
                product.lastModified = (DateTime)dr["LastModified"];
                product.categoryName = (string)dr["CategoryName"];
            }
            return product;
        }
        #endregion
        #region ProductsInsertRow
        public int ProductsInsertRow(string productName, int categoryId) {
            DAL.Product.Products insert = new DAL.Product.Products();
            return insert.ProductsInsertRow(productName, categoryId);
        }

        #endregion
        #region ProductsUpdateRow
        public int ProductsUpdateRow(int productId, string productName, int categoryId) {
            DAL.Product.Products update = new DAL.Product.Products();
            return update.ProductsUpdateRow(productId, productName, categoryId);
        }
        #endregion

        #region ProductsDeleteRow
        public int ProductsDeleteRow(int productId) {
            DAL.Product.Products delete = new DAL.Product.Products();
            return delete.ProductsDeleteRow(productId);
        }
        #endregion

        #region ProductsPatchRow
        public int ProductsPatchRow(int productId, string productName, int categoryId) {
            DAL.Product.Products patch = new DAL.Product.Products();
            return patch.ProductsPatchRow(productId, productName, categoryId);
        }
        #endregion
    }
}
