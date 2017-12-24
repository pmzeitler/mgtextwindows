using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace net.PhoebeZeitler.TextWindowSystem
{
    public class WindowBorder
    {
        protected Texture2D cornerUpLf { get; private set; } = null;
        protected Texture2D cornerUpRt { get; private set; } = null;
        protected Texture2D cornerLoLf { get; private set; } = null;
        protected Texture2D cornerLoRt { get; private set; } = null;

        protected Texture2D pipingTop { get; private set; } = null;
        protected Texture2D pipingBottom { get; private set; } = null;
        protected Texture2D pipingLeft { get; private set; } = null;
        protected Texture2D pipingRight { get; private set; } = null;

        public WindowBorder()
        {
            // default constructor
        }

        /// <summary>
        /// Creates a WindowBorder object that uses one texture for all corners, one texture for all 
        /// vertical piping, and one texture for all horizontal piping. Textures are NOT rotated at
        /// draw time. 
        /// </summary>
        /// <param name="cornerUpLf">The corner texture, to be used in all four corners.</param>
        /// <param name="horizontalPiping">The piping to run along the top and bottom borders.</param>
        /// <param name="verticalPiping">The piping to run along the left and right borders.</param>
        public WindowBorder(Texture2D cornerUpLf, Texture2D horizontalPiping, Texture2D verticalPiping)
        {
            this.cornerUpLf = cornerUpLf;
            this.cornerUpRt = cornerUpLf;
            this.cornerLoLf = cornerUpLf;
            this.cornerLoRt = cornerUpLf;

            this.pipingTop = horizontalPiping;
            this.pipingBottom = horizontalPiping;
            this.pipingLeft = verticalPiping;
            this.pipingRight = verticalPiping;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cornerUpLf">Upper Left corner image</param>
        /// <param name="cornerUpRt">Upper Right corner image</param>
        /// <param name="cornerLoLf">Lower Left corner image</param>
        /// <param name="cornerLoRt">Lower Right corner image</param>
        /// <param name="pipingTop">Top border piping</param>
        /// <param name="pipingBottom">Bottom border piping</param>
        /// <param name="pipingLeft">Left border piping</param>
        /// <param name="pipingRight">Right border piping</param>
        public WindowBorder(Texture2D cornerUpLf, Texture2D cornerUpRt, Texture2D cornerLoLf, Texture2D cornerLoRt, 
            Texture2D pipingTop, Texture2D pipingBottom, Texture2D pipingLeft, Texture2D pipingRight)
        {
            this.cornerUpLf = cornerUpLf;
            this.cornerUpRt = cornerUpRt;
            this.cornerLoLf = cornerLoLf;
            this.cornerLoRt = cornerLoRt;

            this.pipingTop = pipingTop;
            this.pipingBottom = pipingBottom;
            this.pipingLeft = pipingLeft;
            this.pipingRight = pipingRight;
        }

        private struct MarginData
        {
            public int top;
            public int right;
            public int bottom;
            public int left;
        }

        /// <summary>
        /// Retrieve the margin inside the window that this WindowBorder will draw in.
        /// </summary>
        /// <returns>A Vector4 containing margins as follows: X = top/North, Y = right/East, Z = bottom/South, W = left/West</returns>
        private MarginData FindMargins()
        {
            MarginData retval = new MarginData();

            if(this.pipingTop != null)
            {
                retval.top = pipingTop.Height;
            } else
            {
                retval.top = 0;
            }
            if (this.pipingRight != null)
            {
                retval.right = pipingRight.Width;
            }
            else
            {
                retval.right = 0;
            }
            if (this.pipingBottom != null)
            {
                retval.bottom = pipingBottom.Height;
            }
            else
            {
                retval.bottom = 0;
            }
            if (this.pipingLeft != null)
            {
                retval.left = pipingLeft.Width;
            }
            else
            {
                retval.left = 0;
            }
            return retval;
        }

        public Rectangle FindInnerMargin(Rectangle windowSize, int buffer)
        {
            Rectangle retval = Rectangle.Empty;

            MarginData margins = FindMargins();

            retval.X = buffer + (int)margins.left;
            retval.Y = buffer + (int)margins.top;
            retval.Width = (int)(windowSize.Width - (margins.right + buffer));
            retval.Height = (int)(windowSize.Height - (margins.bottom + buffer));

            return retval;
        }

        /// <summary>
        /// Draws the border. Piping is drawn first, with corners drawn afterward.
        /// </summary>
        /// <param name="sb">A SpriteBatch that has already had "SpriteBatch.Begin" called.</param>
        /// <param name="TextureDimensions">The dimensions of the window texture.</param>
        public virtual void Draw(SpriteBatch sb, Rectangle TextureDimensions)
        {
            MarginData margins = FindMargins();

            int nextY = (int)margins.top;
            while (nextY < (TextureDimensions.Height - margins.bottom))
            {
                sb.Draw(this.pipingLeft, new Vector2(0, nextY), Color.White);
                nextY += this.pipingLeft.Height;
            }
            nextY = (int)margins.top;
            while (nextY < (TextureDimensions.Height - margins.bottom))
            {
                sb.Draw(this.pipingRight, new Vector2((TextureDimensions.Width - margins.right), nextY), Color.White);
                nextY += this.pipingRight.Height;
            }
            int nextX = (int)margins.left;
            while (nextX < (TextureDimensions.Width - margins.right))
            {
                sb.Draw(this.pipingTop, new Vector2(nextX, 0), Color.White);
                nextX += this.pipingTop.Width;
            }
            nextX = (int)margins.left ;
            while (nextX < (TextureDimensions.Width - margins.right))
            {
                sb.Draw(this.pipingBottom, new Vector2(nextX, (TextureDimensions.Height - margins.bottom)), Color.White);
                nextX += this.pipingBottom.Width;
            }

            sb.Draw(this.cornerUpLf, Vector2.Zero, Color.White);
            sb.Draw(this.cornerUpRt, new Vector2((TextureDimensions.Width - margins.right), 0), Color.White);
            sb.Draw(this.cornerLoLf, new Vector2(0, (TextureDimensions.Height - margins.bottom)), Color.White);
            sb.Draw(this.cornerLoRt, new Vector2((TextureDimensions.Width - margins.right), (TextureDimensions.Height - margins.bottom)), Color.White);


        }
    }
}
