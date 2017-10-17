using System;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace Tangente.DAL.Product {
    public class Products {

        public string ConnectionString = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "../DAL/AccessDb.txt");

        #region ProductsSelectAll
        public SqlDataReader ProductsSelectAll() {
            var response = SqlHelper.ExecuteReader(ConnectionString, "ProductsSelectAll");
            return response;
        }
        #endregion

        #region ProductsSelectRow
        public SqlDataReader ProductsSelectRow(int productId) {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@ProductId", SqlDbType.Int) {Value = productId};

            var response = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "ProductsSelectRow", param);
            return response;
        }
        #endregion

        #region ProductsInsertRow
        public int ProductsInsertRow(string productName, int categoryId) {
            if (productName == null) {
                return 0;
            } else {
                var response = SqlHelper.ExecuteNonQuery(ConnectionString, "ProductsInsertRow", productName, categoryId);
                return response;
            }
        }
        #endregion

        #region ProductsUpdateRow
        public int ProductsUpdateRow(int productId, string productName, int categoryId) {
            var response = SqlHelper.ExecuteNonQuery(ConnectionString, "ProductsUpdateRow", productId, productName, categoryId);
            return response;
        }
        #endregion

        #region ProductsPatchRow
        public int ProductsPatchRow(int productId, string productName, int categoryId) {
            var response = SqlHelper.ExecuteNonQuery(ConnectionString, "ProductsPatchRow", productId, productName, categoryId);
            return response;
        }
        #endregion

        #region ProductsDeleteRow
        public int ProductsDeleteRow(int productId) {
            var response = SqlHelper.ExecuteNonQuery(ConnectionString, "ProductsDeleteRow", productId);
            return response;
        }
        #endregion
    }
}
