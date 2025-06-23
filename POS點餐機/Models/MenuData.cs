using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static POS點餐機.MenuModel;

namespace POS點餐機.Models
{
    //為何 class名稱不用static ?
    internal class MenuData
    {
        public static Food[] Foods;
        public static DiscountStrategy[] Discounts;
        static MenuData()
        {
            //路徑的string取得
            String manuPath = ConfigurationManager.AppSettings["MenuPath"].ToString();
            //從路徑取得json
            String manuJson = File.ReadAllText(manuPath);
            //json字串轉 物件 => 反序列化
            //物件轉換成 json字串 => 序列化
            MenuModel menuModel = JsonConvert.DeserializeObject<MenuModel>(manuJson);
            Foods = menuModel.Foods;    
            Discounts = menuModel.Discounts;
        }
    }
}
