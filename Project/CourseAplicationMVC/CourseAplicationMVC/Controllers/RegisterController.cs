using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;
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
        public ActionResult Index(User user)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus createStatus;
                Membership.CreateUser(user.Name, user.Pass, user.Email, null, null, false, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    MailMessage mail = new MailMessage("me@mycompany.com", "you@yourcompany.com",
                                                       "this is a test email.", "Some text goes here");
             
                  
                    mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");	//basic authentication
                    mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "my_username_here"); //set your username here
                    mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "super_secret");	//set your password here

                    
                    var s = new SmtpClient("mail.mycompany.com");  //your real server goes here
                    s.Send(mail);

                    Membership.GetUser(user.Name).
                    FormsAuthentication.SetAuthCookie(user.Name, false);
                    return RedirectToAction("Index", "Home");

                }
           
            }

            return View(user);

            /*
            if (!ModelState.IsValid) return View();
            _repoUsers.Add(u);
            return Redirect(string.Format("/{0}", "Home"));*/
        }
    }
}
