using System.Collections.Generic;
using System.Linq;
using CourseAplication.Model;
using CourseAplication.Views;
using PI.WebGarten;
using PI.WebGarten.HttpContent.Html;
using PI.WebGarten.MethodBasedCommands;

namespace CourseAplication.Controllers
{
    class LoginController
    {

        [HttpCmd(HttpMethod.Get, "/login")]
        public HttpResponse GetLogin()
        {

            return new HttpResponse(200, new HtmlDoc("Login",
                                        HtmlBase.Form("post", "/login",
                                                HtmlBase.InputFieldset(HtmlBase.InputLegend("Login"),
                                                HtmlBase.Label("name", "Name: "), HtmlBase.InputText("name"), HtmlBase.P(),
                                                HtmlBase.Label("pwd", "Password: "), HtmlBase.InputPassword("pwd")),
                                                HtmlBase.InputSubmit("Submit")
                                                )
                                        )

                   );
        }
      
        [HttpCmd(HttpMethod.Post, "/login")]
        public HttpResponse PostLogin(IEnumerable<KeyValuePair<string, string>> content)
        { 

            string user = content.Where(p => p.Key == "name").Select(p => p.Value).FirstOrDefault();
            string passwd = content.Where(p => p.Key == "pwd").Select(p => p.Value).FirstOrDefault();


            User u = RepositoryLocator.GetUserRep().GetById(user);
            if (u == null || !u.Match(passwd)) return new HttpResponse(401, new TextContent("Not Authorized")).WithHeader("WWW-Authenticate", "Basic realm=\"Private Area\"");
            //falta colocar headers!
            return new HttpResponse(200, new RootView()).WithHeader("WWW-Authenticate", "Basic realm=\"Private Area\"");;
        }
    }
}
