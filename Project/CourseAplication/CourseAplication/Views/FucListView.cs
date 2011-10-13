using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PI.WebGarten;
using PI.WebGarten.HttpContent.Html;

namespace CourseAplication.Views
{
    class FucListView : HtmlDoc
    {
        public FucListView(string t, params IWritable[] content)
            : base("FUC index",
                H1(Text("Lista de FUC")),
                Ul(
                    t.Select(td => Li(A(ResolveUri.For(td), td.Description))).ToArray()
                   )
                   ) { }
    }
}
