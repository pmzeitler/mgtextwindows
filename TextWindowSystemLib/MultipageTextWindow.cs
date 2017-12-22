using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace net.PhoebeZeitler.TextWindowSystem
{
    public class MultipageTextWindow : DefaultTextWindow
    {
        public MultipageTextWindow(string windowId, Rectangle dimensions, Color bgColor, string textContents, SpriteFont textFont, Color textColor) : base(windowId, dimensions, bgColor, textContents, textFont, textColor)
        {
            
        }

        public MultipageTextWindow(string windowId, Rectangle dimensions, Color bgColor, string textContents, SpriteFont textFont, Color textColor, WindowBorder border, int buffer) : base(windowId, dimensions, bgColor, textContents, textFont, textColor, border, buffer)
        {

        }

        private bool InitChunkText(string textContents)
        {
            bool retval = true;



            return retval;
        }
    }
}
