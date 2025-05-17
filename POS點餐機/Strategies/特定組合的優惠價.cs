using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.Strategies
{
    class 特定組合的優惠價 : ADiscountStrategy
    {
        public 特定組合的優惠價(MenuModel.DiscountStrategy discountStrategy, List<MealItem> items) : base(discountStrategy, items)
        {
        }

        public override void Discount()
        {
            
        }
    }
}
