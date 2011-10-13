using System.Collections.Generic;

namespace CourseAplication.Model
{
    public class Fuc
    {
        private string _name;
        private string _acr;
        private readonly bool _required;
        private List<ushort> _semester;
        private List<Fuc> _prerequisites;
        private double _ects;
        private List<string> _description;


        public Fuc(string name, string acr, bool req, List<ushort> sems, List<Fuc> reqmts, double ects, List<string> desc)
        {
            _name = name;
            _acr = acr;
            _required = req;
            _semester = sems;
            _prerequisites = reqmts;
            _ects = ects;
            _description = desc;
        }

        public string Name
        {
            get{return _name;}
            set{_name = value;}
        }
        public string Acr
        {
            get { return _acr; }
            set { _acr = value; }
        }
        public bool IsValid
        {
            get { return _required; }

        }
        public double Ects
        {
            get { return _ects; }
            set { _ects = value; }
        }
    }
}
