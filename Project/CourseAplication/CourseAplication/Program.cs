using CourseAplication.Controllers;
using CourseAplication.Model;
using PI.WebGarten;
using PI.WebGarten.MethodBasedCommands;

namespace CourseAplication
{
    class Program
    {
        internal class Loader
        {
            public static void FucRepoFill()
            {
                var repo = RepositoryLocator.GetFucRep();
                var f = new Fuc("Object Oriented Programming", "POO", true, 6.0)
                            {
                                Prerequisites = "PG",
                                Semester = 2
                            };
                f.AddDescription("Objectives", "Esta unidade curricular introduz os conceitos e vocabulário fundamental da programação orientada por objectos, e da" +
                "programação event-driven, concretizados na linguagem Java.Desenvolve boas práticas de desenho e codificação de forma a produzir " +
                "programas usando classes e objectos.");

                f.AddDescription("Results", "Os estudantes que terminam com sucesso esta unidade curricular serão capazes de:" +
                                "Definir e usar: classes derivadas, classes que representem Estruturas de Dados dinâmicas, e genéricos." +
                                "Exprimir objectivos na forma de algoritmos recursivos." +
                                "Construir aplicações simples usando o paradigma da Programação Orientada por Objectos e interface gráfica." +
                                "Testar e corrigir as aplicações construídas, usando as ferramentas de desenvolvimento adequadas." +
                                "Escrever relatórios onde se justifica a hierarquia de classes e a estrutura de dados usada nas aplicações construídas.");

                f.AddDescription("Evaluation", "Os resultados de aprendizagem (1) , (2) são avaliados individualmente" +
                "através do teste escrito e das fichas realizadas durante o semestre.Durante o acompanhamento dos trabalhos de grupo realizados" +
                "nas aulas práticas são avaliados os resultados de aprendizagem (3) e (4).Os resultados de aprendizagem (3), (5) são avaliados na" +
                "discussão final dos trabalhos de grupo, onde é discutida a qualidade das soluções.");

                f.AddDescription("Program", "Herança: classes derivadas; classes abstractas; interfaces; ligação dinâmica; polimorfismo." +
                "Tratamento de excepções. Estruturas de dados dinâmicas e genéricos: vectores; listas; iteradores. Algoritmos recursivos." +
                "Ficheiros de texto e binários. Introdução à interface gráfica: programação event-driven; listeners; layout managers;" +
                "Model-View-Controller.");
                repo.Add(f);
                f = new Fuc("Programming", "PG", true, 6.0)
                {
                    Semester = 1
                };
                f.AddDescription("Objectives", "Esta unidade curricular representa para a maioria dos alunos um primeiro contacto com a"+ 
                "programação, que se pretende motivador sem descurar o formalismo e o rigor. São introduzidos conceitos e vocabulário "+
                "fundamental da programação e, em particular, da programação baseada em objectos, concretizados na linguagem Java.");

                f.AddDescription("Results", "Os estudantes que terminam com sucesso esta unidade curricular serão capazes de:"+
                                "Demonstrar o conhecimento sobre os mecanismos básicos da linguagem de programação Java."+
                                "Construir pequenos programas em Java que resolvam problemas simples descritos em linguagem natural."+
                                "Testar e corrigir pequenos programas."+
                                "Escrever relatórios onde se justifica as decisões tomadas nos programas construídos."+
                                "Utilizar ferramentas para desenvolver programas e para elaborar relatórios.");

                f.AddDescription("Evaluation", "Os resultados da aprendizagem (1) e (2) são avaliados individualmente através do teste escrito e das fichas realizadas durante o semestre. Durante o acompanhamento dos trabalhos de grupo realizados nas aulas práticas são avaliados os resultados da aprendizagem (3) e (5).Os resultados da aprendizagem (4) e (5) são avaliados na discussão final dos trabalhos de grupo.");

                f.AddDescription("Program", "Conceitos básicos: valores, tipos e variáveis; expressões; instruções de controlo de fluxo. Entrada/Saída de dados. Introdução à programação baseada em objectos. Tipos referência. Construção de novos tipos. Classes: métodos; passagem de parâmetros; membros de instância e de tipo; construtores; encapsulamento. Arrays. Algoritmos de pesquisa e ordenação.");
                repo.Add(f);
            }
        }

        static void Main(string[] args)
        {
            Loader.FucRepoFill();
            var host = new HttpListenerBasedHost("http://localhost:8080/");
            host.Pipeline.AddFilterFirst("Authentication", typeof(AuthenticationFilter));
            host.Add(DefaultMethodBasedCommandFactory.GetCommandsFor(
                typeof(FucController),
                typeof(RootController), typeof(EditFucController), typeof(CreateFucProposalController)
               ));
            host.OpenAndWaitForever();
        }
    }
}
