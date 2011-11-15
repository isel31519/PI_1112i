using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CourseAplicationLib;
using CourseAplicationMVC.Models;

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
        public ActionResult LogOn(string returnUrl, LogInModel user)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(user.Username, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.Username,false);
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

        //[Authorize(Roles="admin")]
        public ActionResult Admin()
        {
            SelectList s=new SelectList(Membership.GetAllUsers());
            return View(s);
        }


        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
