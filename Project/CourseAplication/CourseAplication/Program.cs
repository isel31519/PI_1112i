using System.Collections.Generic;
using CourseAplication.Controllers;
using CourseAplication.Model;
using PI.WebGarten;
using PI.WebGarten.MethodBasedCommands;

namespace CourseAplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var repo =  RepositoryLocator.GetFucRep();
            Fuc f = new Fuc("Programaçao Orientada a Objectos", "POO", true, 6.0);
            f.Prerequisites = "PG";
            f.Semester = 2;
            f.AddDescription("Objetivos","Esta unidade curricular introduz os conceitos e vocabulário fundamental da programação orientada por objectos, e da"+
            "programação event-driven, concretizados na linguagem Java.Desenvolve boas práticas de desenho e codificação de forma a produzir "+
            "programas usando classes e objectos.") ;

            f.AddDescription("Resultados de aprendizagem", "Os estudantes que terminam com sucesso esta unidade curricular serão capazes de:"+
                            "Definir e usar: classes derivadas, classes que representem Estruturas de Dados dinâmicas, e genéricos."+
                            "Exprimir objectivos na forma de algoritmos recursivos."+
                            "Construir aplicações simples usando o paradigma da Programação Orientada por Objectos e interface gráfica."+
                            "Testar e corrigir as aplicações construídas, usando as ferramentas de desenvolvimento adequadas."+
                            "Escrever relatórios onde se justifica a hierarquia de classes e a estrutura de dados usada nas aplicações construídas.");

            f.AddDescription("Avaliação dos resultados de aprendizagem", "Os resultados de aprendizagem (1) , (2) são avaliados individualmente"+
            "através do teste escrito e das fichas realizadas durante o semestre.Durante o acompanhamento dos trabalhos de grupo realizados"+
            "nas aulas práticas são avaliados os resultados de aprendizagem (3) e (4).Os resultados de aprendizagem (3), (5) são avaliados na"+
            "discussão final dos trabalhos de grupo, onde é discutida a qualidade das soluções.");

            f.AddDescription("Programa resumido", "Herança: classes derivadas; classes abstractas; interfaces; ligação dinâmica; polimorfismo."+
            "Tratamento de excepções. Estruturas de dados dinâmicas e genéricos: vectores; listas; iteradores. Algoritmos recursivos."+ 
            "Ficheiros de texto e binários. Introdução à interface gráfica: programação event-driven; listeners; layout managers;"+ 
            "Model-View-Controller.");
            repo.Add(f);
            //repo.Add(new Fuc;

            var host = new HttpListenerBasedHost("http://localhost:8080/");
            host.Add(DefaultMethodBasedCommandFactory.GetCommandsFor(
                typeof(FucController),
                typeof(RootController)
               ));
            host.OpenAndWaitForever();
        }
    }
}
