using POS點餐機.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static POS點餐機.MenuModel;



namespace POS點餐機
{
    class Order
    {
        public static List<MealItem> OrderList = new List<MealItem>();
       
       
        public static async Task AddOrder(OrderRequestModel requestModel)
        {
            MealItem item = OrderList.FirstOrDefault(x => x.Name == requestModel.OrderItem.Name);
            if (item == null)
            {
                OrderList.Add(requestModel.OrderItem);
            }
            else if (requestModel.OrderItem.Quantity <= 0)
            {
                OrderList.Remove(item);
            }
            else if (requestModel.OrderItem.Quantity >= 1)
            {
                item.Quantity = requestModel.OrderItem.Quantity;                            
            }
            requestModel.Items= OrderList;
          await  DisCount.DisCountOrder(requestModel);
        }

        public static async Task RefreshOrder(OrderRequestModel requestModel)
        {
            requestModel.Items = OrderList;
            await DisCount.DisCountOrder(requestModel);
        }

    }
}
