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
            var fucList = _repo.GetAll();
            var fucListToReturn = new List<Fuc>();

            foreach (var fuc in fucList)
            {
                if(fuc.Name.ToLower().Contains(searchQuery))
                    fucListToReturn.Add(fuc);
            }

            return View(fucListToReturn);
        }

    }
}
