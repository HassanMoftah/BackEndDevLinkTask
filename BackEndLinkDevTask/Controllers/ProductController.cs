using BackEndLinkDevTask.Authentication;
using BackEndLinkDevTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BackEndLinkDevTask.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductController : ApiController
    {
        [HttpPost]
        [CustomeAuthorize(Roles ="Admin")]
        public IHttpActionResult Add(MDProduct product)
        {
            MDProduct added = MDProduct.Add(product);
            if (added == null) { return BadRequest(); }
            else return Ok(added);
        }

        [HttpGet]
        public IHttpActionResult GetProductsPaginatedByOffset(int offset)
        {
            List<MDProduct> products = MDProduct.GetProductsPaginatedByOffset(offset, 5);
            return Ok(products);
        }

        [HttpGet]
        [CustomeAuthorize(Roles = "Admin")]
        public  IHttpActionResult GetAll()
        {
            List<MDProduct> products = MDProduct.GetAll();
            return Ok(products);
        }
    }
}
