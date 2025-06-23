using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.AI.Tools
{
    public abstract class ATool
    {
        protected string jsonString;
        public ATool(object json)
        {
            jsonString = JsonConvert.SerializeObject(json);
        }
        public abstract object Apply();
    }
}
