using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POS點餐機.MenuModel;

namespace POS點餐機.Strategies
{
    class 消費總額的打折 : ADiscountStrategy
    {
        public 消費總額的打折(MenuModel.DiscountStrategy discountStrategy, List<MealItem> items) : base(discountStrategy, items)
        {

        }

        public override void Discount()
        {

            int totalPrice = items.Sum(x => x.Subtotal);
            int conditionTotalPrice = discountStrategy.Conditions.Sum(x => x.TotalPrice);

            if (totalPrice < conditionTotalPrice)
                return;

            int saveMoney = discountStrategy.Rewards[0].TotalPrice;
            double savePercentage = discountStrategy.Rewards[0].Percentage;

            saveMoney = saveMoney == 0 ? (int)(totalPrice * savePercentage) : saveMoney;
            int count = savePercentage == 0 ? (totalPrice / conditionTotalPrice) : 1;

            MealItem freeItem = new MealItem(discountStrategy.Name, saveMoney, count);
            items.Add(freeItem);
        }
    }
}
