using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 合併AI練習.Tools
{
    internal abstract class ATool
    {
        protected string jsonString;
        public ATool(object json) {
            jsonString = JsonConvert.SerializeObject(json);
        }
        public abstract void Apply();
    }
}
