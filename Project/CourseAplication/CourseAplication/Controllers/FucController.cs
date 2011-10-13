using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using CourseAplication.Model;
using CourseAplication.Views;
using PI.WebGarten;
using PI.WebGarten.MethodBasedCommands;

namespace CourseAplication.Controllers
{
    class FucController
    {
         private readonly FucRepository _repo;
         public FucController()
        {
            _repo = FucRepositoryLocator.Get();
        }

        [HttpCmd(HttpMethod.Get, "/fuc/{id}")]
        public HttpResponse Get(string id)
        {
            var td = _repo.GetByAcr(id);
            return td == null ? new HttpResponse(HttpStatusCode.NotFound) : new HttpResponse(200, new FucView(td));
        }
    }

}
