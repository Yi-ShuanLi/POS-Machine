using POS點餐機.AI.Tools.DetermineBestChoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS點餐機.EventHandlers
{
    internal class AIRecommandHandler
    {
        public static event EventHandler<BestChoiceArgs> OnReceiveRecommand;
        public static void Recommand(BestChoiceArgs recommand)
        {
            OnReceiveRecommand?.Invoke(null, recommand);
        }
    }
}
