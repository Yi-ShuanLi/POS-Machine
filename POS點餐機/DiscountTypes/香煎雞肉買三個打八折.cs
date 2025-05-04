using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace POS點餐機.DiscountTypes
{
    class 香煎雞肉買三個打八折 :ADiscount
    {
        public 香煎雞肉買三個打八折(List<MealItem> items) : base(items)
        {
        }

        public override void Discount()
        {
           
                MealItem item = items.FirstOrDefault(x => x.Name == "香煎雞肉");
                if (item?.Quantity >= 3)
                {
                    int savePrice = (int)(item.Price * 0.2 * -1);
                    MealItem freeItem = new MealItem("香煎雞肉買三個打八折", savePrice, item.Quantity / 3);
                    items.Add(freeItem);
                }
            
            
        }
    }
}
