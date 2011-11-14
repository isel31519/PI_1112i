using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CourseAplicationLib;

namespace CourseAplicationMVC.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserRepository _repo = RepositoryLocator.GetUserRep();
        //
        // GET: /Account/

        public ActionResult LogOn(string returnUrl)
        {
            ViewData["RetUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(string returnUrl,User user)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(user.Name, user.Pass))
                {
                    FormsAuthentication.SetAuthCookie(user.Name,false);
                    if (returnUrl != null && !returnUrl.Equals(""))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect name or password.");
                }
            }


            return View(user);

        }


        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
