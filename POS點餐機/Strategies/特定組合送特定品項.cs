using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static POS點餐機.MenuModel;
using static System.Windows.Forms.LinkLabel;

namespace POS點餐機.Strategies
{
    class ConditionGroup {
        public String Name { get; set; }
        public string ConditionID { get; set; }
        public int BuyCount { get; set; }
        public int ConditionCount { get; set; }
        public int Price { get; set; }
        public string RewardType { get; set; }
    }


    class 特定組合送特定品項 : ADiscountStrategy
    {
        public 特定組合送特定品項(MenuModel.DiscountStrategy discountStrategy, List<MealItem> items) : base(discountStrategy, items)
        {
        }

        public override void Discount()
        {
            #region LinQ

            var conditions = discountStrategy.Conditions.Select((x, index) => new
            {

                Names = x.Product.Split(','),
                MinRequireQty = x.Quantity,
                ConditionID = index.ToString()
            }).ToList();

            var rewards = discountStrategy.Rewards.Select((x, index) => new
            {

                Names = x.Product.Split(','),
                FreeQty = x.Quantity,
                ConditionID = String.IsNullOrEmpty(x.RewardsType) ? "normal_" + index : index.ToString(),
                RewardType = x.RewardsType
            }).ToList();



            List<ConditionGroup> availableConditions = items.Select(x =>
            {
                
                var condition = conditions.FirstOrDefault(y => y.Names.Contains(x.Name));
                if (condition == null)
                    return null;
                ConditionGroup availableCondition = new ConditionGroup()
                {
                    Name = x.Name,
                    ConditionID = condition.ConditionID,
                    BuyCount = x.Quantity,
                    ConditionCount = condition.MinRequireQty
                };
                return availableCondition;
            }).Where(x => x != null).ToList();


            var conditionGrops = availableConditions.GroupBy(x => new { x.ConditionID, x.ConditionCount });
            if (conditionGrops.Count() != conditions.Count)
                return;

            List<int> conditionFreeCount = new List<int>();

            conditionFreeCount = conditionGrops.Select(x =>
            {
                int GroupSum = x.Sum(y => y.BuyCount);
                int count = GroupSum / x.Key.ConditionCount;
                return count;
            }).ToList();

            if (conditionFreeCount.Contains(0))
                return ;

            int freeCount = conditionFreeCount.Min();

            //int passConditionCount = conditionGrops.Select(x =>
            //{
            //    int GroupSum = x.Sum(y => y.BuyCount);
            //    int count = GroupSum / x.Key.ConditionCount;
            //    conditionFreeCount.Add(count);
            //    return count > 0;
            //}).Where(x => x == true).Count();

            //if (passConditionCount != conditions.Count)
            //    return;

            //int freeCount = conditionFreeCount.Min();


            // 開始撰寫可以贈送的邏輯

            List<ConditionGroup> availableRewards = items.Select((x) =>
            {
                var reward = rewards.FirstOrDefault(y => y.Names.Contains(x.Name));
                if (reward == null)
                    return null;
                ConditionGroup availableReward = new ConditionGroup()
                {
                    Name = x.Name,
                    ConditionID = reward.ConditionID,
                    BuyCount = x.Quantity,
                    ConditionCount = reward.FreeQty,
                    Price = x.Price,
                    RewardType = reward.RewardType
                };
                return availableReward;
            }).Where(x => x != null).ToList();


            List<MealItem> freeGift = availableRewards.GroupBy(x => new { x.ConditionID, x.RewardType, x.ConditionCount }).Select(x =>
            {

                int price = 0;
                string rewardType = x.Key.RewardType;
                if (rewardType == "Min")
                    price = x.Min(y => y.Price);
                else if (rewardType == "Max")
                    price = x.Max(y => y.Price);
                else if (rewardType == "Any")
                {
                    price = x.OrderBy(y => new Random(Guid.NewGuid().GetHashCode()).Next()).First().Price;
                }



                string itemName = price != 0 ? x.First(y => y.Price == price).Name : rewards.First(y => y.ConditionID == x.Key.ConditionID).Names[0];

                return new MealItem("(贈送)" + itemName, 0, freeCount * x.Key.ConditionCount);
            }).ToList();

            items.AddRange(freeGift);


            //處理直接贈送的品項
            List<MealItem> normalGift = rewards.Where(x => x.ConditionID.Contains("normal")).Select(x => new MealItem($"(贈送){x.Names[0]}", 0, freeCount * x.FreeQty)).ToList();
            items.AddRange(normalGift);

            #endregion

            #region 迴圈完成的版本
            //List<List<string>> strings = new List<List<string>>();
            //List<List<int>> prices = new List<List<int>>();
            //List<int> anyQuantityRequired = new List<int>();
            //List<int> anyQuantitySubtotal = new List<int>();
            //List<int> freeQuantity = new List<int>();
            //for (int i = 0; i < discountStrategy.Conditions.Length; i++)
            //{
            //    List<string> strings1 = new List<string>();

            //    string[] strings2 = discountStrategy.Conditions[i].Product.Split(',');
            //    for (int k = 0; k < strings2.Length; k++)
            //    {
            //        strings1.Add(strings2[k]);
            //    }

            //    anyQuantityRequired.Add(discountStrategy.Conditions[i].Quantity);
            //    strings.Add(strings1);
            //}
            //if (strings.Count == 0)
            //{
            //    return;
            //}
            //string typeControl = discountStrategy.Rewards[0].RewardsType;

            //for (int j = 0; j < strings.Count; j++)
            //{
            //    List<int> priceMins = new List<int>();
            //    int count = 0;
            //    for (int l = 0; l < strings[j].Count; l++)
            //    {
            //        int price = typeControl == "Max" ? -1 : int.MaxValue;
            //        MealItem item = items.FirstOrDefault(x => x.Name == strings[j][l]);
            //        if (item != null)
            //        {
            //            price = item.Price;
            //            count += item.Quantity;
            //        }

            //        priceMins.Add(price);
            //    }
            //    prices.Add(priceMins);
            //    anyQuantitySubtotal.Add(count);

            //}
            //for (int i = 0; i < anyQuantityRequired.Count; i++)
            //{
            //    int freeNum = anyQuantitySubtotal[i] / anyQuantityRequired[i];
            //    freeQuantity.Add(freeNum);
            //}
            //if (freeQuantity.Count != conditions.Count)
            //{
            //    return;
            //}

            //if (freeQuantity.Contains(0))
            //{
            //    return;
            //}
            //List<List<string>> rewordStrings = new List<List<string>>();
            //List<List<int>> rewordPrices = new List<List<int>>();
            //List<int> rewordQuantity = new List<int>();
            //for (int i = 0; i < discountStrategy.Rewards.Length; i++)
            //{
            //    List<string> strings1 = new List<string>();

            //    string[] strings2 = discountStrategy.Rewards[i].Product.Split(',');
            //    for (int k = 0; k < strings2.Length; k++)
            //    {
            //        strings1.Add(strings2[k]);
            //    }

            //    rewordQuantity.Add(discountStrategy.Rewards[i].Quantity);
            //    rewordStrings.Add(strings1);
            //}
            //for (int i = 0; i < rewordStrings.Count; i++)
            //{
            //    List<int> ints = new List<int>();
            //    for (int j = 0; j < rewordStrings[i].Count; j++)
            //    {
            //        int price = typeControl == "Max" ? -1 : int.MaxValue;
            //        MealItem item = items.FirstOrDefault(x => x.Name == rewordStrings[i][j]);
            //        if (item != null)
            //        {
            //            price = item.Price;

            //        }
            //        ints.Add(price);
            //    }
            //    rewordPrices.Add(ints);
            //}

            //for (int i = 0; i < discountStrategy.Rewards.Length; i++)
            //{
            //    int index = SearchAndReturnIndex(rewordPrices[i], typeControl);
            //    int rewordQuantityCalc = freeQuantity.Min() * rewordQuantity[i];
            //    MealItem freeItemNew = new MealItem("贈" + rewordStrings[i][index], 0, rewordQuantityCalc);
            //    items.Add(freeItemNew);
            //}
            #endregion













            #region 原本
            //List<string> requireItem = new List<string>();
            ////List<int> requireQuantity = new List<int>();
            //requireItem = discountStrategy.Conditions.Select(x => x.Product).ToList();
            //bool checkHasReword = items.Any(x=> requireItem.Contains(x.Name));
            //if (!checkHasReword)
            //{
            //    return;
            //}
            ////requireQuantity= discountStrategy.Conditions.Select(x => x.Quantity).ToList();
            //List<int> rewardQuantity = new List<int>();
            //for(int i = 0; i < discountStrategy.Conditions.Length; i++)
            //{
            //    MealItem mealItem = items.FirstOrDefault(x => x.Name == discountStrategy.Conditions[i].Product);
            //    int rewordQ = mealItem.Quantity / discountStrategy.Conditions[i].Quantity;
            //    if (rewordQ<1)
            //    {
            //        return;
            //    }
            //    rewardQuantity.Add(rewordQ);
            //}
            //MealItem freeItem = new MealItem(discountStrategy.Name, 0, rewardQuantity.Min());
            //items.Add(freeItem);
            #endregion



        }
        private int  SearchAndReturnIndex(List<int> ints,String require)
        {
             int index;
             
            if (require == "Min")
            {
               index= ints.IndexOf(ints.Min());
            }else if (require=="Max")
            {
                index = ints.IndexOf(ints.Max());
            }else if (require =="Any")
            {
                List<int> exclude = new List<int> {-1,2147483647 };
                do
                {
                    Random random = new Random(Guid.NewGuid().GetHashCode());
                    index = random.Next(0, ints.Count);
                } while (exclude.Contains(ints[index]));
            }
            else
            {
                index = 0;
            }

            return index;
        }
    }
}
