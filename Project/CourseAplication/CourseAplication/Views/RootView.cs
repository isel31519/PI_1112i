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
        public RootView() : base("Homepage",
                H1(Text("Homepage")),
                    Form("get", "/login",
                    InputSubmit("Login")
                   ),
                A(ResolveUri.ForFuc(), "Fuc List"), P()
                ){ }
    }
}