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

        [Authorize(Roles="admin")]
        public ActionResult Admin()
        {
            SelectList s=new SelectList(Membership.GetAllUsers());
            return View(s);
        }
         [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Admin(string user, ICollection<string> roles)
        {
            Membership.GetUser(user);
            Roles.RemoveUserFromRoles(user,Roles.GetRolesForUser(user));
            Roles.AddUserToRoles(user,roles.ToArray());
            return RedirectToAction("Admin", "Account");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.Password);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    //return RedirectToAction("ChangePasswordSuccess");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            return View(model);
        }
    }
}
