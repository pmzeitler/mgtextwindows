using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.PhoebeZeitler.TextWindowSystem
{
    interface DrawableTextWindow : DrawableWindow
    {
        bool SetWindowText(String windowTextIn);
    }
}
