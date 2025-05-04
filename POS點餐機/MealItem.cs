using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS點餐機
{
     class MealItem
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Subtotal
        {
            get
            {
                return this.Price * this.Quantity;
            }
        }
        public MealItem(string mealNameOnMenu,int quantity)
        {
            string[] stringNameAndPrice = mealNameOnMenu.Split('$');
            this.Name = stringNameAndPrice[0];
            this.Price = int.Parse(stringNameAndPrice[1]);
            this.Quantity = quantity;
        }
        public MealItem(string mealName, int price, int quantity)
        {
            this.Name = mealName;
            this.Price = price;
            this.Quantity = quantity;
        }

    }
}
