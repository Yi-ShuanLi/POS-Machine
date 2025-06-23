using Newtonsoft.Json;
using POS點餐機.AI.Tools.DetermineBestChoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.AI.Tools.BestChoice
{
    internal class BestChoiceTool : ATool
    {

            BestChoiceArgs args = null;
            public BestChoiceTool(object json) : base(json)
            {
                args = JsonConvert.DeserializeObject<BestChoiceArgs>(jsonString);
            }

            public override object Apply()
            {
                //TODO:用建構元傳入json去轉成實際類別做使用
                return args;
            }
        
    }
}
