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
        public HttpResponse GetFucAlterationForm(HttpListenerRequest req,string acr)
        {
            string auth = req.Headers["Authorization"];
            auth = auth.Replace("Basic ", "");
            string userPassDecoded = Encoding.UTF8.GetString(Convert.FromBase64String(auth));
            string[] userPasswd = userPassDecoded.Split(':');
            var userid = userPasswd[0];
            //funca mal por causa dos acr
            if (_proprepo.HaveProp(userid,acr)) return new HttpResponse(HttpStatusCode.BadRequest, new TextContent("You already have a proposal for this course"));
            return new HttpResponse(200, new EditFormView(_repo.GetByAcr(acr)));
        }

        [HttpCmd(HttpMethod.Get, "/fuc/{acr}/prop/{id}/edit")]
        public HttpResponse GetFucProposedAlterationForm(string acr, string id)
        {
            return new HttpResponse(200, new FucProposalEditView(_proprepo.GetById(Convert.ToInt32(id))));
        }

        [HttpCmd(HttpMethod.Get, "/fuc/{acr}/prop/{id}")]
        public HttpResponse GetFucProposedAlteration(string acr, string id)
        {
            return new HttpResponse(200, new FucProposalView(_proprepo.GetById(Convert.ToInt32(id))));
        }

        [HttpCmd(HttpMethod.Post, "/fuc/{acr}/prop/{id}/accept")]
        public HttpResponse PostFucProposedAccept(string acr,int id,IEnumerable<KeyValuePair<string, string>> content)
        {
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
        public HttpResponse PostFucProposedAlteration(HttpListenerRequest req,IEnumerable<KeyValuePair<string, string>> content)
        {
            FucProposal fuc = ControllerUtils.CreateFuc(req,content);
            if (fuc == null) return new HttpResponse(HttpStatusCode.BadRequest);


            _proprepo.Add(fuc);
            return new HttpResponse(HttpStatusCode.SeeOther).WithHeader("Location", ResolveUri.For(fuc));  
        }

       

        [HttpCmd(HttpMethod.Post, "/fuc/{acr}/prop/{id}/edit")]
        public HttpResponse PostFucAlterationForm(HttpListenerRequest req,string acr,string id, IEnumerable<KeyValuePair<string, string>> content)
        {

           FucProposal fuc =ControllerUtils.CreateFuc(req,content);
           if (fuc == null) return new HttpResponse(HttpStatusCode.BadRequest);
            _proprepo.Edit(Convert.ToInt32(id), fuc); ;
            return new HttpResponse(HttpStatusCode.SeeOther).WithHeader("Location", ResolveUri.For(fuc));
        }
    }
}
