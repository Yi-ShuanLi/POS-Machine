using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 合併AI練習.Tools;

namespace 合併AI練習
{
    public class AgentResult
    {
        public bool CanRunTool { get; set; } = false;
        public string Message { get; set; } 
        public ATool Tool { get; set; } 
        public AgentResult(string message) { 

            this.CanRunTool = false;
            this.Message = message;
        }
        public AgentResult( ATool tool)
        {
            this.CanRunTool = true;
            this.Tool = tool;
        }
    }
    internal class AgentHandler
    {
        AIAgent agent = new AIAgent();
        public async Task<AgentResult> RunAgentAsync(string userInput)
        {
            AIResponse aIResponse = await agent.SendMessage(userInput);
            var toolCall = aIResponse.candidates[0].content.parts[0].functionCall;
            if (toolCall != null)
            {
                Type type = Type.GetType("合併AI練習." + toolCall.name);
                ATool tool = (ATool)Activator.CreateInstance(type, new object[] { toolCall.args });
                return new AgentResult(tool);

            }
            return new AgentResult(aIResponse.candidates[0].content.parts[0].text);
        }

    }
}
