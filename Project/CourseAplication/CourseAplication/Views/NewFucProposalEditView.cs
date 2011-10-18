using CourseAplication.Model;
using PI.WebGarten.HttpContent.Html;

namespace CourseAplication.Views
{
    class NewFucProposalEditView : HtmlDoc
    {
        public NewFucProposalEditView(FucProposal f)
            : base("Edit Proposal Form",
             A(ResolveUri.ForRoot(), "Home"),
                 H1(Text("Edit Proposal Form")),
                     Form("post", ResolveUri.For(f) + "/edit",//fica no mesmo sitio
                     P(Label("name", "Name: "), P(InputText("name", f.Name))),
                     P(Label("acr", "Acronym: "), P(InputText("acr", f.Acr))),
                     P(Label("req", "Required: "), InputCheckBox("req")),
                     P(Label("sem", "Semester: "), P(InputText("sem", f.GetSemesters()))),
                     P(Label("prereq", "Prerequisites: "), P(InputText("prereq", f.GetPrerequisites()))),
                     P(Label("objectives", "Objectives: "), P(InputTextArea("objectives", "5", "30"))),
                     P(Label("results", "Results: "), P(InputTextArea("results", "5", "30"))),
                     P(Label("evaluation", "Evaluation: "), P(InputTextArea("evaluation", "5", "30"))),
                     P(Label("program", "Program: "), P(InputTextArea("program", "5", "30"))),
                     InputSubmit("Submit")
                    )
             ) { }
    }
}