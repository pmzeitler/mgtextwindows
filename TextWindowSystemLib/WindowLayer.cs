using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace net.PhoebeZeitler.TextWindowSystem
{
    public class WindowLayer : DrawableWindow
    {
        private String layerId = null;
        private List<DrawableWindow> windows;

        public WindowLayer()
        {
            this.windows = new List<DrawableWindow>();
        }

        public WindowLayer(String layerId) : this()
        {
            this.layerId = layerId;
        }

        public bool AddWindow(DrawableWindow windowIn)
        {
            if (windows.Contains(windowIn))
            {
                return false;
            }
            else
            {
                windows.Add(windowIn);
            }
            return true;
        }

        public bool DropWindow(DrawableWindow windowIn)
        {
            while (windows.Contains(windowIn))
            {
                windows.Remove(windowIn);
            }
            return true;
        }

        public virtual bool Draw(SpriteBatch sb)
        {
            bool retval = true;

            int windowCount = windows.Count;

            for (int i = 0; i < windowCount; i++)
            {
                bool checkval = windows[i].Draw(sb);
                retval = retval && checkval;
            }

            if (!retval)
            {
                System.Console.Error.WriteLine("A window layer (" + layerId + ") contained a window that failed to render properly");
            }

            return retval;
        }

        public virtual bool Draw(SpriteBatch sb, Color tintValue)
        {
            bool retval = true;

            int windowCount = windows.Count;

            for (int i = 0; i < windowCount; i++)
            {
                bool checkval = windows[i].Draw(sb, tintValue);
                retval = retval && checkval;
            }

            if (!retval)
            {
                System.Console.Error.WriteLine("A window layer (" + layerId + ") contained a window that failed to render properly");
            }

            return retval;
        }

        public virtual bool Draw(SpriteBatch sb, int transparency)
        {
            bool retval = true;

            int windowCount = windows.Count;

            for (int i = 0; i < windowCount; i++)
            {
                bool checkval = windows[i].Draw(sb, transparency);
                retval = retval && checkval;
            }

            if (!retval)
            {
                System.Console.Error.WriteLine("A window layer (" + layerId + ") contained a window that failed to render properly");
            }

            return retval;
        }

        public virtual bool Draw(SpriteBatch sb, Color tintValue, int transparency)
        {
            bool retval = true;

            int windowCount = windows.Count;

            for (int i = 0; i < windowCount; i++)
            {
                bool checkval = windows[i].Draw(sb, tintValue, transparency);
                retval = retval && checkval;
            }

            if (!retval)
            {
                System.Console.Error.WriteLine("A window layer (" + layerId + ") contained a window that failed to render properly");
            }

            return retval;
        }
    }
}
