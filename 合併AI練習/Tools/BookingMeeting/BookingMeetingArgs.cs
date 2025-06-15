using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace 合併AI練習.Tools.BookingMeeting
{
    internal class BookingMeetingArgs
    {
        public String topic { get; set; }
        public string[] attendees {  get; set; }

        public string date {  get; set; }

        public string time { get; set; }


    }
}
