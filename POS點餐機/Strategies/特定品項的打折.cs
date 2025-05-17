using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.Strategies
{
    class 特定品項的打折 : ADiscountStrategy
    {
        public 特定品項的打折(MenuModel.DiscountStrategy discountStrategy, List<MealItem> items) : base(discountStrategy, items)
        {
        }

        public override void Discount()
        {
            //throw new NotImplementedException();
        }
    }
}
