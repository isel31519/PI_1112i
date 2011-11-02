using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseAplicationLib;

namespace CourseAplicationMVC.Controllers
{
    public class FucController : Controller
    {
        private readonly FucRepository _repo = RepositoryLocator.GetFucRep();
        private readonly ProposalRepository _proprepo = RepositoryLocator.GetPropRep();
        // GET: /Fuc/

        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }

        public ActionResult Detail(string Acr)
        {
            return View(_repo.GetByAcr(Acr));
        }

        public ActionResult Edit(string Acr)
        {
            return View(_repo.GetByAcr(Acr));
        }

        [HttpPost]
        public ActionResult Edit(string Acr,FucProposal f)
        {
            if(!ModelState.IsValid)
                return View(_repo.GetByAcr(Acr));

            _proprepo.Add(f);
            return Redirect("/fuc/proposal"+f.Id);
        }
    }
}
