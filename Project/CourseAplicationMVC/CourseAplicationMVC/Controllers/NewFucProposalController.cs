using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseAplicationLib;

namespace CourseAplicationMVC.Controllers
{
    [Authorize]
    public class NewFucProposalController : Controller
    {
        //
        // GET: /NewFucProposal/

        private readonly FucRepository _repo = RepositoryLocator.GetFucRep();
        private readonly ProposalRepository _newPropRepo = RepositoryLocator.GetNewPropRep();

        public ActionResult Index()
        {
            return View(_newPropRepo.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Detail(int id)
        {
            FucProposal p = _newPropRepo.GetById(id);
            if (p == null) return HttpNotFound("Proposal not found");
            if (!User.IsInRole("coord") && !User.Identity.Name.Equals(p.User)) return RedirectToAction("LogOn", "Account");
            return View(p);
        }


        public ActionResult PDetail(int id)
        {
            FucProposal p = _newPropRepo.GetById(id);
            if (p == null) return HttpNotFound("Proposal not found");
            if (!User.IsInRole("coord") && !User.Identity.Name.Equals(p.User)) return RedirectToAction("LogOn", "Account");
            return PartialView(p);
        }


        [HttpPost]
        public ActionResult Create(FucProposal fp)
        {

            if (!ModelState.IsValid)
                return View(_newPropRepo.GetById(fp.Idx));
            fp.User = User.Identity.Name;
            _newPropRepo.Add(fp);
            //bruta!!
            return Redirect(string.Format("/{0}/{1}/{2}", "NewFucProposal", "Detail", fp.Idx));
        }

        public ActionResult Edit(int id)
        {
            FucProposal p = _newPropRepo.GetById(id);
            if (p == null) return HttpNotFound("Proposal not found");
            if (!User.Identity.Name.Equals(p.User)) return RedirectToAction("LogOn", "Account");

            return View(_newPropRepo.GetById(id));
        }


        [HttpPost]
        public ActionResult Edit(int id, FucProposal fp)
        {
            /*if (!ModelState.IsValid)
              return View(_repo.GetById(f.Id));*/
            fp.User = User.Identity.Name;
            _newPropRepo.Edit(id, fp);
            
            //bruta
            return Redirect(string.Format("/{0}/{1}/{2}", "NewFucProposal", "Detail", fp.Idx));
        }

        [HttpPost]

        public ActionResult Refuse(int id)
        {
            FucProposal p = _newPropRepo.GetById(id);

            if (!User.IsInRole("coord") && !User.Identity.Name.Equals(p.User)) return RedirectToAction("LogOn", "Account");
            _newPropRepo.Remove(id);
            return Redirect(string.Format("/{0}", "NewFucProposal"));
        }

        [HttpPost]
        [Authorize(Roles = "coord")]
        public ActionResult Accept(int id)
        {
            Fuc f = _newPropRepo.GetById(id);
            _repo.Add(f);
            _newPropRepo.Remove(id);

            return Redirect(string.Format("/{0}/{1}/{2}", "Fuc", "Detail", f.Acr));
        }
    }
}
