using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tangente.BLL.CategoryProduct;

namespace Tangente.API.CategoryProduct {
    public class CategoryProductsController : ApiController {
        public HttpResponseMessage Get() {
            try {
                List<CategoryProducts> categoryProductsList = new List<CategoryProducts>();
                CategoryProducts select = new CategoryProducts();
                categoryProductsList = select.CategoryProductsSelectAll();
                if (categoryProductsList.Count != 0) {
                    return Request.CreateResponse(HttpStatusCode.OK, categoryProductsList);
                } else {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "There is no products in the server");
                }
            } catch (SqlException ex) {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            } catch (Exception e) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        public HttpResponseMessage Get(int id) {
            try {
                CategoryProducts categoryProduct = new CategoryProducts();

                var response = categoryProduct.CategoryProductsSelectRow(id);

                if (response.categoryId != 0) {
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                } else {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The product with the id = " + id.ToString() + " is not found");
                }
            } catch (SqlException ex) {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            } catch (Exception e) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        public HttpResponseMessage Post([FromBody] CategoryProducts category) {
            try {
                CategoryProducts categoryProduct = new CategoryProducts();
                var response = categoryProduct.CategoryProductsInsertRow(category.categoryId, category.categoryName );
                if (response == 0) {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Impossible to insert new row, check out your arguments");
                } else {
                    var message = Request.CreateResponse(HttpStatusCode.Created, category);
                    message.Headers.Location = new Uri(Request.RequestUri + categoryProduct.categoryId.ToString());
                    return message;
                }
            } catch (SqlException ex) {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            } catch (Exception e) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]CategoryProducts category) {

            try {
                CategoryProducts categoryProduct = new CategoryProducts();
                var response = categoryProduct.CategoryProductsUpdateRow(id, category.categoryName);

                if (response == 0) {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The product with the id " + id.ToString() + " is not found to be updated");

                } else {
                    return Request.CreateResponse(HttpStatusCode.OK, category);
                }
            } catch (SqlException ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            } catch (Exception e) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        public HttpResponseMessage Delete(int id) {
            try {
                CategoryProducts categoryProduct = new CategoryProducts();
                var response = categoryProduct.CategoryProductsDeleteRow(id);
                if (response == 0) {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Impossible to soft delete this row, this row doesn't exist in soft delete !");
                } else {
                    var message = Request.CreateResponse(HttpStatusCode.OK, "Product soft deleted");
                    return message;
                }
            } catch (SqlException ex) {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            } catch (Exception e) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpPatch]
        public HttpResponseMessage Patch(int id, [FromBody]CategoryProducts category) {
            try {
                CategoryProducts categoryProduct = new CategoryProducts();
                var response = categoryProduct.CategoryProductsPatchRow(id, category.categoryName);
                if (response == 0) {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The product with the id " + id.ToString() + " is not found to be updated");

                } else {
                    return Request.CreateResponse(HttpStatusCode.OK, category);
                }
            } catch (SqlException ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            } catch (Exception e) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }


    }
}

