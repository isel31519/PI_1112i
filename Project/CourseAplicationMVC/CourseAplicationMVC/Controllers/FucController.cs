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

        public ActionResult Edit(string id)
        {
            //id=acronimo da fuc a ser alterada
            Fuc f=_repo.GetByAcr(id);
            if (f == null) 
                return HttpNotFound();

            return View(f);
        }

        [HttpPost]
        public ActionResult Edit(string id,FucProposal f)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 404;
                return View(_repo.GetByAcr(id));
            }

            _proprepo.Add(f);
            //bruta!!
            return Redirect(string.Format("/{0}/{1}/{2}", "Proposal", "Detail", f.Idx));
        }

        [HttpPost]
        public ActionResult Refuse(int id)
        {
            _proprepo.Remove(id);
            return Redirect(string.Format("/{0}", "Proposal"));
        }

        [HttpPost]
        public ActionResult Accept(int id)
        {
            Fuc f = _proprepo.GetById(id);
            //remover a antiga Fuc _repo.Remove(acr);
            _repo.Add(f);
            _proprepo.Remove(id);

            return Redirect(string.Format("/{0}/{1}/{2}", "Fuc", "Detail", f.Acr));
        }
    }
}
