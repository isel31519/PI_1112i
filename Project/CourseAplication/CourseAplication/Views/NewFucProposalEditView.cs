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
                     Form("post", ResolveUri.ForNew(f),
                     P(Label("name", "Name: "), P(InputTextWithContent("name", f.Name))),
                     P(Label("acr", "Acronym: "), P(InputTextWithContent("acr", f.Acr))),
                     P(Label("req", "Required: "), f.IsRequired ? InputCheckBox("req", "yes") : InputCheckBox("req", "no")),
                     P(Label("sem", "Semester: "), P(InputTextWithContent("sem", f.GetSemesters()))),
                     P(Label("prereq", "Prerequisites: "), P(InputTextWithContent("prereq", f.GetPrerequisites()))),
                     P(Label("ects", "ECTS: "), P(InputTextWithContent("ects", "" + f.Ects))),
                     P(Label("objectives", "Objectives: "), P(InputTextArea("objectives", "5", "30"))),
                     P(Label("results", "Results: "), P(InputTextArea("results", "5", "30"))),
                     P(Label("evaluation", "Evaluation: "), P(InputTextArea("evaluation", "5", "30"))),
                     P(Label("program", "Program: "), P(InputTextArea("program", "5", "30"))),
                     InputSubmit("Submit")
                    )
             ) { }
    }
}