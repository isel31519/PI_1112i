using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseAplicationLib;
using CourseAplicationMVC.Filters;

namespace CourseAplicationMVC.Controllers
{
    public class FucController : Controller
    {
        private readonly FucRepository _repo = RepositoryLocator.GetFucRep();
        private readonly ProposalRepository _proprepo = RepositoryLocator.GetPropRep();
        // GET: /Fuc/

        public ActionResult Index(bool? pagination)
        {
            if (pagination.HasValue && !pagination.Value)
              return View("IndexAll",_repo.GetAll());
            return RedirectToAction("PIndex", new { @page = 1, @itemsnumber = 5, @partial = false });
        }

        public ActionResult Detail(string id)
        {
            Fuc f = _repo.GetByAcr(id);
            if (f == null)
                return HttpNotFound();
            return View(f);
        }

        public ActionResult PIndex(int page, int itemsnumber, bool? partial)
        {
            ViewData.Add("pageprev", page - 1);
            ViewData.Add("pagenext", page + 1);
            ViewData.Add("itemsnumber", itemsnumber);
            Fuc[] array = _repo.GetAll().ToArray();
            ViewData.Add("totalitems", array.Length == 0 ? 1 : array.Length);
            int max_elem = Math.Min(page * itemsnumber, array.Length);
            LinkedList<Fuc> list = new LinkedList<Fuc>();
            if (array.Length == 0) return View("Index", list);
            if (page <= 0 || (page - 1) * itemsnumber >= max_elem)
            {
                return HttpNotFound();
            }
            for (int i = (page - 1) * itemsnumber, j = 0; i < max_elem; j++, i++)
            {
                list.AddLast(array[i]);
                // array2[j] = array[i];
            }
            if (partial.HasValue && partial.Value)
                return PartialView("PIndex", list);
            return View("Index", list);
        }
        [AuthenticationFilter]
       // [Authorize]
        public ActionResult Edit(string id)
        {
            //id=acronimo da fuc a ser alterada
            Fuc f = _repo.GetByAcr(id);
            if (f == null)
                return HttpNotFound();

            return View(f);
        }

        [HttpPost]
        [AuthenticationFilter]
       // [Authorize]
        public ActionResult Edit(string id, FucProposal f)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 404;
                return View(_repo.GetByAcr(id));
            }
            f.User = User.Identity.Name;
            f.OriginalAcr = id;
            _proprepo.Add(f);
            //bruta!!
            return Redirect(string.Format("/{0}/{1}/{2}", "Proposal", "Detail", f.Idx));
        }

    }
}
