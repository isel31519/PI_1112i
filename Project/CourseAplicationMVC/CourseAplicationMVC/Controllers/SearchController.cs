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


        //find utilizado com o autocomplete da biblioteca JQuery
        [HttpPost]
        public ActionResult Find(string term)
        {
            string[] fucsWithSearchTerm = _repo.FindFucName(term);
            return Json(fucsWithSearchTerm, JsonRequestBehavior.AllowGet);
        }

        //find utilizado para autosuggest implementado de raíz
        public ActionResult FindAllFucNames()
        {
            string[] fucsWithSearchTerm = _repo.GetAllFucNames();
            return Json(fucsWithSearchTerm, JsonRequestBehavior.AllowGet);
        }

        /*está igual ao de cima pq sim*/
        public ActionResult FindFucsAndAcrs()
        {
            string[] fucs = _repo.GetAllFucNames();
            return Json(fucs, JsonRequestBehavior.AllowGet);
        }
    }
}
