using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;

namespace CourseAplicationMVC.Filters

{ 
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthenticationFilter: ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string username;
            if(!(username=filterContext.HttpContext.User.Identity.Name).Equals(""))
            {
                MembershipUser user = Membership.GetUser(username);
                if (user == null)
                {
                     filterContext.HttpContext.Response.StatusCode = 401;
                    return;
                }
            }
            else
            {
                filterContext.HttpContext.Response.StatusCode = 401;
                return;
            }
             base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {/*falta*/
            
             if (filterContext.Exception != null)
             filterContext.HttpContext.Trace.Write("(Logging Filter)Exception thrown");

             base.OnActionExecuted(filterContext);
        }
    }
}