using Newtonsoft.Json;
using POS點餐機.AI.Tools.DetermineBestChoice;
using POS點餐機.EventHandlers;
using POS點餐機.Models;
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
            Food[] foods = MenuData.Foods;
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
                flowLayoutPanel.CreateMenu(foods[i].itemName, CheckedChange, ValueChange);
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
            AIRecommandHandler.OnReceiveRecommand += AIRecommandHandler_OnReceiveRecommand; 
            discountComboBox.DataSource = MenuData.Discounts;
            discountComboBox.DisplayMember = "Name";
        }

        private void AIRecommandHandler_OnReceiveRecommand(object sender, BestChoiceArgs e)
        {
            reson_Lab.Text = e.reason;
            discountComboBox.SelectedItem = MenuData.Discounts.First(x => x.Name == e.plan_name);
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
        private async void ValueChange(object sender, EventArgs e)
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
            // enableAIRecommend_checkbox.Checked
            bool aiRecommend = enableAIRecommend_checkbox.Checked;
            MealItem newItem = new MealItem(checkBox.Text, (int)numericUpDown.Value);
            DiscountStrategy discountStrategy = (DiscountStrategy)discountComboBox.SelectedValue;
            OrderRequestModel orderRequestModel = new OrderRequestModel(discountStrategy, newItem, aiRecommend);
            await Order.AddOrder(orderRequestModel);                    
        }         
        private  void ReceiveAndShowPanel(object sender, (FlowLayoutPanel, string) flowLayoutPanelAndTotal)
        {
            flowLayoutPanel5.Controls.Clear();
            flowLayoutPanel5.Controls.Add(flowLayoutPanelAndTotal.Item1);
            label1.Text = flowLayoutPanelAndTotal.Item2;
        }

        private async void DiscountComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool aiRecommend = enableAIRecommend_checkbox.Checked;
            
            if (discountComboBox.SelectedValue is DiscountStrategy discount)
            {
               await Order.RefreshOrder(new OrderRequestModel(discount, aiRecommend));
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
