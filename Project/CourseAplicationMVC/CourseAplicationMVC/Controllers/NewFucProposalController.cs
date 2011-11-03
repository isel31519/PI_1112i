using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseAplicationLib;

namespace CourseAplicationMVC.Controllers
{
    public class NewFucProposalController : Controller
    {
        //
        // GET: /NewFucProposal/

        private readonly ProposalRepository _repo = RepositoryLocator.GetNewPropRep();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View(_repo.GetAll());
        }

    }
}
