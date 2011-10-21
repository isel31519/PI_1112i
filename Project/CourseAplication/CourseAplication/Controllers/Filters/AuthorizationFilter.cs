using System;
using System.Security.Principal;
using System.Text;
using CourseAplication.Model;
using PI.WebGarten;
using PI.WebGarten.HttpContent.Html;
using PI.WebGarten.Pipeline;

namespace CourseAplication
{
    public class AuthorizationFilter : IHttpFilter
    {
        private readonly string _name;

        private IHttpFilter _nextFilter;

        public AuthorizationFilter(string name)
        {
            this._name = name;
        }

        #region Implementation of IHttpFilter

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public void SetNextFilter(IHttpFilter nextFilter)
        {
            _nextFilter = nextFilter;
        }

        public HttpResponse Process(RequestInfo requestInfo)
        {
            var ctx = requestInfo.Context;
            if (ctx.Request.Url.AbsolutePath.Contains("accept"))
            {
                if (!requestInfo.User.IsInRole("coord")) return new HttpResponse(403, new TextContent("Not Authorized"));     
            }

            return _nextFilter.Process(requestInfo);
        }

        #endregion
    }
}