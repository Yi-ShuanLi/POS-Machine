using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static POS點餐機.MenuModel;


namespace POS點餐機
{
    public static class Extension
    {
        public static int GetDigitalCount(this string input)
        {
            int count = 0;
            for(int i = 0; i < input.Length; i++)
            {
                if (Char.IsDigit(input[i]))
                {
                    count++;
                }
            }

            return count;
        }
        public static void CreateCheckBoxs( this  FlowLayoutPanel area , List<string> meal,EventHandler checkedChanged,EventHandler valueChanged)
        {

            for (int i = 0; i < meal.Count; i++)
            {
                FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                flowLayoutPanel.Width = area.Width;
                flowLayoutPanel.Height = 40;
                CheckBox checkBox = new CheckBox();
                checkBox.CheckedChanged += checkedChanged;
                NumericUpDown numericUpDown = new NumericUpDown();
                numericUpDown.ValueChanged += valueChanged;
                checkBox.Text = $"{meal[i]}";
                checkBox.Width = 120;
                numericUpDown.Size = new Size(50, 50);
                flowLayoutPanel.Controls.Add(checkBox);
                flowLayoutPanel.Controls.Add(numericUpDown);
                area.Controls.Add(flowLayoutPanel);
            }
        }
        public static int CaculateAmount(this FlowLayoutPanel area)
        {
            int amount = 0;      
            foreach(FlowLayoutPanel flowLayoutPanel in area.Controls)
            {
                CheckBox checkBox = (CheckBox)flowLayoutPanel.Controls[0];
                NumericUpDown numericUpDown = (NumericUpDown)flowLayoutPanel.Controls[1];
                if (checkBox.Checked)
                {
                    //雞腿飯$90
                    //[雞腿飯,90]
                    string[] stringPrice = checkBox.Text.Split('$');
                    int price = int.Parse(stringPrice[1]);
                    int quantity = (int)numericUpDown.Value;
                    amount += price * quantity;
                }
            }           
            return amount;
        }
        public static void addShowChoose(this FlowLayoutPanel flowLayoutPanel5, CheckBox checkBox, NumericUpDown numericUpDown)
        {
            string[] stringPrice = checkBox.Text.Split('$');
            string itemName = stringPrice[0];
            int unitPrice = int.Parse(stringPrice[1]);
            int quantity = (int)numericUpDown.Value;
            int subTotal = unitPrice * quantity;
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Width = flowLayoutPanel5.Width;
            flowLayoutPanel.Height = 40;
            List<string> title = new List<string> { itemName, unitPrice.ToString(), quantity.ToString(), subTotal.ToString() };
            for (int i = 0; i < title.Count; i++)
            {
                Label label = new Label();
                label.Text = title[i];
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Width = i == 0 ? 100 : 50;
                flowLayoutPanel.Controls.Add(label);
            }
            flowLayoutPanel5.Controls.Add(flowLayoutPanel);
        }
        public static void CheckAreaItems(this FlowLayoutPanel showPanel , FlowLayoutPanel checkedPanel)
        {
            foreach (FlowLayoutPanel panel in checkedPanel.Controls)
            {
                CheckBox checkBox = (CheckBox)panel.Controls[0];
                NumericUpDown numericUpDown = (NumericUpDown)panel.Controls[1];
                if (numericUpDown.Value >= 1)
                    showPanel.addShowChoose(checkBox, numericUpDown);
            }
        }
      
        public static void DisableHorizontalScroll(this FlowLayoutPanel flowLayoutPanel)
        {
            flowLayoutPanel.AutoScroll = false;
            flowLayoutPanel.HorizontalScroll.Enabled = false;
            flowLayoutPanel.HorizontalScroll.Visible = false;
            flowLayoutPanel.HorizontalScroll.Maximum = 0;
            flowLayoutPanel.AutoScroll = true;
        }

        /// <summary>
        /// 產生各類餐點的所有細項
        /// </summary>
        /// <param name="flowLayoutPanel">每個類別的Panel 這是call by reference</param>
        /// <param name="itemnames">餐點的array，Itemname []</param>
        /// <param name="checkedChanged">監控每道菜是否被勾選變化，與CheckBox綁定</param>
        /// <param name="valueChanged">監控每道菜的數量變化，與NumericUpDown綁定</param>
        public static void  CreateMenu(this FlowLayoutPanel flowLayoutPanel, Itemname [] itemnames , EventHandler checkedChanged, EventHandler valueChanged)
        {
           
            for (int j = 0; j < itemnames.Length; j++)
            {
                FlowLayoutPanel minPanel = new FlowLayoutPanel();
                minPanel.Width = flowLayoutPanel.Width;
                minPanel.Height = 30;
                CheckBox checkBox = new CheckBox();
                String mealName = itemnames[j].name;
                String mealPrice = itemnames[j].price.ToString();
                checkBox.Text = mealName + "$" + mealPrice;
                checkBox.CheckedChanged += checkedChanged;
                NumericUpDown numericUpDown = new NumericUpDown();
                numericUpDown.Width = 40;
                numericUpDown.ValueChanged += valueChanged;
                minPanel.Controls.Add(checkBox);
                minPanel.Controls.Add(numericUpDown);
                flowLayoutPanel.Controls.Add(minPanel);
            }
            
        }

    }
}
