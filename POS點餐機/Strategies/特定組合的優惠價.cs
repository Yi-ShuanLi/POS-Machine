using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.Strategies
{
    class 特定組合的優惠價 : ADiscountStrategy
    {
        class ConditionGroupOnSecialPrice
        {
            public String Name { get; set; }
            public string ConditionID { get; set; }
            public int BuyCount { get; set; }
            public int ConditionCount { get; set; }
            public int Price { get; set; }
            //public string RewardType { get; set; }
        }
        public 特定組合的優惠價(MenuModel.DiscountStrategy discountStrategy, List<MealItem> items) : base(discountStrategy, items)
        {
        }

        public override void Discount()
        {
            var conditions = discountStrategy.Conditions.Select((x, index) => new
            {
                Names = x.Product.Split(','), // 目的產生List <string>
                MinrequirQty = x.Quantity,      //
                ConditionID = index.ToString(), // 目的供後面Group BY 使用
            }).ToList();
            List<ConditionGroupOnSecialPrice> avaliableConditions = items.Select(x =>
            {
                // condition 是 List<自訂的table> ，所以在FirstOrDefault時，是外層
                // 第一個條件Obj(Table)，第二個條件Obj(Table)，第三個條件這樣跑Obj(Table)
                // Contains是每個Obj(Table)下的Names 欄位的Lsit<string> 再去跑回圈比對條件
                // 找到即回傳至 condition
                var condition = conditions.FirstOrDefault(y => y.Names.Contains(x.Name));
                if (condition == null)
                    return null;
                ConditionGroupOnSecialPrice avaliableCondition = new ConditionGroupOnSecialPrice()
                {
                    Name = x.Name,
                    ConditionID = condition.ConditionID,
                    BuyCount = x.Quantity,
                    Price = x.Price,
                    ConditionCount = condition.MinrequirQty,
                };
                return avaliableCondition;
                // select 感覺有選取之意，用return 的動作來完成選定，中間可插入資料型態的轉換或運算
            }).Where(x => x != null).OrderBy(x => x.Price).ToList();
            // 因為 avaliableConditions 是由items foreach 走完的，所以可能會出現
            // 當conditions 是任選的情況，有List<string> 對應上多items個吻合其中的產品
            // 就會出現明明只是落在condition [0] ，但items 是多筆
            // 所以就必須要用GroupBy 去收起來，群組歸隊，對應到相對應個 condition [index]
            var conditionsGroup = avaliableConditions.GroupBy(x => new { x.ConditionID, x.ConditionCount });

            // 群組後的組別數量必須與conditions條件一樣，否則就 return 停止繼續執行
            if (conditionsGroup.Count() != conditions.Count)
                return;
            //計算每個 在每個 condition 要求下的數量，我符合的倍數=能贈送的數量
            List<int> conditionFreeCount = conditionsGroup.Select(x =>
            {
                // x為群組後的組長key，由{x.ConditionID,x.ConditionCount}所構成
                // 這裡的y，因為前面有x.出來並括號，所以自動默認為每組的細項
                int grounSum = x.Sum(y => y.BuyCount);
                int count = grounSum / x.Key.ConditionCount;
                return count;
            }).ToList();
            // 假設有任何一個條件沒有達成condition所要求的數量至少一倍，那就retrun，不繼續進行程式
            if (conditionFreeCount.Contains(0))
                return;
            // 確定要贈送的數量，即為符合條件倍數的List中的最小值
            int freeCount = conditionFreeCount.Min();
            
            int reduceAmount = (discountStrategy.Rewards[0].Price * freeCount) - conditionsGroup.Select(x => x.SelectMany(y => Enumerable.Repeat(y.Price, y.BuyCount)).OrderBy(Price => Price).Take(freeCount * x.Key.ConditionCount).Sum()).Sum();

            MealItem freeItem = new MealItem("(折)" + discountStrategy.Name, reduceAmount, 1);
            items.Add(freeItem);



            #region 原始
            //List<List<string>> contitionNames = new List<List<string>>();
            //List<List<int>> conditionPrices = new List<List<int>>();
            //List<int> anyQuantityRequired = new List<int>();
            //List<List<int>> buyQuantityRecord = new List<List<int>>();
            //List<int> cheaperQuantitys = new List<int>();

            //for (int i = 0; i < discountStrategy.Conditions.Length; i++)
            //{
            //    List<string> strings1 = new List<string>();

            //    string[] strings2 = discountStrategy.Conditions[i].Product.Split(',');
            //    for (int j = 0; j < strings2.Length; j++)
            //    {
            //        strings1.Add(strings2[j]);
            //    }

            //    anyQuantityRequired.Add(discountStrategy.Conditions[i].Quantity);
            //    contitionNames.Add(strings1);
            //}
            //for (int i = 0; i < contitionNames.Count; i++)
            //{
            //    List<int> price1 = new List<int>();
            //    List<int> quantity1 = new List<int>();
            //    for (int j = 0; j < contitionNames[i].Count; j++)
            //    {
            //        int price2 = 0;
            //        int quantity2 = 0;
            //        MealItem item = items.FirstOrDefault(x => x.Name == contitionNames[i][j]);
            //        if (item != null)
            //        {
            //            price2 = item.Price;
            //            quantity2 = item.Quantity;
            //        }
            //        price1.Add(price2);
            //        quantity1.Add(quantity2);
            //    }
            //    conditionPrices.Add(price1);
            //    buyQuantityRecord.Add(quantity1);
            //}
            //if (conditionPrices.Count == 0)
            //{
            //    return;
            //}
            //for (int i = 0; i < buyQuantityRecord.Count; i++)
            //{
            //    if (buyQuantityRecord[i].Sum() < anyQuantityRequired[i])
            //    {
            //        return;
            //    }
            //}
            ////conditions ORDER  By Price
            //for (int i = 0; i < contitionNames.Count; i++)
            //{
            //    for (int j = 0; j < contitionNames[i].Count; j++)
            //    {
            //        int tempNum1 = conditionPrices[i][j];
            //        for (int k = j + 1; k < contitionNames[i].Count; k++)
            //        {
            //            int tempNum2 = conditionPrices[i][k];
            //            if (tempNum2 < tempNum1)
            //            {
            //                string tempName = contitionNames[i][j];
            //                contitionNames[i][j] = contitionNames[i][k];
            //                contitionNames[i][k] = tempName;

            //                int tempPrice = conditionPrices[i][j];
            //                conditionPrices[i][j] = conditionPrices[i][k];
            //                conditionPrices[i][k] = tempPrice;

            //                int tempBuy = buyQuantityRecord[i][j];
            //                buyQuantityRecord[i][j] = buyQuantityRecord[i][k];
            //                buyQuantityRecord[i][k] = tempBuy;
            //            }
            //        }
            //    }
            //}
            //for (int i = 0; i < buyQuantityRecord.Count; i++)
            //{
            //    int tempQuantity = (buyQuantityRecord[i].Sum() / anyQuantityRequired[i]);
            //    cheaperQuantitys.Add(tempQuantity);
            //}
            //List<List<int>> originalPrices = new List<List<int>>();
            //for (int i = 0; i < conditionPrices.Count; i++)
            //{
            //    List<int> ints = new List<int>();
            //    for (int j = 0; j < conditionPrices[i].Count; j++)
            //    {

            //        for (int k = 0; k < buyQuantityRecord[i][j]; k++)
            //        {
            //            ints.Add(conditionPrices[i][j]);
            //        }
            //    }
            //    originalPrices.Add(ints);
            //}
            //int subTotalAmount = 0;
            //for (int i = 0; i < originalPrices.Count; i++)
            //{
            //    int count = discountStrategy.Conditions[i].Quantity * cheaperQuantitys.Min();
            //    subTotalAmount += originalPrices[i].Take(count).Sum();
            //}
            //int reduceCount =  cheaperQuantitys.Min() ;
            //int reduceAmount =(discountStrategy.Rewards[0].Price* reduceCount)- subTotalAmount;
            //MealItem freeItem = new MealItem("(折)" + discountStrategy.Name, reduceAmount, 1);
            //items.Add(freeItem);
            #endregion
        }
    }
}
