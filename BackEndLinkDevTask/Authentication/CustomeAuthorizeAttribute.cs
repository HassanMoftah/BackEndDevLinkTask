using BackEndLinkDevTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace BackEndLinkDevTask.Authentication
{
    public class CustomeAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {

            string token = "";
            try
            {
                token = actionContext.Request.Headers.GetValues("Authorization").First().ToString();

                var claimsPrincipal = TokenManager.IsValidUser(token);

                if (claimsPrincipal == null)
                {
                    actionContext.Response = actionContext.Request
                        .CreateResponse(HttpStatusCode.Unauthorized);
                }
                else
                {
                    var claims = claimsPrincipal.Claims.Where(x => x.Type == "UserId").Select(x => x.Value).ToList();
                    int userId = Convert.ToInt32( claims[0]);
                    MDUser user = MDUser.GetById(userId);
                    if ((user.IsAdmin && Roles != "Admin")||(!user.IsAdmin&&Roles!="Customer"))
                    {
                        actionContext.Response = actionContext.Request
                       .CreateResponse(HttpStatusCode.Forbidden);
                    }

                }

            }

            catch
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized);

            }
        }
    }
}