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
    public class ProductVsDiscountController : ApiController
    {
        [HttpPost]
        [CustomeAuthorize(Roles = "Admin")]
        public IHttpActionResult Add(MDProductVsDiscountRule productVsDiscount)
        {
           MDProductVsDiscountRule productVsDiscountRule= MDProductVsDiscountRule.Add(productVsDiscount);
            if (productVsDiscountRule == null) { return BadRequest(); }
            return Ok(productVsDiscountRule);
        }

        [HttpPost]
        [CustomeAuthorize(Roles = "Admin")]
        public IHttpActionResult Delete(MDProductVsDiscountRule productVsDiscountRule)
        {
            bool flag = MDProductVsDiscountRule.Delete(productVsDiscountRule);
            if (!flag) { return BadRequest(); }
            return Ok();
        }
    }
}
