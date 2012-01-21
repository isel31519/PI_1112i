
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;
using System.Web.Security;

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

        public ActionResult Detail(string id, bool? partial)
        {
            MembershipUser user = Membership.GetUser(User.Identity.Name, true);
           if (user == null) return HttpNotFound("User not Found");
           if (partial.HasValue) return PartialView((UserProfile)ProfileBase.Create(user.UserName));
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
            profile.Image = u.Image;
            profile.Save();

            MembershipUser user;

            if( (user = Membership.GetUser(id))!=null)
            {
                user.Email = u.Email;
                Membership.UpdateUser(user);
            }
            
            return View(profile);
        }
/*
        public ActionResult Index()
        {
            return View();
        }*/

        public ActionResult Upload()
        {
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase postedFile = Request.Files[fileName];
                postedFile.SaveAs(Server.MapPath("~/Uploads/") + Path.GetFileName(postedFile.FileName));

                UserProfile profile = (UserProfile)ProfileBase.Create(User.Identity.Name);
                profile.Image = Server.MapPath("~/Uploads/") + Path.GetFileName(postedFile.FileName);
                profile.Save();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
