using System.Collections.Generic;

namespace CourseAplicationLib
{
    public class Fuc
    {
        private string _name;
        private string _acr;
        private readonly bool _required;
        private List<ushort> _semester;
        private List<string> _prerequisites;
        private double _ects;
        private Dictionary<string,string> _description;


        public Fuc(string name, string acr, bool req, double ects)
        {
            _name = name;
            _acr = acr;
            _required = req;
            _semester = new List<ushort>();
            _prerequisites = new List<string>();
            _ects = ects;
            _description = new Dictionary<string,string>();
        }

        private string Get<T>(List<T> a)
        {
            string s = "";

            foreach (T val in a)
                s += val + " ";

            return s;
        }

        public string GetSemesters()
        {
            return Get<ushort>(_semester);
        }

        public ushort Semester
        {
             set{ _semester.Add(value); }
        }

        public string GetPrerequisites()
        {
            return Get<string>(_prerequisites);
        }

        public string Prerequisites
        {
            set { _prerequisites.Add(value); }
        }
        
        public void AddDescription(string title,string desc)
        {
            if(desc!=null)
            _description.Add(title,desc); 
        }
        public string GetDescription(string title)
        {
            string desc;
             _description.TryGetValue(title,out desc);
            return desc;
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
        public bool IsRequired
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
