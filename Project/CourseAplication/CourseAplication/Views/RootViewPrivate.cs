using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CourseAplication.Model;
using PI.WebGarten.HttpContent.Html;

namespace CourseAplication.Views
{
    class RootViewPrivate : HtmlDoc
    {
        public RootViewPrivate(string username): base("Homepage",
                H1(Text("Homepage")),
                Form("get", "/login",
                    InputSubmit("Login")
                ),
                H1(Text("Fuc Proposal List")),
                Ul(
                    RepositoryLocator.GetPropRep().GetByUser(username).Select(fuc => Li(A(ResolveUri.For(fuc), fuc.Name))).ToArray()
                   ),
                A(ResolveUri.ForFuc(), "Fuc List"), P(),
                A(ResolveUri.ForCreate(), "Create a new FUC")
                ){ }
    }
}
