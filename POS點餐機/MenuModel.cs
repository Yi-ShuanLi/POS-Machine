using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機
{
    public class MenuModel
    {
        public Food[] Foods { get; set; }
        public DiscountStrategy[] Discounts { get; set; }
        

        public class Food
        {
            public string name { get; set; }
            public Itemname[] itemName { get; set; }
        }

        public class Itemname
        {
            public string name { get; set; }
            public int price { get; set; }
        }

        public class DiscountStrategy
        {
            public string Name { get; set; }
            public string Strategy { get; set; }
            public Condition[] Conditions { get; set; }
            public Reward[] Rewards { get; set; }
        }

        public class Condition
        {
            public string Product { get; set; }
            public int Quantity { get; set; }
            public int TotalPrice { get; set; }
        }

        public class Reward
        {
            public string Product { get; set; }
            public int Quantity { get; set; }
            public int Price { get; set; }
            public int TotalPrice { get; set; }
            public float Percentage { get; set; }
        }

    }
}
