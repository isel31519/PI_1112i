using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseAplicationLib;

namespace CourseAplicationMVC.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/

        private readonly UserRepository _repoUsers = RepositoryLocator.GetUserRep();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User u)
        {
            if (!ModelState.IsValid) return View();
            _repoUsers.Add(u);
            return Redirect(string.Format("/{0}", "Home"));
        }
    }
}
