using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.DiscountTypes
{
     abstract class ADiscount
    {
        protected List<MealItem> items;
        public ADiscount(List<MealItem> items)
        {
            this.items = items;
        }
        public abstract void Discount();
       
    }
}
