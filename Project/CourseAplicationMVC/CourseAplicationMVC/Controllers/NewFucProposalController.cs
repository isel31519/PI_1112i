using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseAplicationLib;

namespace CourseAplicationMVC.Controllers
{
    public class NewFucProposalController : Controller
    {
        //
        // GET: /NewFucProposal/

        private readonly ProposalRepository _repo = RepositoryLocator.GetNewPropRep();

        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Detail(int id)
        {
            FucProposal fp;
            if ((fp = _repo.GetById(id))!=null)
                return View(fp);
            return HttpNotFound("That resource does not exist");
        }

        [HttpPost]
        public ActionResult Create(FucProposal fp)
        {
            /*if (!ModelState.IsValid)
              return View(_repo.GetById(f.Id));*/

            _repo.Add(fp);
            //bruta!!
            return Redirect(string.Format("/{0}/{1}/{2}", "NewFucProposal", "Detail", fp.Idx));
        }

        public ActionResult Edit(int id)
        {
            //notfound
            return View(_repo.GetById(id));
        }


        [HttpPost]
        public ActionResult Edit(int id, FucProposal fp)
        {
            /*if (!ModelState.IsValid)
              return View(_repo.GetById(f.Id));*/
            _repo.Edit(id, fp);
            
            //bruta
            return Redirect(string.Format("/{0}/{1}/{2}", "NewFucProposal", "Detail", fp.Idx));
        }
    }
}
