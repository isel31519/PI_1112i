using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using CourseAplication.Model;
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
            if (auth == null) return new HttpResponse(200, new RootView());
            auth = auth.Replace("Basic ", "");
            string userPassDecoded = Encoding.UTF8.GetString(Convert.FromBase64String(auth));
            string[] userPasswd = userPassDecoded.Split(':');
            var user = userPasswd[0];

            //if(user == null) return new HttpResponse(200, new RootView());
            if (RepositoryLocator.GetUserRep().GetById(user).Role != null) return new HttpResponse(200, new RootViewMaster(user));
            return new HttpResponse(200, new RootViewPrivate(user));
        }
    }
}
