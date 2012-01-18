using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseAplicationLib;

namespace CourseAplicationMVC.Ordenation
{
    public abstract class FucOrdenation : IOrdenation<Fuc>
    {
        protected Fuc[] array;

        public FucOrdenation(Fuc[] ar)
        {
            array = ar;
        }

        public abstract IEnumerable<Fuc> Order(ResolveOrdenationType.OrderType type);

    }

    public class NameOrdenation : FucOrdenation
    {
        public NameOrdenation(Fuc[] ar) : base(ar)
        {
        }

        public override IEnumerable<Fuc> Order(ResolveOrdenationType.OrderType type)
        {
          return type == ResolveOrdenationType.OrderType.Ascending ? array.OrderBy(n => n.Name).ToList() :
                  array.OrderByDescending(n => n.Name).ToList();
         }
    }

    public class AcronymOrdenation : FucOrdenation
    {
        public AcronymOrdenation(Fuc[] ar)
            : base(ar)
        {
        }
        public override IEnumerable<Fuc> Order(ResolveOrdenationType.OrderType type)
        {
            return type == ResolveOrdenationType.OrderType.Ascending ? array.OrderBy(n => n.Acr).ToList() :
                 array.OrderByDescending(n => n.Acr).ToList();
        }
    }
}