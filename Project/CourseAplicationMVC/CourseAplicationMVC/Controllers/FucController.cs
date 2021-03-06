﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Xml;
using CourseAplicationLib;
using CourseAplicationMVC.Ordenation;


namespace CourseAplicationMVC.Controllers
{
    public class FucController : Controller
    {
        private readonly FucRepository _repo = RepositoryLocator.GetFucRep();
        private readonly ProposalRepository _proprepo = RepositoryLocator.GetPropRep();
        private readonly IDictionary<string, IOrdenation<Fuc>> _sort = new Dictionary<string, IOrdenation<Fuc>>();
        // GET: /Fuc/

        public ActionResult Pagination()
        {
                return PartialView();      
        }


        public ActionResult Index(bool? pagination, int? page, int? itemsnumber, string orderby, string type, bool? partial)
        {
            Fuc[] array = _repo.GetAll().ToArray();
            ViewData.Add("totalitems", array.Length == 0 ? 1 : array.Length);

            if(orderby!=null &&type!=null )
            {

                _sort.Add("Fuc Name", new NameOrdenation(array));
                _sort.Add("Fuc Acr", new AcronymOrdenation(array));
                IOrdenation<Fuc> sort;
                _sort.TryGetValue(orderby, out sort);
                IEnumerable<Fuc> list;

                list = sort.Order(ResolveOrdenationType.ResolveType(type));

                if (partial.HasValue && partial.Value)
                    return PartialView("PIndex", list);
                return View("IndexAll", list);

            }

            if (pagination.HasValue && !pagination.Value)
            {
                if (partial.HasValue && partial.Value)
                  return PartialView("PIndex", _repo.GetAll());
                return View("IndexAll", _repo.GetPartialFucs(10));//5
            }

            if(!page.HasValue)
                 page = 1;
            if (!itemsnumber.HasValue)
                itemsnumber = 5;
            ViewData.Add("page", page);
            ViewData.Add("pageprev", page - 1);
            ViewData.Add("pagenext", page + 1);
            ViewData.Add("itemsnumber", itemsnumber);

            if (page <= 0 || (array.Length != 0 && page > Math.Ceiling(((double)array.Length / (double)itemsnumber))) /*!list2.Any()(page - 1) * itemsnumber >= max_elem*/)
            {
                return HttpNotFound();
            }

            IEnumerable<Fuc> list2 = _repo.GetPaged(page, itemsnumber);

            if (partial.HasValue && partial.Value)
                return PartialView("PIndex", list2);
            return View("Index", list2);
            
        }
        public ActionResult Detail(string id, bool? partial)
        {
            Fuc f = _repo.GetByAcr(id);
            if (f == null)
                return HttpNotFound();
            if (partial.HasValue) return PartialView(f);
            return View(f);
        }
          
        [Authorize]
        public ActionResult Edit(string id)
        {
            //id=acronimo da fuc a ser alterada
            Fuc f = _repo.GetByAcr(id);
            if (f == null)
                return HttpNotFound();

            return View(f);
        }

        [HttpPost]
        [Authorize]
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
            return Redirect(string.Format("/{0}/{1}/{2}", "Proposal", "Detail", f.Idx));
        }

    }
}
