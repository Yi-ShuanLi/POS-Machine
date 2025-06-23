using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS點餐機
{
    class ShowPanel
    {
        /// <summary>
        /// 秀出所有目前選擇的餐點
        /// </summary>
        /// <param name="OrderList">每個前端變動當下，更新後完整(選取與折扣)的OrderList</param>
        public static void UpdateSelectedOnShowPanel(List<MealItem> OrderList)
        {
            //取得所有餐點的總金額
            string total = OrderList.Sum(x => x.Subtotal).ToString();
            FlowLayoutPanel bigFlowLayoutPanel = new FlowLayoutPanel();
            bigFlowLayoutPanel.Width = 420;
            bigFlowLayoutPanel.Height = 589;
            //顯示標題
            List<string> title = new List<string> { "品名", "單價", "數量", "小計" };
            for (int i = 0; i < title.Count; i++)
            {
                Label label = new Label();
                label.Text = title[i];
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Width = i == 0 ? 100 : 50;
                bigFlowLayoutPanel.Controls.Add(label);
            }
            //顯示每個final餐點(包含折扣方案)
            foreach (MealItem mealItem in OrderList)
            {
                //callByReference 包含標題的bigFlowLayoutPanel會透過AMealShow被改變
                AMealShow(bigFlowLayoutPanel, mealItem);
            }
            PanelHandler.PassPanel((bigFlowLayoutPanel, total));
        }
        /// <summary>
        /// 把每個final餐點使用callByReference裝進參數的Panel
        /// </summary>
        /// <param name="bigFlowLayoutPanel">callByReference 包含標題的bigFlowLayoutPanel會透過AMealShow被改變</param>
        /// <param name="newItem">final版餐點折扣</param>
        private static void AMealShow(FlowLayoutPanel bigFlowLayoutPanel, MealItem newItem)
        {
            //每個meal使用new FlowLayoutPanel包著
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
