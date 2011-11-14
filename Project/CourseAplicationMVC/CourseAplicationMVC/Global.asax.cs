using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
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
            var userrepo = RepositoryLocator.GetUserRep();
            var f = new Fuc("Object Oriented Programming", "POO", true,"2", "PG",6.0)
            {
              Objectives= "Esta unidade curricular introduz os conceitos e vocabulário fundamental da programação orientada por objectos, e da" +
            "programação event-driven, concretizados na linguagem Java.Desenvolve boas práticas de desenho e codificação de forma a produzir " +
            "programas usando classes e objectos.",

            
            Results= "Os estudantes que terminam com sucesso esta unidade curricular serão capazes de:" +
                            "Definir e usar: classes derivadas, classes que representem Estruturas de Dados dinâmicas, e genéricos." +
                            "Exprimir objectivos na forma de algoritmos recursivos." +
                            "Construir aplicações simples usando o paradigma da Programação Orientada por Objectos e interface gráfica." +
                            "Testar e corrigir as aplicações construídas, usando as ferramentas de desenvolvimento adequadas." +
                            "Escrever relatórios onde se justifica a hierarquia de classes e a estrutura de dados usada nas aplicações construídas.",

            Evaluation="Os resultados de aprendizagem (1) , (2) são avaliados individualmente" +
            "através do teste escrito e das fichas realizadas durante o semestre.Durante o acompanhamento dos trabalhos de grupo realizados" +
            "nas aulas práticas são avaliados os resultados de aprendizagem (3) e (4).Os resultados de aprendizagem (3), (5) são avaliados na" +
            "discussão final dos trabalhos de grupo, onde é discutida a qualidade das soluções.",

            Program= "Herança: classes derivadas; classes abstractas; interfaces; ligação dinâmica; polimorfismo." +
            "Tratamento de excepções. Estruturas de dados dinâmicas e genéricos: vectores; listas; iteradores. Algoritmos recursivos." +
            "Ficheiros de texto e binários. Introdução à interface gráfica: programação event-driven; listeners; layout managers;" +
            "Model-View-Controller."
            };
            

            repo.Add(f);
            f = new Fuc("Programming", "PG", true,"1", null,6.0)
            {
                 Objectives="Esta unidade curricular representa para a maioria dos alunos um primeiro contacto com a" +
            "programação, que se pretende motivador sem descurar o formalismo e o rigor. São introduzidos conceitos e vocabulário " +
            "fundamental da programação e, em particular, da programação baseada em objectos, concretizados na linguagem Java.",

            Results=  "Os estudantes que terminam com sucesso esta unidade curricular serão capazes de:" +
                            "Demonstrar o conhecimento sobre os mecanismos básicos da linguagem de programação Java." +
                            "Construir pequenos programas em Java que resolvam problemas simples descritos em linguagem natural." +
                            "Testar e corrigir pequenos programas." +
                            "Escrever relatórios onde se justifica as decisões tomadas nos programas construídos." +
                            "Utilizar ferramentas para desenvolver programas e para elaborar relatórios.",

            Evaluation="Os resultados da aprendizagem (1) e (2) são avaliados individualmente através do teste escrito e das fichas realizadas durante o semestre. Durante o acompanhamento dos trabalhos de grupo realizados nas aulas práticas são avaliados os resultados da aprendizagem (3) e (5).Os resultados da aprendizagem (4) e (5) são avaliados na discussão final dos trabalhos de grupo.",

                 Program = "Conceitos básicos: valores, tipos e variáveis; expressões; instruções de controlo de fluxo. Entrada/Saída de dados. Introdução à programação baseada em objectos. Tipos referência. Construção de novos tipos. Classes: métodos; passagem de parâmetros; membros de instância e de tipo; construtores; encapsulamento. Arrays. Algoritmos de pesquisa e ordenação."
            };
           
            repo.Add(f);

            userrepo.Add(new User("Aimar", "maior", "aimar@benfica.pt", null));
            userrepo.Add(new User("Saviola", "maior", "saviola@benfica.pt", null));
            userrepo.Add(new User("Rui_Costa", "maior", "ruicosta@benfica.pt", "coord"));
            
            Membership.CreateUser("Aimar", "slb.1maior", "aimar@benfica.pt");
            Membership.CreateUser("Saviola", "maior", "saviola@benfica.pt");
            Membership.CreateUser("Rui_Costa", "maior", "ruicosta@benfica.pt");

            Roles.CreateRole("coord");
            Roles.AddUserToRole("Rui_Costa","coord");
        }
    }

}