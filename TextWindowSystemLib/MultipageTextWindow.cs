using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using net.PhoebeZeitler.TextWindowSystem.TextDataChunking;

namespace net.PhoebeZeitler.TextWindowSystem
{
    public class MultipageTextWindow : DefaultTextWindow, InteractiveWindow
    {
        protected int currentPage = 0;
        protected List<TextDataPage> dataPages;
        protected bool hasDisplayedAllPages = false;
        public bool HasDisplayedAllPages
        {
            get
            {
                return this.hasDisplayedAllPages;
            }
        }

        public MultipageTextWindow(string windowId, Rectangle dimensions, Color bgColor, string textContents, SpriteFont textFont, Color textColor) : base(windowId, dimensions, bgColor, textContents, textFont, textColor)
        {
            PrepTextForChunking();
        }

        public MultipageTextWindow(string windowId, Rectangle dimensions, Color bgColor, string textContents, SpriteFont textFont, Color textColor, WindowBorder border, int buffer) : base(windowId, dimensions, bgColor, textContents, textFont, textColor, border, buffer)
        {
            PrepTextForChunking();
        }

        public override void SetupBorder(WindowBorder border, int buffer)
        {
            base.SetupBorder(border, buffer);
            PrepTextForChunking();
        }

        private bool PrepTextForChunking()
        {
            bool retval = true;
            try
            {
                List<TextDataChunk> dataChunks = TextDataChunkerFactory.Chunker.chunkThis(this.textContents);
                dataPages = TextDataChunkerFactory.Positioner.paginateChunks(dataChunks, this.textFont, this.textArea);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception encountered during PrepTextForChunking:");
                Debug.WriteLine(ex.ToString());
                retval = false;
            }

            return retval;
        }

        public void AdvancePage()
        {
            this.cacheValid = false;
            currentPage++;
            if (currentPage >= dataPages.Count)
            {
                this.hasDisplayedAllPages = true;
                currentPage = 0;
            }
        }

        protected override void drawTextContent(SpriteBatch sb)
        {
            //Debug.WriteLine("MultipageTextBox Writing Page " + currentPage + " with " + this.dataPages[currentPage].DataChunks.Count + " chunks");
            
            foreach (TextDataChunk chunk in this.dataPages[currentPage].DataChunks)
            {
                sb.DrawString(textFont, chunk.data, new Vector2(chunk.dimensions.X, chunk.dimensions.Y), textColor);
            }
        }

        public void ProcessInput(WindowInteractions interactionsIn)
        {
            throw new NotImplementedException();
        }

        public WindowResponse GetWindowResponse()
        {
            throw new NotImplementedException();
        }
    }
}
