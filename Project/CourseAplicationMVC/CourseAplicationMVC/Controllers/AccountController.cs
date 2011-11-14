using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
        public ActionResult LogOn(string returnUrl,User u)
        {
            if (ModelState.IsValid)
            {
                User user = _repo.GetById(u.Name);
                if (user == null) return View();//n existe user
                if (!user.Match(u.Pass)) return View();

                FormsAuthentication.SetAuthCookie(u.Name, false);
                return Redirect(returnUrl);
                //return RedirectToAction("Index", "Home");

            }
            else
            {
                ModelState.AddModelError("", "Incorrect name or password.");
            }

            return View();

            /*if (!ModelState.IsValid)
            {
                
            }
            User user;
            if (!(user=_repo.GetById(u.Name)).Match(u.Pass)) return View();
            var principal = new GenericPrincipal( new GenericIdentity(u.Name),new string[]{user.Role} );

            FormsAuthentication.SetAuthCookie(user.Name, false);
            /* FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.Email,
            DateTime.Now, DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
            createPersistentCookie, userData);
            string hashedTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashedTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);*/

        }



        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
