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
        public RootView(string t, params IWritable[] content) : base(t, content)
        {
            t = "FUC Manager Root";

        }
    }
}
