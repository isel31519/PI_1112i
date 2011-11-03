using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseAplicationLib;

namespace CourseAplicationMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserRepository _repo = RepositoryLocator.GetUserRep();
        
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult IndexUser(string user)
        {
            return View();
        }
    }
}
