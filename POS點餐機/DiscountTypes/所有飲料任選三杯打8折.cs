using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace POS點餐機.DiscountTypes
{
    class 所有飲料任選三杯打8折 : ADiscount
    {
        public 所有飲料任選三杯打8折(List<MealItem> items) : base(items)
        {
        }

        public override void Discount()
        {           
                List<string> drink = new List<string>()
                {
                    "珍珠奶茶", "冬瓜檸檬", "四季春青茶",
                    "紅茶拿鐵", "黑糖鮮奶", "仙草凍飲",
                     "檸檬愛玉", "芒果冰沙"
                };              
                List<MealItem> drinksList = items.Where(x => drink.Contains(x.Name)).OrderBy(x => x.Price).ToList();
                int drinkCount = (drinksList.Sum(x => x.Quantity) / 3) * 3;

                int discountAmount = (int)(drinksList.SelectMany(x => Enumerable.Repeat(x.Price, x.Quantity))
                                     .OrderBy(Price => Price)
                                     .Take(drinkCount)
                                     .Sum(x => x) * 0.2 * -1);
                
                MealItem freeItem = new MealItem("所有飲料任選三杯打8折", discountAmount, 1);
                items.Add(freeItem);
            
        }

    
    }
}
