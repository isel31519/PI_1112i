using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PI.WebGarten;
using PI.WebGarten.HttpContent.Html;
using CourseAplication.Model;

namespace CourseAplication.Views
{
    class FucView : HtmlDoc
    {
        public FucView(Fuc f)
            : base(f.Name,
                A(ResolveUri.ForRoot(), "Home"),
                H1(Text(f.Name)),
                Form("GET", ResolveUri.For(f)+"/edit",
                InputSubmit("Edit")),
                Ul(
                Li(Label("name", "Name: "), Text(f.Name)),
                Li(Label("acr", "Acronym: "), Text(f.Acr)),
                Li(Label("req", "Required: "), Text(f.IsRequired ? "Mandatory" : "Opcional")),
                Li(Label("sem", "Semester: "), Text(f.GetSemesters())),
                GenerateLinks(f.GetPrerequisites()),
                Li(Label("ects", "ECTS: "), Text("" + f.Ects)),
                Li(Label("objectives", "Objectives: "), Text(f.GetDescription("Objetives"))),
                Li(Label("results", "Results: "), Text(f.GetDescription("Results"))),
                Li(Label("evaluation", "Evaluation: "), Text(f.GetDescription("Evaluation"))),
                Li(Label("program", "Program: "), Text(f.GetDescription("Program")))
                )
                ) { }

        private static IWritable GenerateLinks(string getPrerequisites)
        {
            HtmlElem e = new HtmlElem("li");
            e.WithContent(Label("prereq", "Prerequisites: "));
            foreach (string s in getPrerequisites.Split())
            {
                e.WithContent(A(ResolveUri.ForFuc() + "/" + s.Trim(), s.Trim()));

            }
            return e;
        }
    }
}