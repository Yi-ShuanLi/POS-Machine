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

        /// <summary>
        /// 增加餐點，該function裡面自己有List<MealItem>，會經過邏輯判斷後加入在自己class的 List<MealItem>，把input參數OrderRequestModel與自有的List<MealItem>裝一起，pass出去給DisCount.DisCountOrder去執行
        /// </summary>
        /// <param name="requestModel">OrderRequestModel提供多載的建構式</param>
        /// <returns>把input參數OrderRequestModel與自有的List<MealItem>裝一起，pass出去給DisCount.DisCountOrder去執行</returns>
        public static async Task AddOrder(OrderRequestModel requestModel)
        {
            //先判斷本次MealItem有沒有在本class的List<MealItem>已經被選取過了，
            //迴圈走在OrderList 把OrderList的每個MealItem.Name去與OrderRequestModel.OrderItem.Name 比對
            MealItem item = OrderList.FirstOrDefault(x => x.Name == requestModel.OrderItem.Name);            
            if (item == null)//假設從未被選過，無腦加入List<MealItem>
            {
                OrderList.Add(requestModel.OrderItem);
            }//假設有被選過，判斷傳入的 requestModel.OrderItem.Quantity 值
            else if (requestModel.OrderItem.Quantity <= 0)
            {
                OrderList.Remove(item);
            }
            else if (requestModel.OrderItem.Quantity >= 1)
            {
                item.Quantity = requestModel.OrderItem.Quantity;                            
            }
            //把input參數OrderRequestModel與自有的List<MealItem>裝一起
            requestModel.Items= OrderList;
            //pass出去給DisCount.DisCountOrder去執行
            await DisCount.DisCountOrder(requestModel);
        }

        public static async Task RefreshOrder(OrderRequestModel requestModel)
        {
            requestModel.Items = OrderList;
            await DisCount.DisCountOrder(requestModel);
        }

    }
}
