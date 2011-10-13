using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseAplication.Model
{
    class ResolveUri
    {
        public static string For(Fuc f)
        {
            return string.Format("{0}/{1}", ForFuc(), f.Acr);
        }

        public static string ForFuc()
        {
            return "/fuc";
        }
    }
}
