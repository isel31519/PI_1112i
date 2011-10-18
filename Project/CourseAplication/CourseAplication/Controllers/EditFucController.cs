using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CourseAplication.Model;

namespace CourseAplication.Controllers
{
    class EditFucController
    {
        private readonly ProposalRepository _repo;

        public EditFucController()
        {
            _repo = RepositoryLocator.GetPropRep();
        }


    }
}
