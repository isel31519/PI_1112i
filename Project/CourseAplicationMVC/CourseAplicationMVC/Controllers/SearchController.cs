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
        public ActionResult Index(String searchQuery)
        {
            var fucListToReturn = new List<Fuc>();

            foreach (var fuc in _repo.GetAll())
            {
                if(fuc.Name.ToLower().Contains(searchQuery.ToLower()))
                    fucListToReturn.Add(fuc);
            }

            return View(fucListToReturn);
        }

    }
}
