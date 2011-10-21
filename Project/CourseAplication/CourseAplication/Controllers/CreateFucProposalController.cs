using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using CourseAplication.Model;
using CourseAplication.Views;
using PI.WebGarten;
using PI.WebGarten.HttpContent.Html;
using PI.WebGarten.MethodBasedCommands;

namespace CourseAplication.Controllers
{
    class CreateFucProposalController
    {
        private readonly FucRepository _repo;
        private readonly ProposalRepository _proprepo;
        public CreateFucProposalController()
        {
            _repo = RepositoryLocator.GetFucRep();
            _proprepo = RepositoryLocator.GetNewPropRep();
        }

        [HttpCmd(HttpMethod.Get, "/create")]
        public HttpResponse GetNewFucProposalForm()
        {
            return new HttpResponse(200, new CreateNewFucProposalView());
        }

        [HttpCmd(HttpMethod.Get, "/newfuc/{id}")]
        public HttpResponse GetNewProposedFuc(IPrincipal user,int id)
        {//proprio ou coord
           
            var prop = _proprepo.GetById(id);
            if (prop == null) new HttpResponse(HttpStatusCode.NotFound);
            if (user.Identity.Name.Equals(prop.User) || user.IsInRole("coord"))
                return  new HttpResponse(200, new NewFucProposalView(prop));
            return new HttpResponse(403, new TextContent("Not Authorized"));
        }

        [HttpCmd(HttpMethod.Get, "/newfuc/{id}/edit")]
        public HttpResponse GetEditNewProposedFuc(IPrincipal user,int id)
        {//proprio
            var prop = _proprepo.GetById(id);
            if (prop == null) new HttpResponse(HttpStatusCode.NotFound);
            if (user.Identity.Name.Equals(prop.User))
                return  new HttpResponse(200, new NewFucProposalEditView(prop));
            return new HttpResponse(403, new TextContent("Not Authorized"));
        }


        [HttpCmd(HttpMethod.Post, "/create")]
        public HttpResponse PostNewFuc(IPrincipal user, IEnumerable<KeyValuePair<string, string>> content)
        {
            FucProposal fuc = ControllerUtils.CreateFuc(user,content);
            if (fuc == null) return new HttpResponse(HttpStatusCode.BadRequest);

            _proprepo.Add(fuc);
            return new HttpResponse(HttpStatusCode.SeeOther).WithHeader("Location", ResolveUri.ForNew(fuc));
        }


        [HttpCmd(HttpMethod.Post, "/newfuc/{id}/edit")]
        public HttpResponse PostNewFucEdit(IPrincipal user, int id, IEnumerable<KeyValuePair<string, string>> content)
        {

            FucProposal fuc = ControllerUtils.CreateFuc(user,content);
            if (fuc == null) return new HttpResponse(HttpStatusCode.BadRequest);

            _proprepo.Edit(id, fuc);
            return new HttpResponse(HttpStatusCode.SeeOther).WithHeader("Location", ResolveUri.ForNew(fuc));
        }


        [HttpCmd(HttpMethod.Post, "/newfuc/{id}/accept")]
        public HttpResponse PostNewFucAccept(int id, IEnumerable<KeyValuePair<string, string>> content)
        {

            Fuc fuc = _proprepo.GetById(id);
            _repo.Add(fuc);
            _proprepo.Remove(id);
            return new HttpResponse(HttpStatusCode.SeeOther).WithHeader("Location", ResolveUri.For(fuc));
        }

        [HttpCmd(HttpMethod.Post, "/newfuc/{id}/refuse")]
        public HttpResponse PostNewFucRefuse(int id, IEnumerable<KeyValuePair<string, string>> content)
        {//proprio ou coord
            _proprepo.Remove(id);
            return new HttpResponse(HttpStatusCode.SeeOther).WithHeader("Location", ResolveUri.ForRoot());
        }

    }
}
