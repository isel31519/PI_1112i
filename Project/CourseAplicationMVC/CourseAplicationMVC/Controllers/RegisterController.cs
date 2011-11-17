using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Profile;
using System.Web.Security;
using CourseAplicationLib;
using CourseAplicationMVC.Models;

namespace CourseAplicationMVC.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(RegisterUserModel user)
        {

            if (ModelState.IsValid)
            {
               MembershipCreateStatus createStatus;
               MembershipUser u = Membership.CreateUser(user.Username, user.Password, user.Email, null, null, false, null, out createStatus);
                if (createStatus == MembershipCreateStatus.Success)
                {
                    var profile = (UserProfile) ProfileBase.Create(user.Username);
                    profile.Number = user.Number;
                    profile.Email = user.Email;
                    profile.Save();
                    
                   // string randomId = "asdfgtre";//criar random(ou n)id k identifica
                    var mail = new MailMessage("pi.admin.li51n.g02@sapo.pt", user.Email,
                                                       "Account verification",
                                                       "Click the link below to activate your account: " +
                                                       "http://"+Request.UserHostName+"/Register/Activate/" + user.Username);

                    var s = new SmtpClient
                                {
                                    Host = "smtp.sapo.pt",
                                    Credentials =new System.Net.NetworkCredential("pi.admin.li51n.g02@sapo.pt", "adminslb")
                                };

                    s.Send(mail);
                    return RedirectToAction("Index", "Home");

                }
           
            }

            return View(user);

        }

        public ActionResult Activate(string id)
        {
            MembershipUser u = Membership.GetUser(id);
            if (u == null) return HttpNotFound("Resource not found");
            u.IsApproved = true;
          
            Membership.UpdateUser(u);
            FormsAuthentication.SetAuthCookie(u.UserName, false);
            return RedirectToAction("Index", "Home");
        }
    }
}
