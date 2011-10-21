using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using CourseAplication.Model;

namespace CourseAplication.Controllers
{
    class ControllerUtils
    {
        public static FucProposal CreateFuc(IPrincipal user,IEnumerable<KeyValuePair<string, string>> content)
        {
            var acro = content.Where(p => p.Key == "acr").Select(p => p.Value).FirstOrDefault();
            var name = content.Where(p => p.Key == "name").Select(p => p.Value).FirstOrDefault();
            var required = content.Where(p => p.Key == "req").Select(p => p.Value).FirstOrDefault();
            var semester = content.Where(p => p.Key == "sem").Select(p => p.Value).FirstOrDefault();
            var prerequisites = content.Where(p => p.Key == "prereq").Select(p => p.Value).FirstOrDefault();
            var ects = content.Where(p => p.Key == "ects").Select(p => p.Value).FirstOrDefault();
            var objectives = content.Where(p => p.Key == "objectives").Select(p => p.Value).FirstOrDefault();
            var results = content.Where(p => p.Key == "results").Select(p => p.Value).FirstOrDefault();
            var evaluation = content.Where(p => p.Key == "evaluation").Select(p => p.Value).FirstOrDefault();
            var program = content.Where(p => p.Key == "program").Select(p => p.Value).FirstOrDefault();

            if (!user.Identity.IsAuthenticated) return null;

            var userid= user.Identity.Name;
            

            if (acro == null || name == null || semester == null || ects == null/* ||
                objectives == null || results == null || evaluation == null || program == null*/ || userid == null)
            {
                return null;
            }


            var fuc = new FucProposal(name, acro, required!=null ? true : false, Convert.ToDouble(ects), userid);


            foreach (var sem in semester.Split(' '))
            {
                if (sem != "")
                    fuc.Semester = Convert.ToUInt16(sem);
            }


            foreach (var pre in prerequisites.Split(' '))
            {
                if (pre != "")
                    fuc.Prerequisites = pre;
            }



            fuc.AddDescription("Objectives", objectives);
            fuc.AddDescription("Results", results);
            fuc.AddDescription("Evaluation", evaluation);
            fuc.AddDescription("Program", program);

            return fuc;
        }


    }
}
