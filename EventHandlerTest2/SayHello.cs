using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventHandlerTest2
{
    class SayHello
    {
        public void SayHi(string name)
        {
            Console.WriteLine("連線中..請稍候");
            Thread.Sleep(1000);
            Console.WriteLine("此聯絡人不在附近，開始擴大搜索...");
            Console.WriteLine("啟動搜索...");
            Thread.Sleep(3000);

            Random random = new Random(Guid.NewGuid().GetHashCode());
            int num = random.Next(0,3);
            if (num == 0)
            {
                MessageHandler.ExpandSearchMessage(name);
            }
            else if (num == 1)
            {
                MessageHandler.NotifyMessage($"Hi,{name} 你今天過得好嗎？");
            }
            else
            {
                MessageHandler.NotFoundMessage($"Error,無法與{name} 取得聯繫!");
            }


        }
    }
}
