using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CourseAplication.Model;
using PI.WebGarten;
using PI.WebGarten.HttpContent.Html;

namespace CourseAplication.Views
{
    class RootView : HtmlDoc
    {
        public RootView() : base("FUC index",
                H1(Text("Homepage")),
                    H2(Text("Create a new FUC")),
                    Form("post", "/fucproposal",
                    P(Label("name", "Name: "), InputText("name"))
                   ),
                A(ResolveUri.ForFuc(), "fuclist")
                ){ }
    }
}
