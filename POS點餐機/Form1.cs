using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS點餐機
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<MealItem> FrontSelectedMeals = new List<MealItem>();
        List<string> mainMeal = new List<string>() { 
            "酥炸魚排$225","香煎雞肉$250","日式醬燒牛$210","台式爆蔥豬$210" 
            ,"皇家大排$225","酥炸魚排$225","酥炸雞柳$250","經典牛排$300"
        };
        List<string> sideMeal = new List<string>()
        {
            "鮮蔬蛋沙拉$80","凱薩雞肉沙拉$85","牛番茄燉湯$90",
            "昆布海鮮清湯$85","雞肉巧達濃湯$100"
           
        };
        List<string> snack = new List<string>()
        {
            "炸地瓜條$60", "花生麻糬$55", "鹽酥雞小份$70",
            "芋頭西米露$65", "黑糖粉粿$50", "甜不辣拼盤$75",
            "蔥抓餅$55", "蘿蔔糕$50"
        };

        List<string> drink = new List<string>()
        {
            "珍珠奶茶$60", "冬瓜檸檬$55", "四季春青茶$50",
            "紅茶拿鐵$65", "黑糖鮮奶$70", "仙草凍飲$55",
            "檸檬愛玉$50", "芒果冰沙$65"
        };

        
        private void Form1_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.CreateCheckBoxs(mainMeal, CheckedChange,ValueChange);
            flowLayoutPanel2.CreateCheckBoxs(sideMeal, CheckedChange, ValueChange);
            flowLayoutPanel3.CreateCheckBoxs(snack, CheckedChange, ValueChange);
            flowLayoutPanel4.CreateCheckBoxs(drink, CheckedChange, ValueChange);
            ClearAndUpdate();
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
            flowLayoutPanel5.Controls.Clear();
            flowLayoutPanel5.Controls.Add(Order.AddOrder(newItem));          
            
        }

      
        private void CalculateTatalAmount()
        {
            int amount = 0;
            amount += flowLayoutPanel1.CaculateAmount();
            amount += flowLayoutPanel2.CaculateAmount();
            amount += flowLayoutPanel3.CaculateAmount();
            amount += flowLayoutPanel4.CaculateAmount();
            label1.Text = amount.ToString();
        }
    
        private void ClearAndUpdate()
        {
            flowLayoutPanel5.Controls.Clear();
            List<string> title = new List<string> { "品名", "單價", "數量", "小計" };
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Width = flowLayoutPanel5.Width;
            flowLayoutPanel.Height = 40;
            for (int i = 0; i < title.Count; i++)
            {
                Label label = new Label();
                label.Text = title[i];
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Width = i == 0 ? 100 : 50;
                flowLayoutPanel.Controls.Add(label);
            }
            flowLayoutPanel5.Controls.Add(flowLayoutPanel);
            flowLayoutPanel5.CheckAreaItems(flowLayoutPanel1);
            flowLayoutPanel5.CheckAreaItems(flowLayoutPanel2);
            flowLayoutPanel5.CheckAreaItems(flowLayoutPanel3);
            flowLayoutPanel5.CheckAreaItems(flowLayoutPanel4);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
