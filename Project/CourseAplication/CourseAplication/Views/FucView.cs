using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PI.WebGarten;
using PI.WebGarten.HttpContent.Html;
using CourseAplication.Model;

namespace CourseAplication.Views
{
    class FucView : HtmlDoc
    {
        public FucView(Fuc f):base("FUC",
                H1(Text(f.Name)),
                Ul(
                    Li(Text(f.Acr)),
                    Li(Text(Convert.ToString(f.Ects)))
                  ),
                A(ResolveUri.ForFuc(),"fuc")
                ){}
    }
}
