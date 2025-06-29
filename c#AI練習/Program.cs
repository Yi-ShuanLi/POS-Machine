using AI練習.AI;
using AI練習.AI.Tools;
using System;
using System.Threading.Tasks;

namespace c_AI練習
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            AgentHandler agentHandler = new AgentHandler();
            Console.WriteLine("我是天氣資訊小幫手~請問有甚麼可以幫忙您的?");
            agentHandler.AddModelPrompt("你是個推理大師，根據使用著描述的情境，去推理使用者所在的縣市，回答我想知道的天氣資訊");
            while (true) 
            {
                string userInput=Console.ReadLine();                
                AgentResult result = await agentHandler.RunAgentAsync(userInput);
                if (result.CanRunTool)
                {
                    ATool aTool = result.Tool;
                    aTool.Apply();
                    break;
                }
                else
                {
                    Console.WriteLine(result.Message);
                }
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
