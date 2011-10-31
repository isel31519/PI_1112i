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
        // GET: /Fuc/

        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }

        public ActionResult Detail(string Acr)
        {
            return View(_repo.GetByAcr(Acr));
        }

    }
}
