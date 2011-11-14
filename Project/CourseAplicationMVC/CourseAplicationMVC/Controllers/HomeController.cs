using System.Runtime.Remoting.Contexts;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;
using CourseAplicationLib;

namespace CourseAplicationMVC.Controllers
{
    public class HomeController : Controller
    {
      
        
        // GET: /Home/
        public ActionResult Index()
        {

            return View();
        }

        
    }
}
