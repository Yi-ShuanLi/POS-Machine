using EventHandlerTest2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EventHandlerTest2
{
    class FindPerson
    {
        public void Find(string name)
        {
            Console.WriteLine("搜索中...請稍候");
            Thread.Sleep(1000);
            Console.WriteLine("找到目標對象，正在嘗試進行聯繫");
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int num = random.Next(0, 3);
            if (num == 0) //間接聯繫
            {
                ContactPerson contactPerson = new ContactPerson();
                contactPerson.Contact(name);
            }
            else if (num == 1) //直接聯繫
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
