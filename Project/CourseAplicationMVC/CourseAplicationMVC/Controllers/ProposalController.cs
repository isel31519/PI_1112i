using System;
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
            //notfound
            return View(_proprepo.GetAll());
        }

        public ActionResult Detail(int id)
        {
            //notfound passar o acronimo da fuc original
            return View(_proprepo.GetById(id));
        }

        public ActionResult Edit(int id, string oracr)
        {
            //notfound
            return View(_proprepo.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, string oracr, FucProposal f)
        {
            /*if (!ModelState.IsValid)
                return View(_repo.GetById(f.Id));*/
            f.OriginalAcr = oracr;
            _proprepo.Edit(id, f);
            //bruta!!
            return Redirect(string.Format("/{0}/{1}/{2}", "Proposal","Detail", f.Idx));
        }
        [HttpPost]
        public ActionResult Refuse(int id)
        {
            _proprepo.Remove(id);
            return Redirect(string.Format("/{0}", "Proposal"));
        }

        [HttpPost]
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
