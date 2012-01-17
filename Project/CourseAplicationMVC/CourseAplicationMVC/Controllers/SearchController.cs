using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseAplicationLib;

namespace CourseAplicationMVC.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        private readonly FucRepository _repo = RepositoryLocator.GetFucRep();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(String query)
        {
            var fucListToReturn = new List<Fuc>();

            foreach (var fuc in _repo.GetAll())
            {
                if (fuc.Name.ToLower().Contains(query.ToLower()))
                    fucListToReturn.Add(fuc);
            }

            return View(fucListToReturn);
        }

        [HttpPost]
        public ActionResult Find(string term)
        {
            string[] sites = _repo.FindFucName(term);
            return Json(sites, JsonRequestBehavior.AllowGet);
        }
    }
}
