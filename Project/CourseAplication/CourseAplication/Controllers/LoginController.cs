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
    class LoginController
    {
        
        [HttpCmd(HttpMethod.Get, "/login")]
        public HttpResponse GetLogin()
        {
            return new HttpResponse(HttpStatusCode.Found).WithHeader("Location", ResolveUri.ForRoot());
        }

        [HttpCmd(HttpMethod.Get, "/logout")]
        public HttpResponse GetLogout()
        {
            return new HttpResponse(HttpStatusCode.OK, new HtmlDoc("LOGOUT", HtmlDoc.A(ResolveUri.ForRoot(), "Home"), HtmlDoc.H1(HtmlDoc.Text("Logged Out"))));
            // var resp = new HttpResponse(401, new TextContent("Logged out")).WithHeader("Authorization", null);
            //return resp;
        }
    }
}
