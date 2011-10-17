using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CourseAplication.Views;
using PI.WebGarten;
using PI.WebGarten.MethodBasedCommands;

namespace CourseAplication.Controllers
{
    class RootController
    {
        [HttpCmd(HttpMethod.Get, "/")]
        public HttpResponse GetRoot()
        {
            return new HttpResponse(200, new RootView());
        }
    }
}
