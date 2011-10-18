using CourseAplication.Model;
using PI.WebGarten.HttpContent.Html;

namespace CourseAplication.Views
{
    class NewFucProposalView : HtmlDoc
    {
        public NewFucProposalView(FucProposal f)
            : base(f.Name + " Proposal " + f.Id,
              A(ResolveUri.ForRoot(), "Home"),
                  H1(Text("" + f.User)),
                      Form("GET", ResolveUri.For(f) + "/edit",//mudar
                      InputSubmit("Edit")),
                      Form("POST", "accept",
                      InputSubmit("Accept")),
                      Form("POST", "delete",
                      InputSubmit("Refuse")),
                  Ul(
                      Li(Label("name", "Name: "), Text(f.Name)),
                      Li(Label("acr", "Acronym: "), Text(f.Acr)),
                      Li(Label("req", "Required: "), Text(f.IsRequired ? "Mandatory" : "Opcional")),
                      Li(Label("sem", "Semester: "), Text(f.GetSemesters())),
                      Li(Label("prereq", "Prerequisites: "), Text(f.GetPrerequisites())),
                      Li(Label("ects", "ECTS: "), Text(""+f.Ects)),
                      Li(Label("objectives", "Objectives: "), Text(f.GetDescription("Objectives"))),
                      Li(Label("results", "Results: "), Text(f.GetDescription("Results"))),
                      Li(Label("evaluation", "Evaluation: "), Text(f.GetDescription("Evaluation"))),
                      Li(Label("program", "Program: "), Text(f.GetDescription("Program")))
                    )
              ) { }
    }
}