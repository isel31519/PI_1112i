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
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Upload(UserProfile model)
        {
            var image = WebImage.GetImageFromRequest();

            if (image != null)
            {
                if (image.Width > 500)
                {
                    image.Resize(500, ((500 * image.Height) / image.Width));
                }

                var filename = Path.GetFileName(image.FileName);
                image.Save(Path.Combine("../TempImages", filename));
                filename = Path.Combine("~/TempImages", filename);

                model.ImageUrl = Url.Content(filename);

            }

            return View("Test", model);
        }
    }
}
