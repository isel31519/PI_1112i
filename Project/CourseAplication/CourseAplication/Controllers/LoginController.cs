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

            //var resp = new HttpResponse(401, new TextContent("This server could not verify that you are authorized to access the document requested. Either you supplied the wrong credentials (e.g., bad password), or your browser doesn't understand how to supply the credentials required."));

            //resp.WithHeader("WWW-Authenticate", "Basic realm=\"Private Area\"");
            return new HttpResponse(HttpStatusCode.SeeOther).WithHeader("Location", ResolveUri.ForRoot());
        }
      
    }
}
