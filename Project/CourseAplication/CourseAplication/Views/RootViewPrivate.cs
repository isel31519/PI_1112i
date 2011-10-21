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
                H1(Text(username)),
                Form("get", "/logout",
                    InputSubmit("Logout")
                ),
                H1(Text("Fuc Proposal List")),
                Ul(
                    RepositoryLocator.GetPropRep().GetByUser(username).Select(fuc => Li(A(ResolveUri.For(fuc), fuc.Name))).ToArray()
                   ),
                H1(Text("New Fuc Proposal List")),
                Ul(
                    RepositoryLocator.GetNewPropRep().GetByUser(username).Select(fuc => Li(A(ResolveUri.ForNew(fuc), fuc.Name))).ToArray()
                   ),
                A(ResolveUri.ForFuc(), "Fuc List"), P(),
                A(ResolveUri.ForCreate(), "Create a new FUC")
                ){ }
    }
}
