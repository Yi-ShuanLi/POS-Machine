using AI練習.AI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AI練習.AI.Common.AIRequest;

namespace AI練習.AI.Tools.WeatherAPI
{
    internal class WeatherAPIFunctiondeclaration : Functiondeclaration
    {
        public WeatherAPIFunctiondeclaration()
        {
            this.name = "AI.Tools.WeatherAPI.WeatherAPITool";
            this.description = "此函數為取得欲知道的地點天氣資訊";
            this.parameters = new WeatherAPIParameter();
        }
    }
    internal class WeatherAPIParameter : AIRequest.Parameters
    {
        private object _properties;
        public override object properties { get => _properties; }
        public WeatherAPIParameter()
        {
            this.type = "object";
            this.required = new string[] { "location_name" };

            _properties = new
            {
                location_name = new AIRequestArgs("string", "台灣的縣市地點，請用英文回答", new[] { 
                                            "Taipei",
                                            "New Taipei",
                                            "Taoyuan",
                                            "Taichung",
                                            "Tainan",
                                            "Kaohsiung",
                                            "Keelung",
                                            "Hsinchu",
                                            "Chiayi",
                                            "Hsinchu County",
                                            "Miaoli County",
                                            "Changhua County",
                                            "Nantou County",
                                            "Yunlin County",
                                            "Chiayi County",
                                            "Pingtung County",
                                            "Yilan County",
                                            "Hualien County",
                                            "Taitung County",
                                            "Penghu County",
                                            "Kinmen County",
                                            "Lienchiang County" }),
               
            };
        }

    }
}
