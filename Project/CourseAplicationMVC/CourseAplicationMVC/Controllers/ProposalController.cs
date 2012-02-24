using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseAplicationLib;
using CourseAplicationMVC.Ordenation;

namespace CourseAplicationMVC.Controllers
{
    [Authorize]
    public class ProposalController : Controller
{

        private readonly ProposalRepository _proprepo = RepositoryLocator.GetPropRep();
        private readonly FucRepository _repo = RepositoryLocator.GetFucRep();
        private readonly IDictionary<string, IOrdenation<FucProposal>> _sort = new Dictionary<string, IOrdenation<FucProposal>>();
        //
        // GET: /Proposal/

        public ActionResult Index(bool? pagination, int? page, int? itemsnumber, string orderby, string type, bool? partial)
        {
              FucProposal[] array = _proprepo.GetAll().ToArray();
                ViewData.Add("totalitems", array.Length == 0 ? 1 : array.Length);

            if (orderby != null && type != null)
            {
              

                _sort.Add("Proposal", new ProposalNameOrdenation(array));
                _sort.Add("Acronym", new ProposalAcronymOrdenation(array));
                _sort.Add("Creator", new ProposalCreatorOrdenation(array));

                IOrdenation<FucProposal> sort;
                _sort.TryGetValue(orderby, out sort);

                IEnumerable<FucProposal> list;
                list = sort.Order(ResolveOrdenationType.ResolveType(type));

                if (partial.HasValue && partial.Value)
                    return PartialView("PIndex", list);
                return View("IndexAll", list);

            }


            if (pagination.HasValue && !pagination.Value)
            {
                if (partial.HasValue && partial.Value)
                    return PartialView("PIndex", _proprepo.GetAll());
                return View("IndexAll", _proprepo.GetAll());
            }


            if (!page.HasValue)
                page = 1;
            if (!itemsnumber.HasValue)
                itemsnumber = 5;

            ViewData.Add("page", page);
            ViewData.Add("pageprev", page - 1);
            ViewData.Add("pagenext", page + 1);
            ViewData.Add("itemsnumber", itemsnumber);

            if (page <= 0 || (array.Length !=0 && page > Math.Ceiling(((double)array.Length / (double)itemsnumber))) /*!list2.Any()(page - 1) * itemsnumber >= max_elem*/)
            {
                return HttpNotFound();
            }

            IEnumerable<FucProposal> list2 = _proprepo.GetPaged(page, itemsnumber);

            if (partial.HasValue && partial.Value)
                return PartialView("PIndex", list2);
            return View("Index", list2);

            /*
            int max_elem = Math.Min((int)(page * itemsnumber), array.Length);
            LinkedList<FucProposal> list2 = new LinkedList<FucProposal>();
            if (array.Length == 0)
            {
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
            return View("Index", list2);*/


        }
        public ActionResult PIndex(int page, int itemsnumber, bool? partial)
        {
            ViewData.Add("pageprev", page - 1);
            ViewData.Add("pagenext", page + 1);
            ViewData.Add("itemsnumber", itemsnumber);
            FucProposal[] array = _proprepo.GetAll().ToArray();
            ViewData.Add("totalitems", array.Length == 0 ? 1 : array.Length);
            int max_elem = Math.Min(page * itemsnumber, array.Length);
            LinkedList<FucProposal> list = new LinkedList<FucProposal>();
            if (array.Length == 0) return View("Index", list);
            if (page == 0 || (page - 1) * itemsnumber >= max_elem)
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
        public ActionResult Detail(int id, bool? partial)
        {
           
            FucProposal p = _proprepo.GetById(id);
            if (p == null) return HttpNotFound("Proposal not found");
            if (!User.IsInRole("coord") && !User.Identity.Name.Equals(p.User))
                return RedirectToAction("LogOn", "Account");

            if (partial.HasValue) return PartialView(p);
            return View(_proprepo.GetById(id));
        }

        public ActionResult Edit(int id, string oracr)
        {
            FucProposal p = _proprepo.GetById(id);
            if (p == null) return HttpNotFound("Proposal not found");
            if (!User.IsInRole("coord") && !User.Identity.Name.Equals(p.User))
                return RedirectToAction("LogOn", "Account");

            return View(_proprepo.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, string oracr, FucProposal f)
        {
            
            if (!ModelState.IsValid)
                return View(_proprepo.GetById(id));
            f.OriginalAcr = oracr;
            f.User = User.Identity.Name;
            _proprepo.Edit(id, f);
            //bruta!!
            return Redirect(string.Format("/{0}/{1}/{2}", "Proposal","Detail", f.Idx));
        }
        [HttpPost]

        public ActionResult Refuse(int id)
        {
            FucProposal p = _proprepo.GetById(id);

            if (!User.IsInRole("coord") && !User.Identity.Name.Equals(p.User)) return RedirectToAction("LogOn", "Account");
            _proprepo.Remove(id);
            return Redirect(string.Format("/{0}", "Proposal"));
        }

        [HttpPost]
        [Authorize(Roles = "coord")]
        public ActionResult Accept(int id, string oracr)
        {
            FucProposal f = _proprepo.GetById(id);
            _repo.Remove(f.OriginalAcr);
            _repo.Add(f);
            _proprepo.Remove(id);

            return Redirect(string.Format("/{0}/{1}/{2}", "Fuc", "Detail", f.Acr));
        }
    }
}
