using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace POS點餐機.DiscountTypes
{
    class 台式蔥爆豬加鹽酥雞小份就送冬瓜檸檬 : ADiscount
    {
        public 台式蔥爆豬加鹽酥雞小份就送冬瓜檸檬(List<MealItem> items) : base(items)
        {
        }

        public override void Discount()
        {            
                MealItem item1 = items.FirstOrDefault(x => x.Name == "台式蔥爆豬");
                MealItem item2 = items.FirstOrDefault(x => x.Name == "鹽酥雞小份");
                if (item1?.Quantity > 0 && item2?.Quantity > 0)
                {
                    MealItem freeItem = new MealItem("(贈)冬瓜檸檬", 0, 1);
                    items.Add(freeItem);
                }
            
        }
    }
}
