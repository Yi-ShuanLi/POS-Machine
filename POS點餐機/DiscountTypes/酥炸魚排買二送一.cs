using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace POS點餐機.DiscountTypes
{
     class 酥炸魚排買二送一 : ADiscount
    {
        public 酥炸魚排買二送一(List<MealItem> items) : base(items)
        {
        }

        public override void Discount()
        {
            MealItem item = items.FirstOrDefault(x => x.Name == "酥炸魚排");
            if (item?.Quantity > 1)
            {
                MealItem freeItem = new MealItem("(贈)酥炸魚排", 0, item.Quantity / 2);
                items.Add(freeItem);
            }
            
        }
    }
}
