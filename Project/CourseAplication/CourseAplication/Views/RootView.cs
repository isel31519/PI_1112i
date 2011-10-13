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
        public RootView() : base("FUC index",
                H1(Text("Homepage")),
                    H2(Text("Create a new FUC")),
                    Form("post", "/fucproposal",
                    Label("desc", "Description: "), InputText("desc")
                   )
                ){ }
    }
}
