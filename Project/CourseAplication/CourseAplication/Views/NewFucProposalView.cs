using CourseAplication.Model;
using PI.WebGarten.HttpContent.Html;

namespace CourseAplication.Views
{
    class NewFucProposalView : HtmlDoc
    {
        public NewFucProposalView(FucProposal fuclist)
            : base("FUC index",
                A(ResolveUri.ForRoot(), "Home"),
                H1(Text("Lista de FUC")),
                Ul(
                    fuclist.Select(fuc => Li(A(ResolveUri.For(fuc), fuc.Name))).ToArray()
                   )
                   ) { }
    }
}
