using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseAplicationLib;

namespace CourseAplicationMVC.Controllers
{
    [Authorize]
    public class ProposalController : Controller
{

        private readonly ProposalRepository _proprepo = RepositoryLocator.GetPropRep();
        private readonly FucRepository _repo = RepositoryLocator.GetFucRep();
        //
        // GET: /Proposal/

        public ActionResult Index()
        {
             IEnumerable<FucProposal> props= _proprepo.GetAll();
            // if (props == null) return HttpNotFound("Proposal not found");
             return View(props);
        }

        public ActionResult Detail(int id)
        {
           
            FucProposal p = _proprepo.GetById(id);
            if (p == null) return HttpNotFound("Proposal not found");
            if (!User.IsInRole("coord") && !User.Identity.Name.Equals(p.User))
                return RedirectToAction("LogOn", "Account");

            return View(_proprepo.GetById(id));
        }

        public ActionResult Edit(int id, string oracr)
        {
            FucProposal p = _proprepo.GetById(id);
            if (p == null) return HttpNotFound("Proposal not found");
            if (!User.IsInRole("coord") && !User.Identity.Name.Equals(p.User))
                return RedirectToAction("LogOn", "Account");

            return View(_proprepo.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, string oracr, FucProposal f)
        {
            
            if (!ModelState.IsValid)
                return View(_proprepo.GetById(id));
            f.OriginalAcr = oracr;
            f.User = User.Identity.Name;
            _proprepo.Edit(id, f);
            //bruta!!
            return Redirect(string.Format("/{0}/{1}/{2}", "Proposal","Detail", f.Idx));
        }
        [HttpPost]

        public ActionResult Refuse(int id)
        {
            FucProposal p = _proprepo.GetById(id);

            if (!User.IsInRole("coord") && !User.Identity.Name.Equals(p.User)) return RedirectToAction("LogOn", "Account");
            _proprepo.Remove(id);
            return Redirect(string.Format("/{0}", "Proposal"));
        }

        [HttpPost]
        [Authorize(Roles = "coord")]
        public ActionResult Accept(int id, string oracr)
        {
            FucProposal f = _proprepo.GetById(id);
            _repo.Remove(f.OriginalAcr);
            _repo.Add(f);
            _proprepo.Remove(id);

            return Redirect(string.Format("/{0}/{1}/{2}", "Fuc", "Detail", f.Acr));
        }
    }
}
