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
       
        private readonly ProposalRepository _repo = RepositoryLocator.GetPropRep();
        
        //
        // GET: /Create/

        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }

        public ActionResult Detail(int id)
        {
            return View(_repo.GetById(id));
        }

        public ActionResult Edit(int id)
        {
            return View(_repo.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(Fuc f)
        {
            return View();
        }

    }
}
