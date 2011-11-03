using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseAplicationLib;

namespace CourseAplicationMVC.Controllers
{
    public class ProposalController : Controller
{

        private readonly ProposalRepository _repo = RepositoryLocator.GetPropRep();
        //
        // GET: /Proposal/

        public ActionResult Index()
        {
            //notfound
            return View(_repo.GetAll());
        }

        public ActionResult Detail(int id)
        {
            //notfound
            return View(_repo.GetById(id));
        }

        public ActionResult Edit(int id, string acr)
        {
            //notfound
            return View(_repo.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(string acr,FucProposal f)
        {
            /*if (!ModelState.IsValid)
                return View(_repo.GetById(f.Id));

            _repo.Edit(f.Id, f);*/
            //bruta!!
            return Redirect(string.Format("/{0}/{1}/{2}", "Proposal","Detail", f.Id));
        }
}
}
