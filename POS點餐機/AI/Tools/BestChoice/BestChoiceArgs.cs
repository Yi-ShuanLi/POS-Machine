using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS點餐機.AI.Tools.DetermineBestChoice
{
    internal class BestChoiceArgs
    {
        public String plan_name { get; set; }
        public String strategy { get; set; }
        public String reason { get; set; }
    }
}
