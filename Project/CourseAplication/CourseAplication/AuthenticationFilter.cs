using System;
using System.Net;
using System.Security.Principal;
using System.Text;
using CourseAplication.Model;
using CourseAplication.Views;
using PI.WebGarten;
using PI.WebGarten.HttpContent.Html;
using PI.WebGarten.Pipeline;

namespace CourseAplication
{
    public class AuthenticationFilter : IHttpFilter
    {
        private readonly string _name;

        private IHttpFilter _nextFilter;

        public AuthenticationFilter(string name)
        {
            this._name = name;
        }

        #region Implementation of IHttpFilter

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public void SetNextFilter(IHttpFilter nextFilter)
        {
            _nextFilter = nextFilter;
        }

        public HttpResponse Process(RequestInfo requestInfo)
        {
            var ctx = requestInfo.Context;
            if (ctx.Request.Url.AbsolutePath.Contains("/logout"))
            {
                string auth = ctx.Request.Headers["Authorization"];
                if (auth != null)
                {
                    requestInfo.User = null;
                    var resp = new HttpResponse(401, new TextContent("Logged out"));
                    return resp;
                }
               
            }
            if (ctx.Request.Url.AbsolutePath.Contains("newfuc") || ctx.Request.Url.AbsolutePath.Contains("prop") ||
                ctx.Request.Url.AbsolutePath.Contains("create") || ctx.Request.Url.AbsolutePath.Contains("edit") ||
                ctx.Request.Url.AbsolutePath.Contains("login") )
            {
                string auth = ctx.Request.Headers["Authorization"];
                if (auth == null)
                {
                    
                    var resp = new HttpResponse(401, new TextContent("Not Authorized"));

                    resp.WithHeader("WWW-Authenticate", "Basic realm=\"Private Area\"");
                    return resp;

                }
                auth = auth.Replace("Basic ", "");
                string userPassDecoded = Encoding.UTF8.GetString(Convert.FromBase64String(auth));
                string[] userPasswd = userPassDecoded.Split(':');
                string user = userPasswd[0];
                string passwd = userPasswd[1];

                 User u= RepositoryLocator.GetUserRep().GetById(user);
                 if (u == null || !u.Match(passwd)) return new HttpResponse(401, new TextContent("Not Authenticaded")).WithHeader("WWW-Authenticate", "Basic realm=\"Private Area\"");
                
                requestInfo.User = new GenericPrincipal(new GenericIdentity(user), null);

                Console.WriteLine("Authentication: {0} - {1}", auth, userPassDecoded);
            }

            return _nextFilter.Process(requestInfo);
        }

        #endregion
    }
}