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
    class CreateFucProposalController
    {
        private readonly FucRepository _repo;
        public CreateFucProposalController()
        {
        _repo = FucRepositoryLocator.GetNewPropRep();
        }

        [HttpCmd(HttpMethod.Get, "/create")]
        public HttpResponse GetNewFucProposalForm()
        {
            return new HttpResponse(200, new ProposalListView(_repo.GetAll())); //mudar nome proposal
        }

        [HttpCmd(HttpMethod.Get, "/newfuc/{id}")]
        public HttpResponse GetNewProposedFuc(string id)
        {
            var prop = _repo.GetByAcr(id);
            return prop == null ? new HttpResponse(HttpStatusCode.NotFound) : new HttpResponse(200, new NewFucProposalView(prop)); 
            //mudar nome de New Fuc proposal
        }

        [HttpCmd(HttpMethod.Get, "/newfuc/{id}/edit")]
        public HttpResponse GetEditNewProposedFuc(string id)
        {
            var prop = _repo.GetByAcr(id);
            return prop == null ? new HttpResponse(HttpStatusCode.NotFound) : new HttpResponse(200, new NewFucProposalEditView(prop));
            //mudar nome de New Fuc proposal edit
        }


        [HttpCmd(HttpMethod.Post, "/create")]
        public HttpResponse Post(IEnumerable<KeyValuePair<string, string>> content)
        {
            var acr = content.Where(p => p.Key == "acr").Select(p => p.Value).FirstOrDefault();
            var name = content.Where(p => p.Key == "name").Select(p => p.Value).FirstOrDefault();
            var required = content.Where(p => p.Key == "required").Select(p => p.Value).FirstOrDefault();
            var semester = content.Where(p => p.Key == "semester").Select(p => p.Value).FirstOrDefault();
            var prerequisites = content.Where(p => p.Key == "prerequisites").Select(p => p.Value).FirstOrDefault();
            var ects = content.Where(p => p.Key == "ects").Select(p => p.Value).FirstOrDefault();
            var objectives = content.Where(p => p.Key == "objectives").Select(p => p.Value).FirstOrDefault();
            var results = content.Where(p => p.Key == "results").Select(p => p.Value).FirstOrDefault();
            var evaluation = content.Where(p => p.Key == "evaluation").Select(p => p.Value).FirstOrDefault();
            var program = content.Where(p => p.Key == "program").Select(p => p.Value).FirstOrDefault();


            if (acr == null|| name == null || required == null || semester == null || prerequisites == null || ects == null || 
                objectives == null || results == null || evaluation == null || program == null)
            {
                return new HttpResponse(HttpStatusCode.BadRequest);
            }

            var fuc = new FucProposal(name, acr, required, ects);
            fuc.Prerequisites = prerequisites //acabar

            _repo.Add(fuc);
            return new HttpResponse(HttpStatusCode.SeeOther).WithHeader("Location", ResolveUri.For(fuc));
        }

    }
}
