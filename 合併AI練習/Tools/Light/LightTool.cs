using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 合併AI練習.Tools.Light
{
    internal class LightTool : ATool
    {
        LightResArgs args = null;
        public LightTool(object json) : base(json)
        {
            args = JsonConvert.DeserializeObject<LightResArgs>(jsonString);
        }

        public override void Apply()
        {
            //TODO:用建構元傳入json去轉成實際類別做使用
            Console.WriteLine($"已經幫您設定完成， 色溫: {args.brightness} 顏色: {args.color_temp}");
        }
    }
}
