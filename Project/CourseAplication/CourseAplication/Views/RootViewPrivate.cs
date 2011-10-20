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
        public RootViewPrivate() : base("FUC index",
                H1(Text("Homepage")),
                A(ResolveUri.ForFuc(), "Fuc List"), P(),
                A(ResolveUri.ForCreate(), "Create a new FUC")
                ){ }
    }
}
