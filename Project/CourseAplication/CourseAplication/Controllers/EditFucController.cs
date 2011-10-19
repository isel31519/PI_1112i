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
        public HttpResponse PostFucProposedAlteration(IEnumerable<KeyValuePair<string, string>> content)
        {
            FucProposal fuc = CreateFuc(content);
            if (fuc == null) return new HttpResponse(HttpStatusCode.BadRequest);


            _proprepo.Add(fuc);
            return new HttpResponse(HttpStatusCode.SeeOther).WithHeader("Location", ResolveUri.For(fuc));  
        }

        public static FucProposal CreateFuc(IEnumerable<KeyValuePair<string, string>> content)
        {
            var acro = content.Where(p => p.Key == "acr").Select(p => p.Value).FirstOrDefault();
            var name = content.Where(p => p.Key == "name").Select(p => p.Value).FirstOrDefault();
            var required = content.Where(p => p.Key == "req").Select(p => p.Value).FirstOrDefault();
            var semester = content.Where(p => p.Key == "sem").Select(p => p.Value).FirstOrDefault();
            var prerequisites = content.Where(p => p.Key == "prereq").Select(p => p.Value).FirstOrDefault();
            var ects = content.Where(p => p.Key == "ects").Select(p => p.Value).FirstOrDefault();
            var userid = content.Where(p => p.Key == "userid").Select(p => p.Value).FirstOrDefault(); //será mesmo user a key?
            var objectives = content.Where(p => p.Key == "objectives").Select(p => p.Value).FirstOrDefault();
            var results = content.Where(p => p.Key == "results").Select(p => p.Value).FirstOrDefault();
            var evaluation = content.Where(p => p.Key == "evaluation").Select(p => p.Value).FirstOrDefault();
            var program = content.Where(p => p.Key == "program").Select(p => p.Value).FirstOrDefault();


            if (acro == null || name == null || required == null || semester == null || ects == null ||
                objectives == null || results == null || evaluation == null || program == null/* || userid == null*/)
            {
                return null;
            }


            var fuc = new FucProposal(name, acro, required.Equals("on") ? true : false, Convert.ToDouble(ects), Convert.ToInt32(userid));


            foreach (var sem in semester.Split(' '))
            {
                if (sem != "")
                    fuc.Semester = Convert.ToUInt16(sem);
            }


            foreach (var pre in prerequisites.Split(' '))
            {
                if (pre != "")
                    fuc.Prerequisites = pre;
            }



            fuc.AddDescription("Objectives", objectives);
            fuc.AddDescription("Results", results);
            fuc.AddDescription("Evaluation", evaluation);
            fuc.AddDescription("Program", program);

            return fuc;
        }

        [HttpCmd(HttpMethod.Post, "/fuc/{acr}/prop/{id}/edit")]
        public HttpResponse PostFucAlterationForm(string acr,string id, IEnumerable<KeyValuePair<string, string>> content)
        {

           FucProposal fuc =CreateFuc(content);
           if (fuc == null) return new HttpResponse(HttpStatusCode.BadRequest);
            _proprepo.Edit(Convert.ToInt32(id), fuc); ;
            return new HttpResponse(HttpStatusCode.SeeOther).WithHeader("Location", ResolveUri.For(fuc));
        }
    }
}
