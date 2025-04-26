using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS點餐機
{
    class PanelHandler
    {
        public static EventHandler<(FlowLayoutPanel, string)> OnReceivePanel;
        public static void PassPanel((FlowLayoutPanel,string) flowLayoutPanelAndTotal )
        {
            OnReceivePanel?.Invoke(null, flowLayoutPanelAndTotal);
        }
    }
}
