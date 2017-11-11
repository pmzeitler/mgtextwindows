using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace net.PhoebeZeitler.TextWindowSystem
{
    public class DefaultTextWindow : DrawableTextWindow
    {
        public String windowId {
            get;
            private set;
        } = null;
        protected String textContents { get; private set; } = null;
        public bool isModal { get; set; } = false;

        // Window Dimensions
        protected Rectangle dimensions { get; set; } = new Rectangle(Point.Zero, Point.Zero);

        // Appearance Info 
        protected Rectangle textArea { get; set; } = new Rectangle(Point.Zero, Point.Zero);
        protected Color tintValue { get; set; } = Color.White;
        protected WindowBorder border { get; set; } = null;
        protected int borderBuffer { get; set; } = 5;
        protected Color bgColor { get; set; } = Color.Salmon;

        // Text Settings
        protected SpriteFont textFont { get; private set; } = null;
        protected Color textColor { get; private set; } = Color.White;

        // Texture cache 
        private Texture2D drawableTexture = null;
        protected bool cacheValid { get; set; } = false;


        protected Texture2D DrawableTexture
        {
            get { return drawableTexture; }
            set { drawableTexture = value; }
        }

        public bool SetWindowText(String windowTextIn)
        {
            textContents = windowTextIn;
            return true;
        }

        public void SetupBorder(WindowBorder border, int buffer)
        {
            this.border = border;
            borderBuffer = buffer;
        }

        // Minimum viable drawable text window
        public DefaultTextWindow(String windowId, Rectangle dimensions, Color bgColor, String textContents, SpriteFont textFont, Color textColor)
        {
            this.windowId = windowId;
            this.dimensions = dimensions;
            this.bgColor = bgColor;
            this.textContents = textContents;
            this.textFont = textFont;
            this.textColor = textColor;
        }

        public DefaultTextWindow(String windowId, Rectangle dimensions, Color bgColor, String textContents, SpriteFont textFont, 
            Color textColor, WindowBorder border, int buffer)
            : this (windowId, dimensions, bgColor, textContents, textFont, textColor)
        {
            SetupBorder(border, buffer);
        }

        protected void RenderTexture(SpriteBatch sbIn)
        {
            RenderTarget2D rt = new RenderTarget2D(sbIn.GraphicsDevice, dimensions.Width, dimensions.Height);

            SpriteBatch sb = new SpriteBatch(sbIn.GraphicsDevice);

            RenderTargetBinding[] oldTarget = sbIn.GraphicsDevice.GetRenderTargets();

            sbIn.GraphicsDevice.SetRenderTarget(rt);

            sb.Begin();

            drawBackground(sb);

            if (border != null)
            {
                border.Draw(sb, dimensions);
                textArea = border.FindInnerMargin(dimensions, borderBuffer);
            }

            drawTextContent(sb);

            sb.End();

            sbIn.GraphicsDevice.SetRenderTargets(oldTarget);

            ValidateCache(rt);
        }

        protected virtual void drawBackground(SpriteBatch sb)
        {
            sb.GraphicsDevice.Clear(bgColor);
        }

        protected virtual void drawTextContent(SpriteBatch sb)
        {
            sb.DrawString(textFont, textContents, new Vector2(textArea.Left, textArea.Top), textColor);
        }

        protected virtual void ValidateCache(RenderTarget2D rt)
        {
            drawableTexture = rt;
            cacheValid = true;
        }

        public virtual bool Draw(SpriteBatch sb)
        {
            return this.Draw(sb, this.tintValue);
        }

        public virtual bool Draw(SpriteBatch sb, Color tintValue)
        {
            bool retval = false;

            if ((this.drawableTexture == null) || (!this.cacheValid))
            {
                //rerender the texture
                //System.Console.Error.WriteLine("Rendering Texture at Draw Time");
                RenderTexture(sb);
            }
            try
            {
                sb.Draw(this.DrawableTexture, this.dimensions, tintValue);
                retval = true;
            }
            catch (ArgumentNullException anex)
            {
                System.Console.Error.WriteLine("DrawableTextureWindow was ordered to draw to screen with a null texture parameter");
                System.Console.Error.WriteLine(anex);
                retval = false;
            }
            catch (InvalidOperationException iopex)
            {
                System.Console.Error.WriteLine("DrawableTextureWindow was ordered to draw to screen outside of SpriteBatch.Begin");
                System.Console.Error.WriteLine(iopex);
                retval = false;
            }


            return retval;
        }
    }
}
