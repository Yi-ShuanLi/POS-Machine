using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventHandlearTest
{
    class ContactPerson
    {

        public void Contact(string name)
        {
            Console.WriteLine("建立通話中");
            Thread.Sleep(1000);
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int num = random.Next(0, 2);
            if (num == 0)
            {
                MessageHandler.NotFoundMessage($"Error,無法與{name} 取得聯繫!");
            }
            else
            {
                MessageHandler.NotifyMessage($"Hi,{name} 你今天過得好嗎？");
            }
        }
    }
}
