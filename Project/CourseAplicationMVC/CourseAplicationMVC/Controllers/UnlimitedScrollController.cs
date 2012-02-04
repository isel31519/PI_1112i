using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseAplicationLib;

namespace CourseAplicationMVC.Controllers
{
    public class UnlimitedScrollController : Controller
    {
        //
        // GET: /UnlimitedScroll/
        private readonly FucRepository _repo = RepositoryLocator.GetFucRep();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMoreFucs()
        {
            IEnumerable<Fuc> fucs = _repo.GetPartialFucs(10);
            return Json(fucs, JsonRequestBehavior.AllowGet);
        }
    }
}
