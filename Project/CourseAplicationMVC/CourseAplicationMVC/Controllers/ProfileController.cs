using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Profile;
using System.Web.Security;
using CourseAplicationLib;
using CourseAplicationMVC.Models;

namespace CourseAplicationMVC.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/

     /*   public ActionResult Index()//lista de membros?
        {
            return View();
        }*/

        public ActionResult Detail(string id)
        {
            MembershipUser user = Membership.GetUser(User.Identity.Name, true);
           if (user == null) return HttpNotFound("User not Found");
           return View((UserProfile)ProfileBase.Create(user.UserName));
        }

        [HttpPost]
        public ActionResult Detail(string id, UserProfileModel u)
        {
            if(!ModelState.IsValid)
                return View();

            UserProfile profile = (UserProfile)ProfileBase.Create(id);
            profile.FirstName = u.FirstName;
            profile.LastName = u.LastName;
            profile.Email = u.Email;
            profile.Save();

            MembershipUser user;

            if( (user = Membership.GetUser(id))!=null)
            {
                user.Email = u.Email;
                Membership.UpdateUser(user);
            }
            
            return View(profile);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Upload(UserProfileModel model)
        {
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase postedFile = Request.Files[fileName];
                postedFile.SaveAs(Server.MapPath("~/Uploads/") + Path.GetFileName(postedFile.FileName));
            }
            return null;
            //return View();
        }
    }
}
