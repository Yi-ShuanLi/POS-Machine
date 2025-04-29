using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機
{
    class DisCount
    {
        public static void DisCountOrder(string orderType,List<MealItem> items)
        {
            //酥炸魚排買二送一
            //香煎雞肉買三個打八折
            //皇家大排搭配黑糖珍奶250元
            //台式蔥爆豬加鹽酥雞小份就送冬瓜檸檬
            //花生麻糬買三個送一盤鮮蔬蛋沙拉
            //酥炸雞柳搭配蘿蔔糕打85折
            //所有飲料任選三杯打8折
            //所有飲料均一價50元
            //所有飲料任選三杯送一杯(送最便宜價格)
            //全場消費滿399折50
            //全場消費打9折
            items.RemoveAll(x => x.Name.Contains("贈")|| x.Name.Contains("折"));
            
            if (orderType == "酥炸魚排買二送一")
            {
                MealItem item = items.FirstOrDefault(x => x.Name == "酥炸魚排");
                if (item?.Quantity >1)
                {
                    MealItem freeItem = new MealItem("(贈)酥炸魚排",0,item.Quantity/2);
                    items.Add(freeItem);
                }
            }
            if (orderType == "香煎雞肉買三個打八折")
            {
                MealItem item = items.FirstOrDefault(x => x.Name == "香煎雞肉");
                if (item?.Quantity >= 3)
                {
                    int savePrice =(int)(item.Price * 0.2 * -1);
                    MealItem freeItem = new MealItem("香煎雞肉買三個打八折", savePrice, item.Quantity/3);
                    items.Add(freeItem);
                }
            }
            if (orderType == "皇家大排搭配黑糖鮮奶250元")
            {
                MealItem item1 = items.FirstOrDefault(x => x.Name == "皇家大排");
                MealItem item2 = items.FirstOrDefault(x => x.Name == "黑糖鮮奶");
                if (item1?.Quantity > 0 && item2?.Quantity>0)
                {
                    int savePrice = -45;
                    int saveQuantity = item1.Quantity > item2.Quantity ? item2.Quantity : item1.Quantity;
                    MealItem freeItem = new MealItem("(折)皇家大排搭配黑糖鮮奶", savePrice, saveQuantity);
                    items.Add(freeItem);
                }
            }
            if (orderType == "台式蔥爆豬加鹽酥雞小份就送冬瓜檸檬")
            {
                MealItem item1 = items.FirstOrDefault(x => x.Name == "台式蔥爆豬");
                MealItem item2 = items.FirstOrDefault(x => x.Name == "鹽酥雞小份");
                if (item1?.Quantity > 0 && item2?.Quantity > 0)
                {                    
                    MealItem freeItem = new MealItem("(贈)冬瓜檸檬", 0, 1);
                    items.Add(freeItem);
                }
            }
            if (orderType == "花生麻糬買三個送一盤鮮蔬蛋沙拉")
            {
                MealItem item1 = items.FirstOrDefault(x => x.Name == "花生麻糬");
                if (item1?.Quantity>=3)
                {
                    MealItem freeItem = new MealItem("(贈)鮮蔬蛋沙拉", 0, item1.Quantity/3);
                    items.Add(freeItem);
                }
            }
            if (orderType == "酥炸雞柳搭配蘿蔔糕打85折")
            {
                MealItem item1 = items.FirstOrDefault(x => x.Name == "酥炸雞柳");
                MealItem item2 = items.FirstOrDefault(x => x.Name == "蘿蔔糕");
                if (item1?.Quantity > 0 && item2?.Quantity > 0)
                {
                    int savePrice = (int)((item1.Price+item2.Price)*0.15*-1);
                    int saveQuantity = item1.Quantity > item2.Quantity ? item2.Quantity : item1.Quantity;
                    MealItem freeItem = new MealItem("酥炸雞柳搭配蘿蔔糕打85折", savePrice, saveQuantity);
                    items.Add(freeItem);
                }
            }
            if (orderType == "所有飲料任選三杯打8折")
            {
                List<string> drink = new List<string>()
                {
                    "珍珠奶茶", "冬瓜檸檬", "四季春青茶",
                    "紅茶拿鐵", "黑糖鮮奶", "仙草凍飲",
                     "檸檬愛玉", "芒果冰沙"
                };

                //int count = (items.Sum(x => x.Quantity) / 3) * 3;

                //int res = (int)(items.SelectMany(x => Enumerable.Repeat(x.Price, x.Quantity))
                //                     .Take(count)
                //                     .Sum(x => x) * 0.2 * -1);

                //int[,] prices = { };

                //List<int[]> list = new List<int[]> {

                //    new int[]  { 100, 90, 80 },
                //    new int[]  { 50, 60, 70 },
                //    new int[]  { 40, 30, 20 },

                //};

                //var res =  list.SelectMany(x => x).ToList();

                List<MealItem> drinksList = items.Where(x => drink.Contains(x.Name)).OrderBy(x => x.Price).ToList();
                int drinkCount = (drinksList.Sum(x => x.Quantity) / 3) * 3;

                int discountAmount = (int)(drinksList.SelectMany(x => Enumerable.Repeat(x.Price, x.Quantity))
                                     .OrderBy(Price=>Price)
                                     .Take(drinkCount)
                                     .Sum(x => x) * 0.2 * -1);
                //if (drinksList.Sum(x=>x.Quantity) >=3)
                //{
                //    List<int> savePrices = new List<int>();
                //    for (int i = 0; i < drinksList.Count; i++)
                //    {
                //        for (int j = 0; j < drinksList[i].Quantity; j++)
                //        {
                //            savePrices.Add(drinksList[i].Price);
                //        }
                //    }
                //    int discountDrinkQuantity = drinksList.Sum(x => x.Quantity) / 3 * 3;
                //    int saveAmount = (int)(savePrices.Take(discountDrinkQuantity).Sum() * 0.2 * -1);
                    MealItem freeItem = new MealItem("所有飲料任選三杯打8折", discountAmount, 1);
                    items.Add(freeItem);
            }
            

                // 3*50+3*60+3*70 = 3*(50+60+70)
                // 
                // int discountDrinkQuantity =drinksList.Sum(x=>x.Quantity) / 3*3;
                // int saveAmount = 0;              
                //for(int i=0;i< drinksList.Count; i++)
                // {
                //     if (discountDrinkQuantity == 0)
                //     {
                //         break;
                //     }
                //     for (int j=0;j< drinksList[i].Quantity; j++)
                //     {
                //         if (discountDrinkQuantity == 0)
                //         {
                //             break;
                //         }
                //         saveAmount -= (int)(drinksList[i].Price * 0.2);
                //         discountDrinkQuantity--;                       
                //     }
                // }             
                               
           
            if (orderType == "所有飲料均一價50元")
            {
                List<string> drink = new List<string>()
                {
                    "珍珠奶茶", "冬瓜檸檬", "四季春青茶",
                    "紅茶拿鐵", "黑糖鮮奶", "仙草凍飲",
                    "檸檬愛玉", "芒果冰沙"
                };              
                List<MealItem> mealItemsOnDrink = items.Where(x=>drink.Contains(x.Name)).ToList();
                if (mealItemsOnDrink.Count >= 1)
                {
                    int totalAmountDrink = mealItemsOnDrink.Sum(x => x.Subtotal);
                    int totalDrinkCount = mealItemsOnDrink.Sum(x => x.Quantity);
                    int saveAmount = totalDrinkCount * 50 - totalAmountDrink;
                    MealItem freeItem = new MealItem("(折)所有飲料均一價50元", saveAmount, 1);
                    items.Add(freeItem);
                }
            }
            if (orderType == "所有飲料任選三杯送一杯(送最便宜價格)")
            {
                List<string> drink = new List<string>()
                {
                    "珍珠奶茶", "冬瓜檸檬", "四季春青茶",
                    "紅茶拿鐵", "黑糖鮮奶", "仙草凍飲",
                     "檸檬愛玉", "芒果冰沙"
                };

                List<MealItem> mealItemsOnDrink = items.Where(x => drink.Contains(x.Name)).OrderBy(x => x.Price).ToList();
                if (mealItemsOnDrink.Sum(x => x.Quantity)>=3)
                {
                    MealItem cheapestDrink = mealItemsOnDrink.First();
                    int numberOfFreeDrink = mealItemsOnDrink.Sum(x => x.Quantity) / 3;
                    MealItem freeItem = new MealItem("(贈)" + cheapestDrink.Name, 0, numberOfFreeDrink);
                    items.Add(freeItem);
                }
            }
            if (orderType == "全場消費滿399折50")
            {          
                int total = items.Sum(x => x.Subtotal);
                if (total >= 399)
                {
                    MealItem freeItem = new MealItem("全場消費滿399折50", -50, total / 399);
                    items.Add(freeItem);
                }
            }
            if (orderType == "全場消費打9折")
            {
                if (items.Count > 0)
                {
                    int total = items.Sum(x => x.Subtotal);
                    int saveAmount = (int)(total * 0.1 * -1);
                    MealItem freeItem = new MealItem("全場消費打9折", saveAmount, 1);
                    items.Add(freeItem);
                }
            }

            ShowPanel.UpdateSelectedOnShowPanel(items);

        }
    }
}
