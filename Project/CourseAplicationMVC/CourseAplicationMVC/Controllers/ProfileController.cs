using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;
using System.Web.Security;
using CourseAplicationLib;

namespace CourseAplicationMVC.Controllers
{
    [Authorize]
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
           MembershipUser user = Membership.GetUser(id);
            if (user == null) return HttpNotFound("User not Found");
            User u=new User(user.UserName,null,user.Email,null);
            return View(user);
        }
    }
}
