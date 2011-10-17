using System.Collections.Generic;

namespace CourseAplication.Model
{
    class FucProposal:Fuc
    {
        private int _id;
        private int _userId;
        public FucProposal(string name, string acr, bool req, double ects, int user) 
            : base(name, acr, req,ects)
        {
            _userId = user;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}
