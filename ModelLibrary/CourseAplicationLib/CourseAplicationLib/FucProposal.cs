﻿namespace CourseAplicationLib
{
    public class FucProposal:Fuc
    {
        private int _id;
        private string _userId;
        public FucProposal(string name, string acr, bool req, double ects, string user) 
            : base(name, acr, req,ects)
        {
            _userId = user;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string User
        {
            get { return _userId; }
            set { _userId = value; }
        }

    }
}