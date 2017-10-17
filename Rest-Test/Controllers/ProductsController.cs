using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using Tangente.BLL.Product;
using Arebis.Logging.GrayLog;
using System.Net.Sockets;
using System.Text;

namespace Tangente.API.Product {
    public class ProductsController : ApiController {

        public HttpResponseMessage Get() {
            try {
                
                var client = new UdpClient("192.168.0.141", 15678);

                using (var logger = new GrayLogUdpClient("GREF", client))
                {
                    logger.Send("Hello World !", "THE FULL MESSAGE", new { SessionId = "781227", Device = "Iphone" , DeviceDate = ""});
                }

                var productList = new List<Products>();
                var select = new Products();
                productList = select.ProductsSelectAll();

                if (productList.Count != 0) {

                     var response =  Request.CreateResponse(HttpStatusCode.OK, productList);
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    return response;
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
                Products product = new Products();

                var response = product.ProductsSelectRow(id);

                if (response.productId != 0) {
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

        public HttpResponseMessage Post([FromBody] Products product) {
            try {
                Products lProducts = new Products();
                var response = lProducts.ProductsInsertRow(product.productName, product.categoryId);
                if (response == 0) {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Impossible to insert new row, check out your arguments");

                } else {
                    var message = Request.CreateResponse(HttpStatusCode.Created, product);
                    message.Headers.Location = new Uri(Request.RequestUri + lProducts.productId.ToString());
                    return message;
                }
            } catch (SqlException ex) {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            } catch (Exception e) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }


        public HttpResponseMessage Put(int id, [FromBody]Products product) {

            try {
                Products lProduct = new Products();
                var response = lProduct.ProductsUpdateRow(id, product.productName, product.categoryId);

                if (response == 0) {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The product with the id " + id.ToString() + " is not found to be updated");

                } else {
                    return Request.CreateResponse(HttpStatusCode.OK, product);
                }
            }catch (SqlException ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            } 
            catch (Exception e) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }

        }

        public HttpResponseMessage Delete(int id) {
            try {
                Products products = new Products();
                var response = products.ProductsDeleteRow(id);
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


        // Be careful with the int, foreign key with patch method
        [HttpPatch]
        public HttpResponseMessage Patch (int id, [FromBody]Products product) {
            try {
                var lProduct = new Products();
                var response = lProduct.ProductsPatchRow(id, product.productName, product.categoryId);
                if (response == 0) {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The product with the id " + id + " is not found to be updated");

                } else {
                    return Request.CreateResponse(HttpStatusCode.OK, product);
                }
            } catch (SqlException ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            } catch (Exception e) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }


        /***
         * 
         * ________________________
         * | AVEC FRAMEWORK ENTITY|
         * ¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
         * 
         * */

        //public HttpResponseMessage Delete(int Id) {
        //    try {
        //        using (TestEntities entities = new TestEntities()) {
        //            var entity = entities.Products.FirstOrDefault(e => e.ProductID == Id);
        //            if (entity == null) {
        //                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The product with the id " + Id.ToString() + " is not found to be deleted");
        //            } else {
        //                entities.Products.Remove(entity);
        //                entities.SaveChanges();
        //                return Request.CreateResponse(HttpStatusCode.OK);
        //            }
        //        }
        //    } catch (Exception e) {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
        //    }
        //}


        //public IEnumerable<Product> Get() {
        //    using (TestEntities entities = new TestEntities()) {
        //        return entities.Products.ToList();
        //    }
        //}

        //public HttpResponseMessage Get(int Id) {
        //    using (TestEntities entities = new TestEntities()) {
        //        var entity = entities.Products.FirstOrDefault(e => e.ProductID == Id);

        //        if (entity != null) {
        //            return Request.CreateResponse(HttpStatusCode.OK, entity);
        //        } else {
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The product with the id = " + Id.ToString() + " is not found");
        //        }
        //    }
        //}

        //public HttpResponseMessage Post([FromBody] Product product) {
        //    try {
        //        using (TestEntities entities = new TestEntities()) {
        //            entities.Products.Add(product);
        //            entities.SaveChanges();
        //            var message = Request.CreateResponse(HttpStatusCode.Created, product);
        //            message.Headers.Location = new Uri(Request.RequestUri + product.ProductID.ToString());
        //            return message;
        //        }
        //    } catch (Exception e) {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
        //    }

        //}

        //public HttpResponseMessage Delete(int Id) {
        //    try {
        //        using (TestEntities entities = new TestEntities()) {
        //            var entity = entities.Products.FirstOrDefault(e => e.ProductID == Id);
        //            if (entity == null) {
        //                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The product with the id " + Id.ToString() + " is not found to be deleted");
        //            } else {
        //                entities.Products.Remove(entity);
        //                entities.SaveChanges();
        //                return Request.CreateResponse(HttpStatusCode.OK);
        //            }
        //        }
        //    } catch (Exception e) {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
        //    }
        //}

        //public HttpResponseMessage Put(int Id, [FromBody]Product produit) {
        //    using (TestEntities entities = new TestEntities()) {
        //        try {
        //            var entity = entities.Products.FirstOrDefault(e => e.ProductID == Id);

        //            if (entity == null) {
        //                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The product with the id " + Id.ToString() + " is not found to be updated");

        //            } else {
        //                //entity.ProductID = produit.ProductID;
        //                entity.ProductName = produit.ProductName;

        //                entities.SaveChanges();

        //                return Request.CreateResponse(HttpStatusCode.OK, entity);
        //            }
        //        } catch (Exception e) {
        //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
        //        }
        //    }
        //}
    }
}
