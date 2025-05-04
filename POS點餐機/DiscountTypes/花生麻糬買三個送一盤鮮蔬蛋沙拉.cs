using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace POS點餐機.DiscountTypes
{
    class 花生麻糬買三個送一盤鮮蔬蛋沙拉 : ADiscount
    {
        public 花生麻糬買三個送一盤鮮蔬蛋沙拉(List<MealItem> items) : base(items)
        {
        }

        public override void Discount()
        {           
                MealItem item1 = items.FirstOrDefault(x => x.Name == "花生麻糬");
                if (item1?.Quantity >= 3)
                {
                    MealItem freeItem = new MealItem("(贈)鮮蔬蛋沙拉", 0, item1.Quantity / 3);
                    items.Add(freeItem);
                }
            
        }
    }
}
