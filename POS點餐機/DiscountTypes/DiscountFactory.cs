using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace POS點餐機.DiscountTypes
{
    class DiscountFactory
    {
        public static ADiscount CreatDiscount(String discountPlan,List<MealItem>mealItems)
        {
            ADiscount aDiscount = null;
            if (discountPlan == "酥炸魚排買二送一")
            {
                aDiscount = new 酥炸魚排買二送一(mealItems);
            }
            else if (discountPlan == "香煎雞肉買三個打八折")
            {
                aDiscount = new 香煎雞肉買三個打八折(mealItems);
            }
            else if (discountPlan == "皇家大排搭配黑糖鮮奶250元")
            {
                aDiscount = new 皇家大排搭配黑糖鮮奶250元(mealItems);
            }
            else if (discountPlan == "台式蔥爆豬加鹽酥雞小份就送冬瓜檸檬")
            {
                aDiscount = new 台式蔥爆豬加鹽酥雞小份就送冬瓜檸檬(mealItems);
            }
            else if (discountPlan == "花生麻糬買三個送一盤鮮蔬蛋沙拉")
            {
                aDiscount = new 花生麻糬買三個送一盤鮮蔬蛋沙拉(mealItems);
            }
            else if (discountPlan == "酥炸雞柳搭配蘿蔔糕打85折")
            {
                aDiscount = new 酥炸雞柳搭配蘿蔔糕打85折(mealItems);
            }
            else if (discountPlan == "所有飲料任選三杯打8折")
            {
                aDiscount = new 所有飲料任選三杯打8折(mealItems);
            }
            else if (discountPlan == "所有飲料均一價50元")
            {
                aDiscount = new 所有飲料均一價50元(mealItems);
            }
            else if (discountPlan == "所有飲料任選三杯送一杯(送最便宜價格)")
            {
                aDiscount = new 所有飲料任選三杯送一杯送最便宜價格(mealItems);
            }
            else if (discountPlan == "全場消費滿399折50")
            {
                aDiscount = new 全場消費滿399折50(mealItems);
            }
            else if (discountPlan == "全場消費打9折")
            {
                aDiscount = new 全場消費打9折(mealItems);
            }
            return aDiscount;
        }
    }
}
