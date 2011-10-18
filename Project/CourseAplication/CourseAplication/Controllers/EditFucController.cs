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

        public EditFucController()
        {
            _repo = RepositoryLocator.GetFucRep();
        }
        
        [HttpCmd(HttpMethod.Get, "/fuc/{acr}/edit")]
        public HttpResponse GetFucAlterationForm(string acr)
        {
            return new HttpResponse(200, new EditFormView(_repo.GetByAcr(acr)));
        }


    }
}
