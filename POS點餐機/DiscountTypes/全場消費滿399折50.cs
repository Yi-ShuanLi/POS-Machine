using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace POS點餐機.DiscountTypes
{
    class 全場消費滿399折50 : ADiscount
    {
        public 全場消費滿399折50(List<MealItem> items) : base(items)
        {
        }

        public override void Discount()
        {
            
                int total = items.Sum(x => x.Subtotal);
                if (total >= 399)
                {
                    MealItem freeItem = new MealItem("全場消費滿399折50", -50, total / 399);
                    items.Add(freeItem);
                }
            
            
        }

     
    }
}
