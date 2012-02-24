using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml;
using CourseAplicationLib;

namespace CourseAplicationMVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            Loader.FucRepoFill();


        }
    }

    internal class Loader
    {
        public static void FucRepoFill()
        {
            var repo = RepositoryLocator.GetFucRep();
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FUC.xml");
            XmlTextReader reader = new XmlTextReader(path);
            string[] fuc = new string[10]; // name, acr, mand, sem, ects, objectives, results, evaluation, programa;
            Fuc f;
            int i = 0;
            while (reader.Read())
            {


                switch (reader.NodeType)
                {
                    case XmlNodeType.EndElement: // O nó é um elemento.
                        if (reader.Name.CompareTo("fuc") == 0)
                        {
                            f = new Fuc(fuc[0], fuc[1], fuc[2].CompareTo("true") == 0, fuc[3],
                                        fuc[4].CompareTo("null") == 0 ? null : fuc[4],
                                        Double.Parse(fuc[5]))
                                    {
                                        Objectives = fuc[6],
                                        Results = fuc[7],
                                        Evaluation = fuc[8],
                                        Program = fuc[9]
                                    };
                            repo.Add(f);
                            i = 0;
                        }
                        break;
                    case XmlNodeType.Text: //Apresente o texto em cada elemento.
                        fuc[i++] = reader.Value;
                        break;
                }
            }


        }

    }

}