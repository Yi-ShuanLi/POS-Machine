using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static 合併AI練習.AIRequest;

namespace 合併AI練習.Tools.BookingMeeting
{
    internal class BookingMeetingFunctiondeclaration : Functiondeclaration
    {
        public BookingMeetingFunctiondeclaration()
        {
            this.name = "Tools.BookingMeeting.BookingMeetingTool";
            this.description = "安排在指定時間和日期與指定與會者舉行會議。";
            this.parameters = new BookingMeetingParameter();
        }
    }
    internal class BookingMeetingParameter : AIRequest.Parameters
    {
        private object _properties;
        public override object properties { get => _properties; }
        public BookingMeetingParameter()
        {
            this.type = "object";
            this.required = new string[] { "attendees", "date", "time", "topic" };

            _properties = new
            {
                attendees = new AIRequestArgs("array", "與會人員名單", null, new Items("string")),
                date = new AIRequestArgs("string", "會議日期（例如2024-07-29）或(例如2024/7/29)或(例如2024/07/29)或(例如2024-7-29)，共三部分，第一部分為西元年，第二部分為月份範圍只允許1-12，第三部分為日期範圍部分只允許1-31，你可以透過自己判斷判斷使用者輸入是存在的日期，聊天當下的過去日期不能預約"),
                time = new AIRequestArgs("string", "會議時間（例如15:00）"),
                topic = new AIRequestArgs("string", "會議的主題或話題")
            };
        }
    }
}
