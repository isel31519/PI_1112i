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
                    //Label("name", "Name: "), InputText("name"), P(),

                    P(Label("name", "Name: "), InputText("name"))

                    /*Label("acr", "Acr: "), InputText("acr"), P(),
                    Label("req", "Required: "), InputCheckBox("req"), P(),
                    Label("sem", "Semester: "), InputText("sem"), P(),
                    Label("prereq", "Prerequisites: "), InputText("prereq"), P(),
                    Label("objectives", "Objectives: "), InputText("objectives"), P(),
                    Label("results", "Results: "), InputText("results"), P(),
                    Label("evaluation", "Evaluation: "), InputText("evaluation"), P(),
                    Label("program", "Program: "), InputText("program"), P(),
                    InputSubmit("Submit")*/
                   )
            ) { }
    }
}
