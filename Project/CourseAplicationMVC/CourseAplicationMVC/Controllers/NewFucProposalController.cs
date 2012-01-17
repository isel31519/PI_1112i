using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseAplicationLib;
using CourseAplicationMVC.Filters;

namespace CourseAplicationMVC.Controllers
{
   // [Authorize]
    [AuthenticationFilter]
    public class NewFucProposalController : Controller
    {
        //
        // GET: /NewFucProposal/

        private readonly FucRepository _repo = RepositoryLocator.GetFucRep();
        private readonly ProposalRepository _newPropRepo = RepositoryLocator.GetNewPropRep();

        public ActionResult Index(bool? pagination, int? page, int? itemsnumber, string orderby, string type, bool? partial)
        {

            FucProposal[] array = _newPropRepo.GetAll().ToArray();
            ViewData.Add("totalitems", array.Length == 0 ? 1 : array.Length);

            if (orderby != null && type != null)
            {
                List<FucProposal> list;
                list = type.CompareTo("asc") == 0
                           ? array.OrderBy(n => n.Name).ToList()
                           : array.OrderByDescending(n => n.Name).ToList();

                if (partial.HasValue && partial.Value)
                    return PartialView("PIndex", list);
                return View("IndexAll",  list);

            }


            if (pagination.HasValue && !pagination.Value)
            {
                if (partial.HasValue && partial.Value)
                    return PartialView("PIndex", _newPropRepo.GetAll());
                return View("IndexAll", _newPropRepo.GetAll());
            }


            if (!page.HasValue)
                page = 1;
            if (!itemsnumber.HasValue)
                itemsnumber = 5;

            ViewData.Add("pageprev", page - 1);
            ViewData.Add("pagenext", page + 1);
            ViewData.Add("itemsnumber", itemsnumber);
            int max_elem = Math.Min((int) (page*itemsnumber), array.Length);
            LinkedList<FucProposal> list2 = new LinkedList<FucProposal>();
            if (array.Length == 0){
                if (partial.HasValue && partial.Value)
                    return PartialView("PIndex", list2);
            return View("Index", list2);
             }

            if (page <= 0 || (page - 1) * itemsnumber >= max_elem)
            {
                return HttpNotFound();
            }
            for (int i = (int)((page - 1) * itemsnumber), j = 0; i < max_elem; j++, i++)
            {
                list2.AddLast(array[i]);
            }
            if (partial.HasValue && partial.Value)
                return PartialView("PIndex", list2);
            return View("Index", list2);

           // return RedirectToAction("PIndex", new { @page = 1, @itemsnumber = 5, @partial = false });

        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Detail(int id, bool? partial)
        {
            FucProposal p = _newPropRepo.GetById(id);
            if (p == null) return HttpNotFound("Proposal not found");
            if (!User.IsInRole("coord") && !User.Identity.Name.Equals(p.User)) return RedirectToAction("LogOn", "Account");
            if (partial.HasValue) return PartialView(p);
            return View(p);
        }


        [HttpPost]
        public ActionResult Create(FucProposal fp)
        {

            if (!ModelState.IsValid)
                return View(_newPropRepo.GetById(fp.Idx));
            fp.User = User.Identity.Name;
            _newPropRepo.Add(fp);
            //bruta!!
            return Redirect(string.Format("/{0}/{1}/{2}", "NewFucProposal", "Detail", fp.Idx));
        }

        public ActionResult Edit(int id)
        {
            FucProposal p = _newPropRepo.GetById(id);
            if (p == null) return HttpNotFound("Proposal not found");
            if (!User.Identity.Name.Equals(p.User)) return RedirectToAction("LogOn", "Account");

            return View(_newPropRepo.GetById(id));
        }


        [HttpPost]
        public ActionResult Edit(int id, FucProposal fp)
        {
            /*if (!ModelState.IsValid)
              return View(_repo.GetById(f.Id));*/
            fp.User = User.Identity.Name;
            _newPropRepo.Edit(id, fp);
            
            //bruta
            return Redirect(string.Format("/{0}/{1}/{2}", "NewFucProposal", "Detail", fp.Idx));
        }

        [HttpPost]

        public ActionResult Refuse(int id)
        {
            FucProposal p = _newPropRepo.GetById(id);

            if (!User.IsInRole("coord") && !User.Identity.Name.Equals(p.User)) return RedirectToAction("LogOn", "Account");
            _newPropRepo.Remove(id);
            return Redirect(string.Format("/{0}", "NewFucProposal"));
        }

        [HttpPost]
        [Authorize(Roles = "coord")]
        public ActionResult Accept(int id)
        {
            Fuc f = _newPropRepo.GetById(id);
            _repo.Add(f);
            _newPropRepo.Remove(id);

            return Redirect(string.Format("/{0}/{1}/{2}", "Fuc", "Detail", f.Acr));
        }
    }
}
