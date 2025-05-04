using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace POS點餐機.DiscountTypes
{
    class 皇家大排搭配黑糖鮮奶250元 : ADiscount
    {
        public 皇家大排搭配黑糖鮮奶250元(List<MealItem> items) : base(items)
        {
        }

        public override void Discount()
        {
           
                MealItem item1 = items.FirstOrDefault(x => x.Name == "皇家大排");
                MealItem item2 = items.FirstOrDefault(x => x.Name == "黑糖鮮奶");
                if (item1?.Quantity > 0 && item2?.Quantity > 0)
                {
                    int savePrice = -45;
                    int saveQuantity = item1.Quantity > item2.Quantity ? item2.Quantity : item1.Quantity;
                    MealItem freeItem = new MealItem("(折)皇家大排搭配黑糖鮮奶", savePrice, saveQuantity);
                    items.Add(freeItem);
                }
            
        }
    }
}
