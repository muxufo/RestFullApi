using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Tangente.DAL.CategoryProduct {
    public class CategoryProducts {
        public string ConnectionString = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "../DAL/AccessDb.txt");

        #region CategoryProductsSelectAll
        public SqlDataReader CategoryProductsSelectAll() {
            var response = SqlHelper.ExecuteReader(ConnectionString, "CategoryProductsSelectAll");
            return response;
        }
        #endregion

        #region CategoryProductsSelectRow
        public SqlDataReader CategoryProductsSelectRow(int categoryId) {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@CategoryId", SqlDbType.Int);
            param[0].Value = categoryId;

            var response = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, "CategoryProductsSelectRow", param);
            return response;
        }
        #endregion

        #region CategoryProductsInsertRow
        public int CategoryProductsInsertRow(string categoryName) {
            if (categoryName == null ) {
                return 0;
            } else {
                var response = SqlHelper.ExecuteNonQuery(ConnectionString, "CategoryProductsInsertRow", categoryName);
                return response;
            }
        }
        #endregion

        #region CategoryProductsPatchRow
        public int CategoryProductsPatchRow(int categoryId, string categoryName) {
            var response = SqlHelper.ExecuteNonQuery(ConnectionString, "CategoryProductsPatchRow", categoryId, categoryName);
            return response;
        }
        #endregion

        #region CategoryProductsUpdateRow
        public int CategoryProductsUpdateRow(int categoryId, string categoryName) {
            var response = SqlHelper.ExecuteNonQuery(ConnectionString, "CategoryProductsUpdateRow", categoryId, categoryName);
            return response;
        }
        #endregion

        #region CategoryProductsDeleteRow
        public int CategoryProductsDeleteRow(int categoryId) {
            var response = SqlHelper.ExecuteNonQuery(ConnectionString, "CategoryProductsDeleteRow", categoryId);
            return response;
        }
        #endregion
    }
}
