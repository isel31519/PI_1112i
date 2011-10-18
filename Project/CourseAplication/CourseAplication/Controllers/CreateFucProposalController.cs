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
        private readonly ProposalRepository _repo;
        public CreateFucProposalController()
        {
            _repo = RepositoryLocator.GetNewPropRep();
        }

        [HttpCmd(HttpMethod.Get, "/create")]
        public HttpResponse GetNewFucProposalForm()
        {
            return new HttpResponse(200, new CreateNewFucProposalView());
        }

        [HttpCmd(HttpMethod.Get, "/newfuc/{id}")]
        public HttpResponse GetNewProposedFuc(int id)
        {
            var prop = _repo.GetById(id);
            return prop == null ? new HttpResponse(HttpStatusCode.NotFound) : new HttpResponse(200, new NewFucProposalView(prop)); 
        }

        [HttpCmd(HttpMethod.Get, "/newfuc/{id}/edit")]
        public HttpResponse GetEditNewProposedFuc(int id)
        {
            var prop = _repo.GetById(id);
            return prop == null ? new HttpResponse(HttpStatusCode.NotFound) : new HttpResponse(200, new NewFucProposalEditView(prop));
        }


        [HttpCmd(HttpMethod.Post, "/create")]
        public HttpResponse PostNewFuc(IEnumerable<KeyValuePair<string, string>> content)
        {
            var acr = content.Where(p => p.Key == "acr").Select(p => p.Value).FirstOrDefault();
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



            if (acr == null|| name == null || required == null || semester == null || prerequisites == null || ects == null || 
                objectives == null || results == null || evaluation == null || program == null || userid == null)
            {
                return new HttpResponse(HttpStatusCode.BadRequest);
            }


            var fuc = new FucProposal(name, acr, required.Equals("true")?true:false, Convert.ToDouble(ects), Convert.ToInt32(userid));


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


            _repo.Add(fuc);
            return new HttpResponse(HttpStatusCode.SeeOther).WithHeader("Location", ResolveUri.ForNew(fuc));
        }


        [HttpCmd(HttpMethod.Post, "/newfuc/{id}/edit")]
        public HttpResponse PostNewFucEdit(string id, IEnumerable<KeyValuePair<string, string>> content)
        {

            var acr = content.Where(p => p.Key == "acr").Select(p => p.Value).FirstOrDefault();
            var name = content.Where(p => p.Key == "name").Select(p => p.Value).FirstOrDefault();
            var required = content.Where(p => p.Key == "required").Select(p => p.Value).FirstOrDefault();
            var semester = content.Where(p => p.Key == "semester").Select(p => p.Value).FirstOrDefault();
            var prerequisites = content.Where(p => p.Key == "prerequisites").Select(p => p.Value).FirstOrDefault();
            var ects = content.Where(p => p.Key == "ects").Select(p => p.Value).FirstOrDefault();
            var userid = content.Where(p => p.Key == "userid").Select(p => p.Value).FirstOrDefault(); //será mesmo user a key?
            var objectives = content.Where(p => p.Key == "objectives").Select(p => p.Value).FirstOrDefault();
            var results = content.Where(p => p.Key == "results").Select(p => p.Value).FirstOrDefault();
            var evaluation = content.Where(p => p.Key == "evaluation").Select(p => p.Value).FirstOrDefault();
            var program = content.Where(p => p.Key == "program").Select(p => p.Value).FirstOrDefault();


            if (acr == null || name == null || required == null || semester == null || prerequisites == null || ects == null ||
                objectives == null || results == null || evaluation == null || program == null || userid == null)
            {
                return new HttpResponse(HttpStatusCode.BadRequest);
            }


            var fuc = new FucProposal(name, acr, required.Equals("on") ? true : false, Convert.ToDouble(ects), Convert.ToInt32(userid));


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

            _repo.Edit(Convert.ToInt32(id), fuc);
            return new HttpResponse(HttpStatusCode.SeeOther).WithHeader("Location", ResolveUri.ForNew(fuc));
        }

    }
}
