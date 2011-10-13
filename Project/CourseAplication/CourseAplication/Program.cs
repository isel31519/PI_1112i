using System.Collections.Generic;
using CourseAplication.Model;
using PI.WebGarten;
using PI.WebGarten.MethodBasedCommands;

namespace CourseAplication
{
    class Program
    {
        static void Main(string[] args)
        {
           
            var repo = new FucRepository();
            repo.Add(new Fuc("Programaçao Orientada a Objectos", "POO", true, 2, "Pg", 6.0, "Tipo cenas"));
            //repo.Add(new Fuc;

            var host = new HttpListenerBasedHost("http://localhost:8080/");
            host.Add(DefaultMethodBasedCommandFactory.GetCommandsFor(
                typeof(FucContrller),
                typeof(ToDoController)));
            host.OpenAndWaitForever();
        }
    }
}
