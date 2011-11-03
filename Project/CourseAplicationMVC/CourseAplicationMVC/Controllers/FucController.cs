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
            return View(_repo.GetByAcr(id));
        }

        public ActionResult Edit(string id)
        {
            return View(_repo.GetByAcr(id));
        }

        [HttpPost]
        public ActionResult Edit(string id,FucProposal f)
        {
           /* if (!ModelState.IsValid)
            {
                Response.StatusCode = 404;
                return View(_repo.GetByAcr(id));
            }*/

            _proprepo.Add(f);
            //bruta!!
            return Redirect(string.Format("/{0}/?id={1}&acr={2}", "Proposal", f.Id,id));
        }
    }
}
