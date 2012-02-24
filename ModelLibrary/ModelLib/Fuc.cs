using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public Fuc(string name, string acr)
        {
            Name = name;
            Acr = acr;
        }
        [Required(ErrorMessage = "Name is a required field")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Acr is a required field")]
        public string Acr { get; set; }
        public bool IsRequired { get; set; }
        [Required]
        public string Semesters { get; set; }
        public string Prerequisites { get; set; }
        [Required]
        public double Ects { get; set; }
        public string Objectives { get; set; }
        public string Results { get; set; }
        public string Evaluation { get; set; }
        public string Program { get; set; }
    }
}
