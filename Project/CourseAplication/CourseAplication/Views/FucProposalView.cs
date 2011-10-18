using CourseAplication.Model;
using PI.WebGarten.HttpContent.Html;

namespace CourseAplication.Views
{
    class FucProposalView:HtmlDoc
    {
        public FucProposalView(FucProposal f) : base(f.Name+" Proposal "+f.Id,
            A(ResolveUri.ForRoot(), "Home"),
                H1(Text(""+f.User)),
                    Form("GET", "edit",
                    InputSubmit("Edit")),
                    Form("POST", "accept",
                    InputSubmit("Accept")),
                    Form("POST", "delete",
                    InputSubmit("Refuse")),
                Ul(
                    Li(Label("name", "Name: "), Text(f.Name)),
                    Li(Label("acr", "Acronym: "), Text(f.Acr)),
                    Li(Label("req", "Required: "), Text(f.IsRequired?"Obrigatória":"Opcional")),
                    Li(Label("sem", "Semester: "), Text(f.GetSemesters())),
                    Li(Label("prereq", "Prerequisites: "), Text(f.GetPrerequisites())),
                    Li(Label("objectives", "Objectives: "), Text(f.GetDescription("Objectivos"))),
                    Li(Label("results", "Results: "), Text(f.GetDescription("Resultados de aprendizagem"))),
                    Li(Label("evaluation", "Evaluation: "), Text(f.GetDescription("Avaliação dos resultados de aprendizagem"))),
                    Li(Label("program", "Program: "), Text(f.GetDescription("Programa resumido")))
                  )
            ) { }
    }
}