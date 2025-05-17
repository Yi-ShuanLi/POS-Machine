using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POS點餐機.MenuModel;

namespace POS點餐機.Strategies
{
    abstract class  ADiscountStrategy
    {
        protected List<MealItem> items;
        protected DiscountStrategy discountStrategy;
        
        public ADiscountStrategy(DiscountStrategy discountStrategy, List<MealItem> items)
        {
            this.discountStrategy = discountStrategy;
            this.items = items;
        }
        public abstract void Discount();
    }
}
