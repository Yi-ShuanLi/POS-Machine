using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static 合併AI練習.AIRequest;

namespace 合併AI練習.Tools
{
    internal class LightFunctiondeclaration : Functiondeclaration
    {
        public LightFunctiondeclaration()
        {           
            this.name = "Tools.Light.LightTool";
            this.description = "該函數可以直接設定對應的燈光顏色以及色溫";
            this.parameters = new LightParameter();
        }
    }
    internal class LightParameter : AIRequest.Parameters
    {
        private object _properties;
        public override object properties { get => _properties; }
        public LightParameter()
        {
            this.type = "object";
            this.required = new string[] { "brightness", "color_temp" };

            _properties = new
            {
                brightness = new AIRequestArgs("integer", "該參數可以調整0-100的色溫，如果是開心/熱情等氛圍參數的設定值就越高，如果是越驚悚/刺激/恐怖的氛圍設定值就越低"),
                color_temp = new AIRequestArgs("string", "該參數可以調整光線的色溫，可以根據不同的情境設定不同的色溫。例如一般情況下都使用日光,驚悚恐怖使用冷光,讀書或者工作使用黃光,假設要睡覺則採用漆黑", new[] { "日光", "冷光", "黃光", "漆黑" })
            };
        }

    }
}
