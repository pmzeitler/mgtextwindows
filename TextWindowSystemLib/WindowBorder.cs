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

        /// <summary>
        /// Retrieve the margin inside the window that this WindowBorder will draw in.
        /// </summary>
        /// <returns>A Vector4 containing margins as follows: X = top/North, Y = right/East, Z = bottom/South, W = left/West</returns>
        public Vector4 FindMargins()
        {
            Vector4 retval = new Vector4();

            if(this.pipingTop != null)
            {
                retval.X = pipingTop.Height;
            } else
            {
                retval.X = 0;
            }
            if (this.pipingRight != null)
            {
                retval.Y = pipingRight.Width;
            }
            else
            {
                retval.Y = 0;
            }
            if (this.pipingBottom != null)
            {
                retval.Z = pipingBottom.Height;
            }
            else
            {
                retval.Z = 0;
            }
            if (this.pipingLeft != null)
            {
                retval.W = pipingLeft.Width;
            }
            else
            {
                retval.W = 0;
            }
            return retval;
        }

        public Rectangle FindInnerMargin(Rectangle windowSize, int buffer)
        {
            Rectangle retval = Rectangle.Empty;

            Vector4 margins = FindMargins();

            retval.X = buffer + (int)margins.W;
            retval.Y = buffer + (int)margins.X;
            retval.Width = (int)(windowSize.Width - ((margins.Y + buffer) + retval.X));
            retval.Height = (int)(windowSize.Height - ((margins.Z + buffer) + retval.Y));

            return retval;
        }

        /// <summary>
        /// Draws the border. Piping is drawn first, with corners drawn afterward.
        /// </summary>
        /// <param name="sb">A SpriteBatch that has already had "SpriteBatch.Begin" called.</param>
        /// <param name="TextureDimensions">The dimensions of the window texture.</param>
        public virtual void Draw(SpriteBatch sb, Rectangle TextureDimensions)
        {
            Vector4 margins = FindMargins();

            int nextY = (int)margins.X;
            while (nextY < (TextureDimensions.Height - margins.Z))
            {
                sb.Draw(this.pipingLeft, new Vector2(0, nextY), Color.White);
                nextY += this.pipingLeft.Height;
            }
            nextY = (int)margins.X;
            while (nextY < (TextureDimensions.Height - margins.Z))
            {
                sb.Draw(this.pipingRight, new Vector2((TextureDimensions.Width - margins.Y), nextY), Color.White);
                nextY += this.pipingRight.Height;
            }
            int nextX = (int)margins.W;
            while (nextX < (TextureDimensions.Width - margins.Y))
            {
                sb.Draw(this.pipingTop, new Vector2(nextX, 0), Color.White);
                nextX += this.pipingTop.Width;
            }
            nextX = (int)margins.W ;
            while (nextX < (TextureDimensions.Width - margins.Y))
            {
                sb.Draw(this.pipingBottom, new Vector2(nextX, (TextureDimensions.Height - margins.Z)), Color.White);
                nextX += this.pipingBottom.Width;
            }

            sb.Draw(this.cornerUpLf, Vector2.Zero, Color.White);
            sb.Draw(this.cornerUpRt, new Vector2((TextureDimensions.Width - margins.Y), 0), Color.White);
            sb.Draw(this.cornerLoLf, new Vector2(0, (TextureDimensions.Height - margins.Z)), Color.White);
            sb.Draw(this.cornerLoRt, new Vector2((TextureDimensions.Width - margins.Y), (TextureDimensions.Height - margins.Z)), Color.White);


        }
    }
}
