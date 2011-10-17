using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using CourseAplication.Model;
using PI.WebGarten.HttpContent.Html;

namespace CourseAplication.Views
{
    class EditFormView : HtmlDoc
    {
        public EditFormView(Fuc f) : base("Edit Form",
            A(ResolveUri.ForRoot(), "Home"),
                H1(Text("Edid Form")),
                    Form("post", "/"+f.Acr+"/prop/",//falta acrescentar o id
                    P(Label("name", "Name: "), P(InputText("name"))),
                    P(Label("acr", "Acronym: "), P(InputText("acr"))),
                    P(Label("req", "Required: "), InputCheckBox("req")),
                    P(Label("sem", "Semester: "), P(InputText("sem"))),
                    P(Label("prereq", "Prerequisites: "), P(InputText("prereq"))),
                    P(Label("objectives", "Objectives: "), P(InputTextArea("objectives", "5", "30"))),
                    P(Label("results", "Results: "), P(InputTextArea("results", "5", "30"))),
                    P(Label("evaluation", "Evaluation: "), P(InputTextArea("evaluation", "5", "30"))),
                    P(Label("program", "Program: "), P(InputTextArea("program", "5", "30"))),
                    InputSubmit("Submit")
                   )
            ) { }
    }
}
