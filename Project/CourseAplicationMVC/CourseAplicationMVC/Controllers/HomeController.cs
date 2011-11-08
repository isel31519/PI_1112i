using System.Runtime.Remoting.Contexts;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;
using CourseAplicationLib;

namespace CourseAplicationMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserRepository _repo = RepositoryLocator.GetUserRep();
        
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User u)
        {
            if (ModelState.IsValid)
            {
                User user;
                if (!(user = _repo.GetById(u.Name)).Match(u.Pass)) return View();
                {
                    var principal = new GenericPrincipal(new GenericIdentity(u.Name), new string[] { user.Role });
                    HttpContext.User = principal;

                    FormsAuthentication.SetAuthCookie(u.Name, false);
                    return RedirectToAction("Index", "Home");
                }
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


        public ActionResult IndexUser(string user)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return View();

             return RedirectToAction("Login", "Home");
        }
    }
}
