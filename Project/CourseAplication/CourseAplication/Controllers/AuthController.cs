using System.Collections.Generic;
using System.Linq;
using System.Net;
using CourseAplication.Model;
using CourseAplication.Views;
using PI.WebGarten;
using PI.WebGarten.HttpContent.Html;
using PI.WebGarten.MethodBasedCommands;

namespace CourseAplication.Controllers
{
    class AuthController
    {
        
        [HttpCmd(HttpMethod.Get, "/login")]
        public HttpResponse GetLogin()
        {
            return new HttpResponse(HttpStatusCode.Found).WithHeader("Location", ResolveUri.ForRoot());
        }

        [HttpCmd(HttpMethod.Get, "/logout")]
        public HttpResponse GetLogout()
        {
            return new HttpResponse(HttpStatusCode.Found).WithHeader("Location", ResolveUri.ForRoot());
            // var resp = new HttpResponse(401, new TextContent("Logged out")).WithHeader("Authorization", null);
            //return resp;
        }
    }
}
