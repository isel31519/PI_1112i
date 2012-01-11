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
            context.AuthenticateRequest += AuthenticateRequest;
            context.EndRequest += EndRequest;

        }
        private void AuthenticateRequest(Object source, EventArgs e)
        {
            HttpApplication httpApplication = (HttpApplication)source;
            HttpContext context = httpApplication.Context;
            if (FormsAuthentication.CookiesSupported)
            {
                if (context.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    HttpCookie cookie;
                    if(( cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName])!=null)
                    {
                        FormsAuthenticationTicket ticket=FormsAuthentication.Decrypt(cookie.Value);
                        if (ticket != null && !ticket.Expired)
                        {
                            context.User = new GenericPrincipal(new GenericIdentity(ticket.Name), null);
                        }
                    }
                }
                else
                 context.User = new GenericPrincipal(new GenericIdentity(""), null);
            }
        }

        private void EndRequest(Object source, EventArgs e)
        {
            HttpApplication httpApplication = (HttpApplication)source;
            HttpContext context = httpApplication.Context;
            if (!context.Response.StatusCode.Equals(401)) return;
            context.Response.StatusCode = 301;
            context.Response.RedirectLocation = "/Account/LogOn";
        }
        public void Dispose()
        {
        }
    }
}