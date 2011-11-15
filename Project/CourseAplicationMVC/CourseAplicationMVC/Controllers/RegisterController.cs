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

        private readonly UserRepository _repoUsers = RepositoryLocator.GetUserRep();

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
               /*ProfileBase.Create(user.Username);*/
          
                if (createStatus == MembershipCreateStatus.Success)
                {
                    string randomId = "asdfgtre";//criar random(ou n)id k identifica
                    MailMessage mail = new MailMessage("mail", user.Email,
                                                       "Account verification",
                                                       "Click the link below to activate your account: " +
                                                       "http://localhost:51872/Register/Activate/" + randomId);

                    SmtpClient s = new SmtpClient();
                            s.Host = "smtp.sapo.pt";
                            s.Credentials = new System.Net.NetworkCredential("mail", "pass");//criar admin mail
                            s.Send(mail);
                    return RedirectToAction("Index", "Home");

                }
           
            }

            return View(user);

        }

        public ActionResult Activate(string id)
        {
            MembershipUser u = Membership.GetUser("Bernardo" /*usar id*/);
            u.IsApproved = true;
          
            Membership.UpdateUser(u);
            FormsAuthentication.SetAuthCookie(u.UserName, false);
            return RedirectToAction("Index", "Home");
        }
    }
}
