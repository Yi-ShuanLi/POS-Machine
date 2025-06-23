using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.AI.Common
{
    internal class AIRequestArgs
    {
        public string type { get; set; }
        public string[] @enum { get; set; }
        public string description { get; set; }
       

        public AIRequestArgs(string type, string description, string[] @enum = null)
        {
            this.type = type;
            this.description = description;
            this.@enum = @enum;
        }
    }
   
}
