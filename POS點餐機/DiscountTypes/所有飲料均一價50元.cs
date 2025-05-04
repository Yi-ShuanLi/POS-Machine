using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace POS點餐機.DiscountTypes
{
    class 所有飲料均一價50元 : ADiscount
    {
        public 所有飲料均一價50元(List<MealItem> items) : base(items)
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
                List<MealItem> mealItemsOnDrink = items.Where(x => drink.Contains(x.Name)).ToList();
                if (mealItemsOnDrink.Count >= 1)
                {
                    int totalAmountDrink = mealItemsOnDrink.Sum(x => x.Subtotal);
                    int totalDrinkCount = mealItemsOnDrink.Sum(x => x.Quantity);
                    int saveAmount = totalDrinkCount * 50 - totalAmountDrink;
                    MealItem freeItem = new MealItem("(折)所有飲料均一價50元", saveAmount, 1);
                    items.Add(freeItem);
                }
            
        }
    }
}
