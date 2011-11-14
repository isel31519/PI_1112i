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

        public ActionResult Detail(string id)
        {
            Fuc f = _repo.GetByAcr(id);
            if (f == null)
                return HttpNotFound();
            return View(f);
        }

        [Authorize]
        public ActionResult Edit(string id)
        {
            //id=acronimo da fuc a ser alterada
            Fuc f=_repo.GetByAcr(id);
            if (f == null) 
                return HttpNotFound();

            return View(f);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(string id,FucProposal f)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 404;
                return View(_repo.GetByAcr(id));
            }

            f.OriginalAcr = id;
            _proprepo.Add(f);
            //bruta!!
            return Redirect(string.Format("/{0}/{1}/{2}", "Proposal", "Detail", f.Idx));
        }

    }
}
