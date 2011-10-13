using System.Collections.Generic;

namespace CourseAplication.Model
{
    class FucProposal:Fuc
    {
        private int _id;
        private int _userId;
        public FucProposal(string name, string acr, bool req, List<ushort> sems, List<Fuc> reqmts, double ects, List<string> desc,int user) 
            : base(name, acr, req, sems, reqmts, ects, desc)
        {
            _userId = user;
        }
    }
}
