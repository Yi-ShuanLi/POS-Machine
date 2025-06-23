using MySqlX.XDevAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using 合併AI練習.Tools;
using static 合併AI練習.AIRequest;

namespace 合併AI練習
{
    internal class AIAgent
    {
        AIRequest aiRequestBody;
        public AIAgent() {

            aiRequestBody = new AIRequest()
            {
                contents = new List<AIRequest.Content>()
                {
                    new AIRequest.Content()
                    {
                        role = "model",
                        parts = new AIRequest.Part[]{ new AIRequest.Part() { text = "作為一個管理AI助理，你會需要根據使用者上下文中的對話需求，並提供對應的工具，回傳給使用者讓他呼叫使用。你只能回答工具內擁有的東西，除此之外你都不應該幫助使用者回答你的職責以外的事情" }  }
                    },

                },
                tools = new List<AIRequest.Tool>()
            };
            var declarationTool = new Tool();
            var types = Assembly.GetExecutingAssembly().DefinedTypes;
            var tools = types.Where(x => x.BaseType == typeof(Functiondeclaration)).Select(x => (Functiondeclaration)Activator.CreateInstance(x)).ToList();
            declarationTool.AddRange(tools);
            aiRequestBody.tools.Add(declarationTool);

        }
       
        
        public void AddTools(Functiondeclaration functiondeclaration)
        {
            this.aiRequestBody.tools[0].Add(functiondeclaration);
        }


        public  async Task<AIResponse> SendMessage(string userInput)
        {
            var client = new HttpClient();               
                aiRequestBody.contents.Add(new AIRequest.Content()
                {
                    role = "user",
                    parts = new AIRequest.Part[] { new AIRequest.Part() { text = userInput } }
                });

                var json = JsonConvert.SerializeObject(aiRequestBody); // 物件轉字串=> 序列化
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=AIzaSyDNun4eFL0Tcyy-CkUEgqd-zHoeoMIULRg", content);
                string result = await response.Content.ReadAsStringAsync();
                AIResponse aIResponse = JsonConvert.DeserializeObject<AIResponse>(result);                        
                aiRequestBody.contents.Add(new AIRequest.Content()
                {
                    role = "model",
                    parts = new AIRequest.Part[] { new AIRequest.Part() { text = aIResponse.candidates[0].content.parts[0].text } }
                });
                return aIResponse;
            
        }
    }
}
