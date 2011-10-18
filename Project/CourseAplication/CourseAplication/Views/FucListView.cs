using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CourseAplication.Model;
using PI.WebGarten;
using PI.WebGarten.HttpContent.Html;

namespace CourseAplication.Views
{
    class FucListView : HtmlDoc
    {
        public FucListView(IEnumerable<Fuc> fuclist)
            : base("FUC index",
                A(ResolveUri.ForRoot(), "Home"),
                H1(Text("Fuc List")),
                Ul(
                    fuclist.Select(fuc => Li(A(ResolveUri.For(fuc), fuc.Name))).ToArray()
                   )
                   ) { }
    }
}
