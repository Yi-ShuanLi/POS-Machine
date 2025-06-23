using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POS點餐機.MenuModel;

namespace POS點餐機.Models
{
    internal class OrderRequestModel
    {
        //DiscountStrategy discountStrategy, MealItem newItem,bool aiRecommend
        public DiscountStrategy DiscountStrategy { get; set; }
        public MealItem OrderItem { get; set; }
        public List<MealItem> Items {  get; set; }
        public bool AIRecommend {  get; set; }
       public OrderRequestModel(DiscountStrategy discountStrategy, List<MealItem> items, bool aiRecommend)
        {
            this.DiscountStrategy = discountStrategy;
            this.Items = items;
            this.AIRecommend = aiRecommend;
        }
        public OrderRequestModel(DiscountStrategy discountStrategy, MealItem newItem, bool aiRecommend)
        {
            this.DiscountStrategy = discountStrategy;
            this.OrderItem= newItem;
            this.AIRecommend = aiRecommend;

        }
        public OrderRequestModel(DiscountStrategy discountStrategy, bool aiRecommend)
        {
            this.DiscountStrategy = discountStrategy;
            
            this.AIRecommend = aiRecommend;

        }
    }
}
