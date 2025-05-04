using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace POS點餐機.DiscountTypes
{
    class 全場消費打9折 : ADiscount
    {
        public 全場消費打9折(List<MealItem> items) : base(items)
        {
        }

        public override void Discount()
        {          
                if (items.Count > 0)
                {
                    int total = items.Sum(x => x.Subtotal);
                    int saveAmount = (int)(total * 0.1 * -1);
                    MealItem freeItem = new MealItem("全場消費打9折", saveAmount, 1);
                    items.Add(freeItem);
                }
            
        }
    }
}
