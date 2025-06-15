using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static c_AI練習.AIRequest;
using static c_AI練習.AIResponse;

namespace c_AI練習
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            var requestBody = new AIRequest()
            {
                contents = new List<AIRequest.Content>()
                {
                    new AIRequest.Content()
                    {
                        role = "model",
                        parts = new AIRequest.Part[]{ new AIRequest.Part() { text = "作為一個燈光管理助理，你只能回答有關於燈光調節的問題，你必須要幫助使用者在任何情境下去進行溫度調整。除此之外你都不應該幫助使用者回答任何跟環境與燈光相關的問題" }  }
                    }
                   
                },
                tools = new List<AIRequest.Tool>()
                {
                  new Tool()
                  {
                      functionDeclarations=new List<Functiondeclaration>()
                      {
                          new Functiondeclaration()
                          {
                              name="set_light_values",
                              description = "該函數可以直接設定對應的燈光顏色以及色溫",
                              parameters = new AIRequest.Parameters()
                              {
                                  type="object",
                                  properties=new AIRequest.Properties()
                                  {
                                      brightness=new Brightness()
                                      {
                                          type="integer",
                                          description = "該參數可以調整0-100的色溫，如果是開心/熱情等氛圍參數的設定值就越高，如果是越驚悚/刺激/恐怖的氛圍設定值就越低"
                                      },
                                      color_temp=new Color_Temp()
                                      {
                                          type = "string",
                                           @enum = new[] { "日光", "冷光", "黃光", "漆黑" },
                                           description = "該參數可以調整光線的色溫，可以根據不同的情境設定不同的色溫。例如一般情況下都使用日光,驚悚恐怖使用冷光,讀書或者工作使用黃光,假設要睡覺則採用漆黑"
                                      }
                                  },
                                  required= new[] { "brightness", "color_temp" }
                              }
                          }
                      }
                  }
                }
            };
              
           




            Console.WriteLine("我是一個燈光管理助理，請問您有甚麼需要幫忙的嗎?");
            while (true) 
            {
                string userInput = Console.ReadLine();
                requestBody.contents.Add(new AIRequest.Content()
                {
                    role = "user",
                    parts = new AIRequest.Part[] { new AIRequest.Part() { text = userInput } }
                });
                var json = JsonConvert.SerializeObject(requestBody); // 物件轉字串=> 序列化
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=AIzaSyDNun4eFL0Tcyy-CkUEgqd-zHoeoMIULRg", content);
                string result = await response.Content.ReadAsStringAsync();
                AIResponse aIResponse = JsonConvert.DeserializeObject<AIResponse>(result);
                var toolCall = aIResponse.candidates[0].content.parts[0].functionCall;
                if (toolCall != null)
                {
                    Args args1 = toolCall.args;
                    SetLightAndValue(args1.brightness, args1.colorTemp);
                    Console.WriteLine($"已經幫您燈光設定亮度{args1.brightness}，色溫{args1.colorTemp}");
                    return;
                }
                //Console.WriteLine(JsonConvert.SerializeObject(aIResponse.candidates[0].content.parts[0]));
                requestBody.contents.Add(new AIRequest.Content()
                {
                    role = "model",
                    parts = new AIRequest.Part[] { new AIRequest.Part() { text = aIResponse.candidates[0].content.parts[0].text } }
                });
            }

            



        }

        public static void  SetLightAndValue(int brightness,string colorTemp)
        {
            //  Set the brightness and color temperature of a room light. (mock API).

            //  Args:
            //    brightness: Light level from 0 to 100.Zero is off and 100 is full brightness
            //    color_temp: Color temperature of the light fixture, which can be `daylight`, `cool` or `warm`.

            //  Returns:
            //        A dictionary containing the set brightness and color temperature.
           
        }
    }
}
