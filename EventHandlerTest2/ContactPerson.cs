using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventHandlerTest2
{
    class ContactPerson
    {
        public void Contact(string name)
        {
            Console.WriteLine("建立通話中");
            Thread.Sleep(1000);
            Random random = new Random(Guid.NewGuid().GetHashCode());                                 
            MessageHandler.NotifyMessage($"Hi,{name} 你今天過得好嗎？");            
        }
    }
}
