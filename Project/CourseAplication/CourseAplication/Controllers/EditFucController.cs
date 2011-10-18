using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CourseAplication.Model;
using CourseAplication.Views;
using PI.WebGarten;
using PI.WebGarten.MethodBasedCommands;

namespace CourseAplication.Controllers
{
    class EditFucController
    {
        private readonly ProposalRepository _repo;

        public EditFucController()
        {
            _repo = RepositoryLocator.GetPropRep();
        }
        
        [HttpCmd(HttpMethod.Get, "/fuc/{acr}/id")]
        public HttpResponse GetFucAlterationForm(Fuc fuc)
        {
            return new HttpResponse(200, new EditFormView(fuc));
        }
    }
}
