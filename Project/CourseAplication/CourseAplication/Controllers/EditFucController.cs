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

    }
}
