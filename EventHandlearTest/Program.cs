using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EventHandlearTest
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>

        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());


            MessageHandler.OnReceiveMessage += ShowMessage;
            MessageHandler.OnExpandSearch += ExpandSearch;
            MessageHandler.OnPersonNotFound += NotFound;
            Console.WriteLine("hihi");
            String name = "Jenny";
            SayHello sayHello = new SayHello();
            sayHello.SayHi(name);
            Console.Read(); // 讓 console 程式不會閃退

            #region zzz

            //監聽、接收消息
            //handler += Onhandler;
            //handler += Onhandler2;

            //觸發、推送消息
            //handler.Invoke(null, "Hello");
            // handler.Invoke(null,null);
            // handler.Invoke(null,null);
            // handler.Invoke(null,null);
            // handler.Invoke(null,null);
            // handler.Invoke(null,null);
            // handler.Invoke(null,null);
            // handler.Invoke(null,null);
            #endregion

        }
        //private static void Onhandler(object sender, string message)
        //{
        //    Console.WriteLine("事件1被觸發了！"+message);
        //}
        //private static void Onhandler2(object sender, string message)
        //{
        //    Console.WriteLine("事件2被觸發了！" + message);
        //}
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
