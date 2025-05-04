using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace POS點餐機.DiscountTypes
{
    class 酥炸雞柳搭配蘿蔔糕打85折 : ADiscount
    {
        public 酥炸雞柳搭配蘿蔔糕打85折(List<MealItem> items) : base(items)
        {
        }

        public override void Discount()
        {
            
                MealItem item1 = items.FirstOrDefault(x => x.Name == "酥炸雞柳");
                MealItem item2 = items.FirstOrDefault(x => x.Name == "蘿蔔糕");
                if (item1?.Quantity > 0 && item2?.Quantity > 0)
                {
                    int savePrice = (int)((item1.Price + item2.Price) * 0.15 * -1);
                    int saveQuantity = item1.Quantity > item2.Quantity ? item2.Quantity : item1.Quantity;
                    MealItem freeItem = new MealItem("酥炸雞柳搭配蘿蔔糕打85折", savePrice, saveQuantity);
                    items.Add(freeItem);
                }
            
        }
    }
}
