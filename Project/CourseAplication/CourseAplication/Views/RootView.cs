using System;
using System.Collections;
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
                    Form("post", "/",//vai para um sitio
                    InputFieldset(InputLegend("Login"), 
                        Label("name", "Name: "), InputText("name", "text"), P(),
                        Label("pwd", "Password: "), InputText("pwd", "password")),
                    InputSubmit("Submit")
                   ),
                H1(Text("Fuc Proposal List")),
                Ul(
                    RepositoryLocator.GetPropRep().GetAll().Select(fuc => Li(A(ResolveUri.For(fuc), fuc.Name))).ToArray()
                   ),
                H1(Text("New Fuc Proposal List")),
                Ul(
                    RepositoryLocator.GetNewPropRep().GetAll().Select(fuc => Li(A(ResolveUri.For(fuc), fuc.Name))).ToArray()
                   ),
                A(ResolveUri.ForFuc(), "fuclist")
                ){ }
    }
}