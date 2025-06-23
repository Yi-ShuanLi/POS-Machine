using POS點餐機.AI.Common;
using POS點餐機.Models;
using POS點餐機.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POS點餐機.AI.Common.AIRequest;

namespace POS點餐機.AI.Tools.DetermineBestChoice
{
    internal class BestChoiceFunctiondeclaration : Functiondeclaration
    {
        public BestChoiceFunctiondeclaration()
        {
            this.name = "AI.Tools.BestChoice.BestChoiceTool";
            this.description = "此函數為經過推理後返回優惠方案CP值最高的選項";
            this.parameters = new BestChoiceParameter();
        }
    }
    internal class BestChoiceParameter : AIRequest.Parameters
    {
        private object _properties;
        public override object properties { get => _properties; }
        public BestChoiceParameter()
        {
            this.type = "object";
            this.required = new string[] { "plan_name", "strategy", "reason" };

            _properties = new
            {
                plan_name = new AIRequestArgs("string", "該參數是優惠方案的名稱，優惠規則依照名稱去推理", MenuData.Discounts.Select(x=>x.Name).ToArray()),
                strategy = new AIRequestArgs("string", "依照選定的優惠方案名稱plan_name去找出對應的Strategy", MenuData.Discounts.Select(x => x.Strategy).ToArray()),
                reason = new AIRequestArgs("string", "該參數是解釋為何選擇此優惠方案" )
            };
        }

    }

}
