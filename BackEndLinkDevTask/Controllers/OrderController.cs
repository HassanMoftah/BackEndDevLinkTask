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
    public class OrderController : ApiController
    {
        [HttpPost]
        [CustomeAuthorize(Roles = "Customer")]
        public IHttpActionResult GetPriceOfOrder(MDOrder order)
        {
            float totalprice= MDOrder.CalcTotalPrice(order);
            return Ok(totalprice);
        }


        [HttpPost]
        [CustomeAuthorize(Roles ="Customer")]
        public IHttpActionResult Add(MDOrder order)
        {
           MDOrder orderadded= MDOrder.Add(order);
            if (orderadded == null) { return BadRequest(); }
            return Ok(orderadded);
        }

        [HttpGet]
        [CustomeAuthorize(Roles ="Admin")]
        public IHttpActionResult GetAll()
        {
            List<MDOrder> orders = MDOrder.GetAll();
            return Ok(orders);

        }

    }
}
