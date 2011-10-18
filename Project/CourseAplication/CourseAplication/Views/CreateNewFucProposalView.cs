using CourseAplication.Model;
using PI.WebGarten.HttpContent.Html;

namespace CourseAplication.Views
{
    class CreateNewFucProposalView : HtmlDoc
    {
        public CreateNewFucProposalView()
            : base("Create Fuc Form",
            A(ResolveUri.ForRoot(), "Home"),
                H1(Text("Create Fuc Proposal Form")),
                    Form("post", "/create",//fica no mesmo sitio
                    P(Label("name", "Name: "), P(InputText("name"))),
                    P(Label("acr", "Acronym: "), P(InputText("acr"))),
                    P(Label("req", "Required: "), InputCheckBox("req", "no")),
                    P(Label("sem", "Semester: "), P(InputText("sem"))),
                    P(Label("prereq", "Prerequisites: "), P(InputText("prereq"))),
                    P(Label("ects", "ECTS: "), P(InputText("ects"))),
                    P(Label("objectives", "Objectives: "), P(InputTextArea("objectives", "5", "30"))),
                    P(Label("results", "Results: "), P(InputTextArea("results", "5", "30"))),
                    P(Label("evaluation", "Evaluation: "), P(InputTextArea("evaluation", "5", "30"))),
                    P(Label("program", "Program: "), P(InputTextArea("program", "5", "30"))),
                    InputSubmit("Submit")
                   )
            ) { }
    }
}
