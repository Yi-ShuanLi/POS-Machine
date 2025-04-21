using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace POS點餐機
{
    class Order
    {
        public static List<MealItem> OrderList = new List<MealItem>();
       
       
        public static FlowLayoutPanel AddOrder(MealItem newItem)
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

          
            return UpdateSelectedOnShowPanel();
        }
        public static  FlowLayoutPanel UpdateSelectedOnShowPanel()
        {
            
            FlowLayoutPanel bigFlowLayoutPanel= new FlowLayoutPanel();
            bigFlowLayoutPanel.Width = 420;
            bigFlowLayoutPanel.Height = 589;
            List<string> title = new List<string> { "品名", "單價", "數量", "小計" };           
            for (int i = 0; i < title.Count; i++)
            {
                Label label = new Label();
                label.Text = title[i];
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Width = i == 0 ? 100 : 50;
                bigFlowLayoutPanel.Controls.Add(label);
            }
            foreach (MealItem mealItem in OrderList)
            {
                 AMealShow(bigFlowLayoutPanel, mealItem);
            }
            return bigFlowLayoutPanel;
        }
        private static void AMealShow(FlowLayoutPanel bigFlowLayoutPanel, MealItem newItem)
        {                    
            FlowLayoutPanel smallFlowLayoutPanel = new FlowLayoutPanel();
            smallFlowLayoutPanel.Width = 420;
            smallFlowLayoutPanel.Height = 40;
            List<string> title = new List<string> { newItem.Name, newItem.Price.ToString(), newItem.Quantity.ToString(), newItem.Subtotal.ToString() };
            for (int i = 0; i < title.Count; i++)
            {
                Label label = new Label();
                label.Text = title[i];
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Width = i == 0 ? 100 : 50;
                smallFlowLayoutPanel.Controls.Add(label);
            }
            bigFlowLayoutPanel.Controls.Add(smallFlowLayoutPanel);
        }
    }
}
