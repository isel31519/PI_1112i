using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace PI.WebGarten.MethodBasedCommands
{
    class PrincipalParameterBinder : IParameterBinder
    {
        public Func<RequestInfo, object> TryGetBinder(ParameterInfo pi, HttpCmdAttribute attr)
        {
            if(pi.ParameterType == typeof(HttpListenerRequest))
            {
                return ri => ri.User;
            }
            return null;
        }
    }
}
