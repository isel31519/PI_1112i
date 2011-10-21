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
    class EditFucController
    {
        private readonly FucRepository _repo;
        private readonly ProposalRepository _proprepo;

        public EditFucController()
        {
            _repo = RepositoryLocator.GetFucRep();
            _proprepo = RepositoryLocator.GetPropRep();
        }
        
        [HttpCmd(HttpMethod.Get, "/fuc/{acr}/edit")]
        public HttpResponse GetFucAlterationForm(string acr)
        {
            return new HttpResponse(200, new EditFormView(_repo.GetByAcr(acr)));
        }

        [HttpCmd(HttpMethod.Get, "/fuc/{acr}/prop/{id}/edit")]
        public HttpResponse GetFucProposedAlterationForm(IPrincipal user,string acr, int id)
        {//proprio
            if (user.Identity.Name.Equals(_proprepo.GetById(id).User))
                 return new HttpResponse(200, new FucProposalEditView(_proprepo.GetById(Convert.ToInt32(id))));
            return new HttpResponse(403, new TextContent("Not Authorized"));
        }

        [HttpCmd(HttpMethod.Get, "/fuc/{acr}/prop/{id}")]
        public HttpResponse GetFucProposedAlteration(IPrincipal user,string acr, int id)
        {//proprio ou coord
            if (user.Identity.Name.Equals(_proprepo.GetById(id).User) || user.IsInRole("coord"))
                return new HttpResponse(200, new FucProposalView(_proprepo.GetById(id)));
            return new HttpResponse(403, new TextContent("Not Authorized"));
        }

        [HttpCmd(HttpMethod.Post, "/fuc/{acr}/prop/{id}/accept")]
        public HttpResponse PostFucProposedAccept(string acr,int id,IEnumerable<KeyValuePair<string, string>> content)
        {//coord
            Fuc fuc = _proprepo.GetById(id);

            _repo.Remove(acr);
            _repo.Add(fuc);
            _proprepo.Remove(id);
            return new HttpResponse(HttpStatusCode.SeeOther).WithHeader("Location", ResolveUri.For(fuc));
        }

        [HttpCmd(HttpMethod.Post, "/fuc/{acr}/prop/{id}/refuse")]
        public HttpResponse FucProposedRefuse(string acr, int id, IEnumerable<KeyValuePair<string, string>> content)
        {
            _proprepo.Remove(id);
            return new HttpResponse(HttpStatusCode.SeeOther).WithHeader("Location", ResolveUri.ForRoot());
        }

        [HttpCmd(HttpMethod.Post, "/fuc/{acr}/edit")]
        public HttpResponse PostFucProposedAlteration(IPrincipal user, IEnumerable<KeyValuePair<string, string>> content)
        {
            FucProposal fuc = ControllerUtils.CreateFuc(user,content);
            if (fuc == null) return new HttpResponse(HttpStatusCode.BadRequest);


            _proprepo.Add(fuc);
            return new HttpResponse(HttpStatusCode.SeeOther).WithHeader("Location", ResolveUri.For(fuc));  
        }

       

        [HttpCmd(HttpMethod.Post, "/fuc/{acr}/prop/{id}/edit")]
        public HttpResponse PostFucAlterationForm(IPrincipal user, string acr, string id, IEnumerable<KeyValuePair<string, string>> content)
        {

           FucProposal fuc =ControllerUtils.CreateFuc(user,content);
           if (fuc == null) return new HttpResponse(HttpStatusCode.BadRequest);
            _proprepo.Edit(Convert.ToInt32(id), fuc); ;
            return new HttpResponse(HttpStatusCode.SeeOther).WithHeader("Location", ResolveUri.For(fuc));
        }
    }
}
