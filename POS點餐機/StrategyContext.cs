using POS點餐機.DiscountTypes;
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
        private ADiscount aDiscount;
        
        public StrategyContext(DiscountStrategy discountStrategy, List<MealItem> items)
        {
            string strategyType = "POS點餐機.Strategies." +discountStrategy.Strategy;
            Type type = Type.GetType(strategyType);
            aDiscount = (ADiscount)Activator.CreateInstance(type, new object[] { items });
        }
        public void CalcDiscount()
        {
            aDiscount.Discount();
        }
    }
}
