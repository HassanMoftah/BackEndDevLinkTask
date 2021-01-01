using BackEndLinkDevTask.Authentication;
using BackEndLinkDevTask.Models;
using BackEndLinkDevTask.ViewModels;
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
    public class UserController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Register(MDUser user)
        {
            MDUser useradded= MDUser.AddCustomerUser(user);
            if (useradded == null) { return BadRequest("Invalid Data"); }
            return Ok();
        }
        [HttpPost]
        public IHttpActionResult Login(VMSignIn signIn)
        {
           int userid= MDUser.Login(signIn.Email, signIn.Password);
            if (userid == 0) { return Unauthorized(); }
            else
            {
                string token = TokenManager.GenerateToken(userid);
                MDUser user = MDUser.GetById(userid);
                user.Token = token;
                user.Password = "";
                return Ok(user);
            }
        }
    }
}
