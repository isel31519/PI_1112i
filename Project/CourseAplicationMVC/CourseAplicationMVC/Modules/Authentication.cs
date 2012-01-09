using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace CourseAplicationMVC.Modules
{
    public class Authentication : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += (new EventHandler(this.AuthenticateRequest));
            context.EndRequest += (new EventHandler(this.EndRequest));

        }
        private void AuthenticateRequest(Object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            if (FormsAuthentication.CookiesSupported)
            {
                if (context.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    FormsAuthenticationTicket ticket =
                        FormsAuthentication.Decrypt(context.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                    if (ticket != null && !ticket.Expired)
                    {
                        context.User = new GenericPrincipal(new GenericIdentity(ticket.Name), null);
                    }
                }
                else
                 context.User = new GenericPrincipal(new GenericIdentity(""), null);
            }
        }

        private void EndRequest(Object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
           /* if (context.Response.StatusCode.Equals(401))
            {
                context.Response.Write("<hr><h1><font color=red>HelloWorldModule: End of Request</font></h1>");
                context.Response.StatusCode = 301;
                context.Response.Redirect("~/");


            }*/
        }
        public void Dispose()
        {
            //clean-up code here.
        }
    }
}