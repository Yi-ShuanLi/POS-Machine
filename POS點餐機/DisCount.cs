using POS點餐機.DiscountTypes;
using POS點餐機.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POS點餐機.MenuModel;

namespace POS點餐機
{
    class DisCount
    {
        public static async Task DisCountOrder(OrderRequestModel orderRequestModel)
        {
            //酥炸魚排買二送一
            //香煎雞肉買三個打八折
            //皇家大排搭配黑糖珍奶250元
            //台式蔥爆豬加鹽酥雞小份就送冬瓜檸檬
            //花生麻糬買三個送一盤鮮蔬蛋沙拉
            //酥炸雞柳搭配蘿蔔糕打85折
            //所有飲料任選三杯打8折
            //所有飲料均一價50元
            //所有飲料任選三杯送一杯(送最便宜價格)
            //全場消費滿499折50
            //全場消費打9折
            orderRequestModel.Items.RemoveAll(x => x.Name.Contains("贈")|| x.Name.Contains("折"));
            #region 第一版 簡單工廠
            //ADiscount aDiscount= DiscountFactory.CreatDiscount(orderType, items);
            //aDiscount.Discount();
            #endregion
            #region 第二版 反射取代簡單工廠
            //orderType = "POS點餐機.DiscountTypes." + orderType;
            //Type type = Type.GetType(orderType);
            //ADiscount aDiscount = (ADiscount)Activator.CreateInstance(type, new object[] { items });
            //aDiscount.Discount();
            #endregion
            #region 第三版 反射+策略模式
            if (orderRequestModel.Items.Count != 0)
            {
                StrategyContext strategyContext = null;
                if (orderRequestModel.AIRecommend)
                {
                    strategyContext = new StrategyContext(orderRequestModel.Items);
                }
                else
                {
                    strategyContext = new StrategyContext(orderRequestModel.DiscountStrategy, orderRequestModel.Items);
                }

                await strategyContext.CalcDiscount();
            }
          
            #endregion
            ShowPanel.UpdateSelectedOnShowPanel(orderRequestModel.Items);

        }
    }
}
