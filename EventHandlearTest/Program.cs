using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventHandlearTest
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        private static event EventHandler handler;
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //監聽、接收消息
            handler += Onhandler;
            handler += Onhandler2;

            //觸發、推送消息
            handler.Invoke(null, null);
            // handler.Invoke(null,null);
            // handler.Invoke(null,null);
            // handler.Invoke(null,null);
            // handler.Invoke(null,null);
            // handler.Invoke(null,null);
            // handler.Invoke(null,null);
            // handler.Invoke(null,null);
            Console.Read();
        }
        private static void Onhandler(object sender, EventArgs e)
        {
            Console.WriteLine("事件1被觸發了！");
        }
        private static void Onhandler2(object sender, EventArgs e)
        {
            Console.WriteLine("事件2被觸發了！");
        }
    
    }
}
