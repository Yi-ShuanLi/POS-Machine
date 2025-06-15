using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 合併AI練習.Tools.Light;

namespace 合併AI練習.Tools.BookingMeeting
{
    internal class BookingMeetingTool : ATool
    {
        BookingMeetingArgs args;
        public BookingMeetingTool(object json) : base(json)
        {
            args= JsonConvert.DeserializeObject<BookingMeetingArgs>(jsonString); ;
        }

        public override void Apply()
        {
            Console.Write($"已經幫您成功建立一個會議預約日期:{args.date}，時間:{args.time}，主旨:{args.topic}，與會人員:");
            for (int i = 0; i < args.attendees.Length; i++)
            {
                Console.Write(args.attendees[i]+" ");
            }
            Console.WriteLine("。");
        }
        
    }
}
