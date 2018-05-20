using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dinner.Global.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
    }
}