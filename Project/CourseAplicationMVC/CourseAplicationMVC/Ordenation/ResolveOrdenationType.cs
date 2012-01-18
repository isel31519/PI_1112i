using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseAplicationMVC.Ordenation
{
    public class ResolveOrdenationType
    {

        public static OrderType ResolveType(string s)
        {
            if (s != null)
            {
                if (s.CompareTo("dsc") == 0) return OrderType.Descending;

            }
            return OrderType.Ascending;
        }

        public enum OrderType
        {
            Ascending, Descending
        }
    }
}