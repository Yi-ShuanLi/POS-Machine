using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI練習Booking會議
{
    internal class AIRequest
    {     
        public List <Content> contents { get; set; }
        public List <Tool> tools { get; set; }
        

        public class Content
        {
            public string role { get; set; }
            public Part[] parts { get; set; }
        }

        public class Part
        {
            public string text { get; set; }
        }

        public class Tool
        {
            public List<Functiondeclaration> functionDeclarations { get; set; }
        }

        public class Functiondeclaration
        {
            public string name { get; set; }
            public string description { get; set; }
            public Parameters parameters { get; set; }
        }

        public class Parameters
        {
            public string type { get; set; }
            public Properties properties { get; set; }
            public string[] required { get; set; }
        }

        public class Properties
        {
            public Attendees attendees { get; set; }
            public Date date { get; set; }
            public Time time { get; set; }
            public Topic topic { get; set; }
        }

        public class Attendees
        {
            public string type { get; set; }
            public Items items { get; set; }
            public string description { get; set; }
        }

        public class Items
        {
            public string type { get; set; }
        }

        public class Date
        {
            public string type { get; set; }
            public string description { get; set; }
        }

        public class Time
        {
            public string type { get; set; }
            public string description { get; set; }
        }

        public class Topic
        {
            public string type { get; set; }
            public string description { get; set; }
        }

    }
}
