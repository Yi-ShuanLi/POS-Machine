using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 合併AI練習
{
    public class AIRequestArgs
    {
        public string type { get; set; } 
        public string[] @enum { get; set; }
        public string description { get; set; }
        public Items items { get; set; }

        public AIRequestArgs(string type,string description, string[] @enum = null,Items item = null)
        {
            this.type = type;
            this.description = description;
            this.@enum = @enum;
            this.items = item;  
        }
    }

    public class Items
    {
        public String type { get; set; } 
        public Items(string type)
        {
            this.type = type;
        }
    }
}
