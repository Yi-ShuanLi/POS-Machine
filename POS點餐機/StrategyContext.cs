using Newtonsoft.Json;
using POS點餐機.AI;
using POS點餐機.AI.Common;
using POS點餐機.AI.Tools.DetermineBestChoice;
using POS點餐機.DiscountTypes;
using POS點餐機.EventHandlers;
using POS點餐機.Models;
using POS點餐機.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POS點餐機.MenuModel;

namespace POS點餐機
{
    class StrategyContext
    {
        private string strategy = "POS點餐機.Strategies.特定組合送特定品項";
        private DiscountStrategy discountStrategySelected;
        private bool AIRecommand = false;
        private List<MealItem> items;
        private AgentHandler agentHandler = new AgentHandler();
        public StrategyContext(DiscountStrategy discountStrategySelected, List<MealItem> items)
        {
            strategy = "POS點餐機.Strategies." + discountStrategySelected.Strategy;
            this.discountStrategySelected = discountStrategySelected;
            this.items = items; 
        }
        public StrategyContext(List<MealItem> items)
        {
            this.AIRecommand = true;
            this.items = items;
            agentHandler.AddModelPrompt("你是餐廳智慧助手，任務是幫助使用者根據他所購買的品項，自動判斷所有優惠方案中哪一種最划算。回覆格式只能使用tool所提供的工具。");
            agentHandler.AddModelPrompt($"以下是所有的菜單資料:{JsonConvert.SerializeObject(MenuData.Foods)}");
            agentHandler.AddModelPrompt($"以下是所有的餐點折扣資料:{JsonConvert.SerializeObject(MenuData.Discounts)}");

        }
        private async Task<AgentResult> RunAIRecommand(List<MealItem> items)
        {
            if (items.Count == 0)
                return null;
            string userOrder = String.Join("和", items.Select(x => $"{x.Quantity}份{x.Name}"));
            AgentResult response = await agentHandler.RunAgentAsync($"選擇優惠方案前的購買品項及數量(未贈送及未折扣前的原始狀態):{userOrder}，請問哪個優惠方案最划算?");
           
            return response;
        }
        public async Task CalcDiscount()
        {
           
            if (this.AIRecommand)
            {
                AgentResult result = await RunAIRecommand(items);
                if(result.CanRunTool)
                {
                    BestChoiceArgs args = (BestChoiceArgs)result.Tool.Apply();
                    Type type = Type.GetType("POS點餐機.Strategies." + args.strategy);
                    this.discountStrategySelected = MenuData.Discounts.First(x => x.Name == args.plan_name);
                    ADiscountStrategy discountStrategy = (ADiscountStrategy)Activator.CreateInstance(type, new object[] { discountStrategySelected, items });
                    discountStrategy.Discount();
                    AIRecommandHandler.Recommand(args);
                }
               
            }
            else
            {
                Type type = Type.GetType(strategy);

                ADiscountStrategy discountStrategy = (ADiscountStrategy)Activator.CreateInstance(type, new object[] { discountStrategySelected, items });
                discountStrategy.Discount();
            }

              
        }
    }
}
