using POS點餐機.DiscountTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機
{
    class StrategyContext
    {
        private ADiscount aDiscount;
        
        public StrategyContext(string orderType, List<MealItem> items)
        {
            orderType = "POS點餐機.DiscountTypes." + orderType;
            Type type = Type.GetType(orderType);
            aDiscount = (ADiscount)Activator.CreateInstance(type, new object[] { items });
        }
        public void CalcDiscount()
        {
            aDiscount.Discount();
        }
    }
}
