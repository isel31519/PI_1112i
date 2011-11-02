using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseAplicationLib;

namespace CourseAplicationMVC.Controllers
{
    public class CreateController : Controller
    {
       
        private readonly ProposalRepository _repo = RepositoryLocator.GetFucRep();
        
        //
        // GET: /Create/

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
        public ActionResult Edit(Fuc f)
        {
            return View();
        }

    }
}
