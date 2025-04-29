using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace POS點餐機
{
    class Order
    {
        public static List<MealItem> OrderList = new List<MealItem>();
       
       
        public static void AddOrder(string orderType,MealItem newItem)
        {
            MealItem item = OrderList.FirstOrDefault(x => x.Name == newItem.Name);
            if (item == null)
            {
                OrderList.Add(newItem);
            }
            else if (newItem.Quantity <= 0)
            {
                OrderList.Remove(item);
            }
            else if (newItem.Quantity >= 1)
            {
                item.Quantity = newItem.Quantity;                            
            }
            DisCount.DisCountOrder(orderType, OrderList);
        }

        public static void RefreshOrder(string orderType)
        {
            DisCount.DisCountOrder(orderType,OrderList);
        }

    }
}
