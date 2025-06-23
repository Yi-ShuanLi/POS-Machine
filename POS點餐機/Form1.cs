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
            // 來源是 MenuData.Foods 的 static欄位 food
            Food[] foods = MenuData.Foods;
            // 餐點有四個類別，用迴圈去跑，詳 MenuData 的json格式
            for (int i = 0; i < foods.Length; i++)
            {
                //在前端左側 foodsContainer 裡面要放入菜單，以新增個 FlowLayoutPanel 的方式
                //每個類別 主餐 附餐 點心 飲料都自成一個 FlowLayoutPanel
                FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                //因為版面設計要成左右分一半所以取外框 foodsContainer.Width 寬度除2
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
                //各類別名稱產生
                CreateATitlePanel(flowLayoutPanel, foods[i].name.ToString());
                //餐點細項的產生
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
                //把每類所有餐點資訊加入到最大的Panel中
                foodsContainer.Controls.Add(flowLayoutPanel);
            }

            //最終定案final Order、總金額的顯示
            //在ShowPanel.UpdateSelectedOnShowPanel被觸發
            //現在這裡是進行綁定方法
            PanelHandler.OnReceivePanel += ReceiveAndShowPanel;
            
            // 撰寫推薦原因及UI折扣選項自動選取時被觸發
            // 在StrategyContext.CalcDiscount()第73列時(AI推薦原因、UI折扣選項自動選取時被觸發)
            // 現在這裡是進行綁定方法
            AIRecommandHandler.OnReceiveRecommand += AIRecommandHandler_OnReceiveRecommand; 
            //折扣方案下拉式選單資料灌入
            discountComboBox.DataSource = MenuData.Discounts;
            //折扣方案下拉式選單資料灌入後所擷取的欄位
            discountComboBox.DisplayMember = "Name";
        }

        /// <summary>
        /// 進行前端的AI推薦原因、UI下拉式選單自動選起
        /// </summary>
        /// <param name="sender">預設null無意義</param>
        /// <param name="e">StrategyContext.CalcDiscount()發完API取得結果後丟入</param>
        private void AIRecommandHandler_OnReceiveRecommand(object sender, BestChoiceArgs e)
        {
            reson_Lab.Text = e.reason;
            discountComboBox.SelectedItem = MenuData.Discounts.First(x => x.Name == e.plan_name);
        }
        /// <summary>
        /// 監控數值改變的function，CheckBox適用
        /// </summary>
        /// <param name="sender">固定寫法</param>
        /// <param name="e">固定寫法</param>
        private void CheckedChange(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            //先取得 透過sender 定位的CheckBox 的Panel爸爸 
            FlowLayoutPanel flowLayoutPanel = (FlowLayoutPanel)checkBox.Parent;
            //再從爸爸Panel去取得 NumericUpDown值，index 0:CheckBox 1:NumericUpDown
            NumericUpDown numericUpDown = (NumericUpDown)flowLayoutPanel.Controls[1];
            //當得知我要選取 checkBox 時，如果 numericUpDown是0要更改成1
            if (checkBox.Checked )
            {
                if(numericUpDown.Value == 0)
                    numericUpDown.Value = 1;                
            }
            else
            {
                //當得知我不選 checkBox 時，numericUpDown的值要歸零
                numericUpDown.Value = 0;
            }            
        }
        /// <summary>
        /// 監控數值改變的function，NumericUpDown適用，順便寫入AI推薦勾選判斷
        /// </summary>
        /// <param name="sender">固定寫法</param>
        /// <param name="e">固定寫法</param>
        private async void ValueChange(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            //先取得 透過sender 定位的NumericUpDown 的Panel爸爸 
            FlowLayoutPanel flowLayoutPanel = (FlowLayoutPanel)numericUpDown.Parent;
            //再從爸爸Panel去取得 checkBox，index 0:CheckBox 1:NumericUpDown
            CheckBox checkBox = (CheckBox)flowLayoutPanel.Controls[0];           
            if (numericUpDown.Value <= 0)
            {
                checkBox.Checked = false;               
            }
            if (numericUpDown.Value >= 1)
            {
                checkBox.Checked=true;                              
            }
            // 取得AI推薦的CheckBox狀態 enableAIRecommend_checkbox.Checked
            bool aiRecommend = enableAIRecommend_checkbox.Checked;
            // 取得當前value change的MealItem
            MealItem newItem = new MealItem(checkBox.Text, (int)numericUpDown.Value);
            // 取得discountComboBox.SelectedValue 把它轉換成DiscountStrategy
            DiscountStrategy discountStrategy = (DiscountStrategy)discountComboBox.SelectedValue;
            // 建立一個OrderRequestModel
            OrderRequestModel orderRequestModel = new OrderRequestModel(discountStrategy, newItem, aiRecommend);
            // 增加Order裡的餐點
            await Order.AddOrder(orderRequestModel);                    
        }
        /// <summary>
        /// 每個前端變動當下的final Order、總金額的顯示，在ShowPanel.UpdateSelectedOnShowPanel被觸發
        /// </summary>
        /// <param name="sender">預設null，無意義</param>
        /// <param name="flowLayoutPanelAndTotal">item1:已經裝好所有final Order的Panel item2:final總金額的string</param>
        private void ReceiveAndShowPanel(object sender, (FlowLayoutPanel, string) flowLayoutPanelAndTotal)
        {
            flowLayoutPanel5.Controls.Clear();
            flowLayoutPanel5.Controls.Add(flowLayoutPanelAndTotal.Item1);
            label1.Text = flowLayoutPanelAndTotal.Item2;
        }

        /// <summary>
        /// UI監控，監控AI推薦的SelectBox、折扣下拉式選單，有變動Refresh
        /// </summary>
        /// <param name="sender">固定格式</param>
        /// <param name="e">前端UI自帶的EventArgs</param>
        private async void DiscountComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool aiRecommend = enableAIRecommend_checkbox.Checked;
            
            if (discountComboBox.SelectedValue is DiscountStrategy discount)
            {
               await Order.RefreshOrder(new OrderRequestModel(discount, aiRecommend));
            }


        }
        /// <summary>
        /// 每個餐點類別的名稱
        /// </summary>
        /// <param name="flowLayoutPanel">參數1 call by reference，每個類別的Panel</param>
        /// <param name="titleName">參數2 類別名稱</param>
        private void CreateATitlePanel(FlowLayoutPanel flowLayoutPanel, String titleName)
        {
            //一樣再產生一個Panel
            FlowLayoutPanel forLabelUse = new FlowLayoutPanel();
            //設定Panel與每個類別的Panel一樣寬
            forLabelUse.Width = flowLayoutPanel.Width;
            forLabelUse.Height = 15;
            Label categoryName = new Label();
            categoryName.Text = titleName;
            forLabelUse.Controls.Add(categoryName);
            flowLayoutPanel.Controls.Add(forLabelUse);
        }
    }
}
