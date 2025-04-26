using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EventHandlearTest
{
    class MessageHandler
    {
        public static EventHandler<string> OnReceiveMessage; // 可以接收到回應的情況
        
        public static void NotifyMessage(String message)
        {
            Console.ReadLine();
            
            Console.WriteLine("進入NotifyMessage");
            
            OnReceiveMessage?.Invoke(null, message);
            Console.Read();
        }

        public static EventHandler<string> OnPersonNotFound; // 完全找不到人的情況
        public static void NotFoundMessage(String message)
        {
            OnPersonNotFound?.Invoke(null, message);
        }

        public static EventHandler<string> OnExpandSearch; // 需要擴大搜索的情況
        public static void ExpandSearchMessage(String message)
        {

            OnExpandSearch?.Invoke(null, message);
        }
    }
}
