using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseAplicationMVC.Models;

namespace CourseAplicationMVC.Ordenation
{
    public interface IOrdenation<T>
    {
        IEnumerable<T> Order(ResolveOrdenationType.OrderType type);
    }
 

 
}