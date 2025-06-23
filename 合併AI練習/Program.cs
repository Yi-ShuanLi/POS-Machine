using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using 合併AI練習.Tools;
using 合併AI練習.Tools.BookingMeeting;
using static 合併AI練習.AIRequest;

namespace 合併AI練習
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("我是一位智能會議預約助理及燈光調整師，請問您有甚麼需要幫忙的嗎?");

            AgentHandler agentHandler = new AgentHandler();
            AgentResult result = null;
            while (true) {

                string input = Console.ReadLine();
                result = await agentHandler.RunAgentAsync(input);
                if (result.CanRunTool)
                    break;
                Console.WriteLine(result.Message);  
            }

            result.Tool.Apply();
            Console.WriteLine("執行完成");

            Console.ReadKey();  

            #region 舊版本的code
            //var client = new HttpClient();

            //var requestBody = new AIRequest()
            //{
            //    contents = new List<AIRequest.Content>()
            //    {
            //        new AIRequest.Content()
            //        {
            //            role = "model",
            //            parts = new AIRequest.Part[]{ new AIRequest.Part() { text = "作為一個管理AI助理，你會需要根據使用者的需求提供燈光設定及會議預約，你只能回答有關於燈光調節與會議預約的問題，你必須要幫助使用者在任何情境下去進行溫度調整或協助會議預約。除此之外你都不應該幫助使用者回答你的職責以外的事情" }  }
            //        },


            //    },
            //    tools = new List<AIRequest.Tool>()
            //};
            //var declarationTool = new Tool();
            //var types = Assembly.GetExecutingAssembly().DefinedTypes;
            //var tools = types.Where(x => x.BaseType == typeof(Functiondeclaration)).Select(x => (Functiondeclaration)Activator.CreateInstance(x)).ToList();
            //declarationTool.AddRange(tools);
            //requestBody.tools.Add(declarationTool);
            //Console.WriteLine("我是一位智能會議預約助理及燈光調整師，請問您有甚麼需要幫忙的嗎?");
            //while (true)
            //{
            //    string userInput = Console.ReadLine();
            //    requestBody.contents.Add(new AIRequest.Content()
            //    {
            //        role = "user",
            //        parts = new AIRequest.Part[] { new AIRequest.Part() { text = userInput } }
            //    });

            //    var json = JsonConvert.SerializeObject(requestBody); // 物件轉字串=> 序列化
            //    var content = new StringContent(json, Encoding.UTF8, "application/json");
            //    var response = await client.PostAsync("https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=AIzaSyDNun4eFL0Tcyy-CkUEgqd-zHoeoMIULRg", content);
            //    string result = await response.Content.ReadAsStringAsync();
            //    AIResponse aIResponse = JsonConvert.DeserializeObject<AIResponse>(result);
            //    var toolCall = aIResponse.candidates[0].content.parts[0].functionCall;
            //    if (toolCall != null)
            //    {
            //        Type type = Type.GetType("合併AI練習." + toolCall.name);
            //        ATool tool = (ATool)Activator.CreateInstance(type, new object[] { toolCall.args });
            //        tool.Apply();
            //        return;
            //    }
            //    //Console.WriteLine(JsonConvert.SerializeObject(aIResponse.candidates[0].content.parts[0]));
            //    Console.WriteLine(aIResponse.candidates[0].content.parts[0].text);
            //    requestBody.contents.Add(new AIRequest.Content()
            //    {
            //        role = "model",
            //        parts = new AIRequest.Part[] { new AIRequest.Part() { text = aIResponse.candidates[0].content.parts[0].text } }
            //    });
            //    //Console.WriteLine(json);
            //}
            #endregion
        }

    }


}
