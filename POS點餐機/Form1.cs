using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static POS點餐機.MenuModel;

namespace POS點餐機
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }
     
        private void Form1_Load(object sender, EventArgs e)
        {
            //路徑的string取得
            String manuPath = ConfigurationManager.AppSettings["MenuPath"].ToString();
            //從路徑取得json
            String manuJson = File.ReadAllText(manuPath);
            //json字串轉 物件 => 反序列化
            //物件轉換成 json字串 => 序列化
            MenuModel menuModel = JsonConvert.DeserializeObject<MenuModel>(manuJson);
            Food[] foods = menuModel.Foods;
            for (int i = 0; i < foods.Length; i++)
            {
                FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                flowLayoutPanel.Width = (foodsContainer.Width / (foods.Length/2))-50;
                flowLayoutPanel.Height= (foodsContainer.Height / 2) - 20;
                flowLayoutPanel.DisableHorizontalScroll();
                #region 藏1
                //FlowLayoutPanel forLabelUse= new FlowLayoutPanel();
                //forLabelUse.Width = flowLayoutPanel.Width;
                //forLabelUse.Height = 15;
                //Label categoryName = new Label();
                //categoryName.Text = foods[i].name.ToString();
                //forLabelUse.Controls.Add(categoryName);
                //flowLayoutPanel.Controls.Add(forLabelUse);
                #endregion
                CreateATitlePanel(flowLayoutPanel, foods[i].name.ToString());
                flowLayoutPanel.CreateMenu(foods[i].itemName);
                #region 藏2
                //for (int j=0;j< foods[i].itemName.Length; j++)
                //{
                //    FlowLayoutPanel minPanel = new FlowLayoutPanel();
                //    minPanel.Width = flowLayoutPanel.Width;
                //    minPanel.Height = 30;
                //    CheckBox checkBox = new CheckBox();
                //    String mealName = foods[i].itemName[j].name;
                //    String mealPrice = foods[i].itemName[j].price.ToString();
                //    checkBox.Text = mealName + "$" + mealPrice;
                //    NumericUpDown numericUpDown = new NumericUpDown();
                //    numericUpDown.Width = 40;
                //    minPanel.Controls.Add(checkBox);
                //    minPanel.Controls.Add(numericUpDown);
                //    flowLayoutPanel.Controls.Add(minPanel);
                //}
                #endregion
                foodsContainer.Controls.Add(flowLayoutPanel);
            }           
            PanelHandler.OnReceivePanel += ReceiveAndShowPanel;
            discountComboBox.DataSource = menuModel.Discounts;
            discountComboBox.DisplayMember = "Name";



        }
        private void CheckedChange(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            FlowLayoutPanel flowLayoutPanel = (FlowLayoutPanel)checkBox.Parent;
            NumericUpDown numericUpDown = (NumericUpDown)flowLayoutPanel.Controls[1];
            if (checkBox.Checked )
            {
                if(numericUpDown.Value == 0)
                    numericUpDown.Value = 1;                
            }
            else
            {
                numericUpDown.Value = 0;
            }            
        }
        private void ValueChange(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            FlowLayoutPanel flowLayoutPanel = (FlowLayoutPanel)numericUpDown.Parent;
            CheckBox checkBox = (CheckBox)flowLayoutPanel.Controls[0];            
            if (numericUpDown.Value <= 0)
            {
                checkBox.Checked = false;               
            }
            if (numericUpDown.Value >= 1)
            {
                checkBox.Checked=true;                              
            }
            MealItem newItem = new MealItem(checkBox.Text, (int)numericUpDown.Value);
            Order.AddOrder((DiscountStrategy)discountComboBox.SelectedValue,newItem);                    
        }         
        private  void ReceiveAndShowPanel(object sender, (FlowLayoutPanel, string) flowLayoutPanelAndTotal)
        {
            flowLayoutPanel5.Controls.Clear();
            flowLayoutPanel5.Controls.Add(flowLayoutPanelAndTotal.Item1);
            label1.Text = flowLayoutPanelAndTotal.Item2;
        }

        private void DiscountComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(discountComboBox.SelectedValue is DiscountStrategy discount)
            {
                Order.RefreshOrder(discount);
            }


        }

       private void CreateATitlePanel(FlowLayoutPanel flowLayoutPanel, String titleName)
        {
            FlowLayoutPanel forLabelUse = new FlowLayoutPanel();
            forLabelUse.Width = flowLayoutPanel.Width;
            forLabelUse.Height = 15;
            Label categoryName = new Label();
            categoryName.Text = titleName;
            forLabelUse.Controls.Add(categoryName);
            flowLayoutPanel.Controls.Add(forLabelUse);
        }
    }
}
