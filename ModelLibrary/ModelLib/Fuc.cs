using System.Collections.Generic;

namespace CourseAplicationLib
{
    public class Fuc
    {
        public Fuc() {}

        public Fuc(string name, string acr, bool req, string sem, string prereq, double ects)
        {
            Name = name;
            Acr = acr;
            IsRequired = req;
            Semesters = sem;
            Prerequisites = prereq;
            Ects = ects;
        }

        public string Name { get; set; }
        public string Acr { get; set; }
        public bool IsRequired { get; set; }
        public string Semesters { get; set; }
        public string Prerequisites { get; set; }
        public double Ects { get; set; }
        public string Objectives { get; set; }
        public string Results { get; set; }
        public string Evaluation { get; set; }
        public string Program { get; set; }
    }
}
