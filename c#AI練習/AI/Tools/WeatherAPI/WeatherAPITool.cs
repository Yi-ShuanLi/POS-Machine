using AI練習.AI.Tools;
using AI練習.AI.Tools.WeatherAPI;
using c_AI練習.AI.Tools.WeatherAPI;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace AI練習.AI.Tools.WeatherAPI
{
    internal class WeatherAPITool : ATool
    {
        public WeatherAPIArgs args;
        public WeatherAPITool(object json) : base(json)
        {
            args = JsonConvert.DeserializeObject<WeatherAPIArgs>(jsonString);
        }

        public override object Apply()
        {
            //建立 WebRequest 並指定目標的 uri
            WebRequest request = WebRequest.Create($"http://api.weatherapi.com/v1/current.json?key=aa16e6b5c9f442e7aec103636252706&q={args.location_name}&aqi=no");
            // 使用 HttpWebRequest.Create 實際上也是呼叫 WebRequest.Create
            //WebRequest request = HttpWebRequest.Create("http://jsonplaceholder.typicode.com/posts");
            //指定 request 使用的 http verb
            request.Method = "GET";
            //使用 GetResponse 方法將 request 送出，如果不是用 using 包覆，請記得手動 close WebResponse 物件，避免連線持續被佔用而無法送出新的 request
            var httpResponse = (HttpWebResponse)request.GetResponse();
            //使用 GetResponseStream 方法從 server 回應中取得資料，stream 必需被關閉
            //使用 stream.close 就可以直接關閉 WebResponse 及 stream，但同時使用 using 或是關閉兩者並不會造成錯誤，養成習慣遇到其他情境時就比較不會出錯
            var streamReader = new StreamReader(httpResponse.GetResponseStream());

            var result = streamReader.ReadToEnd();
            WeatherAPIResponse weatherAPIResponse = JsonConvert.DeserializeObject<WeatherAPIResponse>(result);
            Console.WriteLine($"氣溫:{weatherAPIResponse.current.temp_c}度");
            Console.WriteLine($"天氣:{weatherAPIResponse.current.condition.text}");

            return null;
        }
    }
}
