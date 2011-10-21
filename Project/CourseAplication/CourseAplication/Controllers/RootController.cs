using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using CourseAplication.Views;
using PI.WebGarten;
using PI.WebGarten.HttpContent.Html;
using PI.WebGarten.MethodBasedCommands;

namespace CourseAplication.Controllers
{
    class RootController
    {
        [HttpCmd(HttpMethod.Get, "/")]
        public HttpResponse GetRoot(HttpListenerRequest req)
        {
            string auth = req.Headers["Authorization"];
            auth = auth.Replace("Basic ", "");
            string userPassDecoded = Encoding.UTF8.GetString(Convert.FromBase64String(auth));
            string[] userPasswd = userPassDecoded.Split(':');
            var username = userPasswd[0];


            //return new HttpResponse(200, new RootView());
            return new HttpResponse(200, new RootViewPrivate(username));
        }
    }
}
