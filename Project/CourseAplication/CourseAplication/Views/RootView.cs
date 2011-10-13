using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PI.WebGarten;
using PI.WebGarten.HttpContent.Html;

namespace CourseAplication.Views
{
    class RootView : HtmlDoc
    {
        public RootView(string t, params IWritable[] content) : base("FUC index",
                H1(Text("Homepage")),
                Ul(
                    t.Select(td => Li(A(ResolveUri.For(td), td.Description))).ToArray()
                   ),
                    H2(Text("Create a new FUC")),
                    Form("post", "/fuclist",
                    Label("desc", "Description: "), InputText("desc")
                   )
                ){ }
    }
}
