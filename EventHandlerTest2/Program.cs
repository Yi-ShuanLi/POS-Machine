using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHandlerTest2
{
    class Program
    {
        static void Main(string[] args)
        {
            MessageHandler.OnReceiveMessage += ShowMessage;
            MessageHandler.OnExpandSearch += ExpandSearch;
            MessageHandler.OnPersonNotFound += NotFound;
            
            String name = "Jenny";
            SayHello sayHello = new SayHello();
            sayHello.SayHi(name);
            Console.Read(); // 讓 console 程式不會閃退

        }
        private static void ShowMessage(object sender, string message)
        {
            Console.WriteLine(message);
            Console.Read();
        }
        private static void ExpandSearch(object sender, string message)
        {
            FindPerson findPerson = new FindPerson();
            findPerson.Find(message);
            Console.Read();
        }
        private static void NotFound(object sender, string message)
        {
            Console.WriteLine(message);
            Console.Read();
        }
    }
}
