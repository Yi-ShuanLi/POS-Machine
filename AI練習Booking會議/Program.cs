using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static AI練習Booking會議.AIRequest;
using static AI練習Booking會議.AIResponse;

namespace AI練習Booking會議
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
                        parts = new AIRequest.Part[]{ new AIRequest.Part() { text = "你的工作是負責幫我預約會議時間。你有權限取得當前日期時間，去推測要預約的日期，只能預約當下往後推的未來時間，參與者部分你要自己判斷空格或,或連在一起的情況並自己去提取人名，表現格式人名與人名間要逗號表示，你只能回答與會議預約有關的問題。" }  }
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
                              name="schedule_meeting",
                              description = "安排在指定時間和日期與指定與會者舉行會議。",
                              parameters = new AIRequest.Parameters()
                              {
                                  type="object",
                                  properties=new AIRequest.Properties()
                                  {
                                      attendees=new Attendees()
                                      {
                                          type="array",
                                          items=new AIRequest.Items()
                                          {
                                            type="string"
                                          },
                                          description="與會人員名單"
                                      },
                                      date=new Date()
                                      {
                                          type="string",
                                          description="會議日期（例如2024-07-29）或(例如2024/7/29)或(例如2024/07/29)或(例如2024-7-29)，共三部分，第一部分為西元年，第二部分為月份範圍只允許1-12，第三部分為日期範圍部分只允許1-31，你可以透過自己判斷判斷使用者輸入是存在的日期，聊天當下的過去日期不能預約"
                                      },
                                      time=new Time()
                                      {
                                          type="string",
                                          description="會議時間（例如15:00）"
                                      },
                                      topic=new Topic()
                                      {
                                          type="string",
                                          description="會議的主題或話題"
                                      }
                                  },
                                  required=new[] {"attendees", "date", "time", "topic" }
                              }
                          }
                      }
                  }
                }
            };
            Console.WriteLine("我是一位智能會議預約助理，請問您有甚麼需要幫忙的嗎?");
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
                    SettingAMetting(args1.attendees,args1.date,args1.time,args1.topic);                   
                    Console.Write($"已經幫您成功建立一個會議預約日期:{args1.date}，時間:{args1.time}，主旨:{args1.topic}，與會人員:");
                    for(int i=0;i< args1.attendees.Length; i++)
                    {
                        Console.Write(args1.attendees[i]);
                    }
                    Console.WriteLine("。");
                    return;
                }
                //Console.WriteLine(JsonConvert.SerializeObject(aIResponse.candidates[0].content.parts[0]));
                Console.WriteLine( aIResponse.candidates[0].content.parts[0].text );
                requestBody.contents.Add(new AIRequest.Content()
                {
                    role = "model",
                    parts = new AIRequest.Part[] { new AIRequest.Part() { text = aIResponse.candidates[0].content.parts[0].text } }
                });
            }
        }
        public static void SettingAMetting(string[] attendees, string date, string time, string topic)
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
