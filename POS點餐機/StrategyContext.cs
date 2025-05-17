using POS點餐機.DiscountTypes;
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
        private ADiscountStrategy discountStrategy;
        
        public StrategyContext(DiscountStrategy discountStrategySelected, List<MealItem> items)
        {
            string strategyType = "POS點餐機.Strategies." + discountStrategySelected.Strategy;
            Type type = Type.GetType(strategyType);
            discountStrategy = (ADiscountStrategy)Activator.CreateInstance(type, new object[] { discountStrategySelected, items });
        }
        public void CalcDiscount()
        {
            discountStrategy.Discount();
        }
    }
}
