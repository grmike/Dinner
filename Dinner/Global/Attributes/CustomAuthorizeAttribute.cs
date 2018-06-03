using Dinner.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Dinner.Global.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private AuthService Service
        {
            get
            {
                return DependencyResolver.Current.GetService<AuthService>();
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request.Cookies["DinnerProAuth"] != null)
            {
                var value = httpContext.Request.Cookies["DinnerProAuth"].Value;
                var user = Service.GetUser(value);
                if (user.HasValue) {
                    httpContext.Items.Add("UserId", user.Value);
                    return true;
                }
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            try
            {
                int userId = (int)filterContext.HttpContext.Items["UserId"];
                base.HandleUnauthorizedRequest(filterContext);
            }
            catch
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Login" }));
            }
        }
    }
}