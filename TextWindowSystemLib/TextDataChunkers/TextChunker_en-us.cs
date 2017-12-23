using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using net.PhoebeZeitler.TextWindowSystem;

namespace net.PhoebeZeitler.TextWindowSystem.TextDataChunking
{
    public class TextDataChunker_enUS : TextDataChunker
    {
        public const string SPLITTER = " ";  

        public override List<TextDataChunk> chunkThis(string StringDataIn)
        {
            List<TextDataChunk> retval = new List<TextDataChunk>();

            String[] rawChunks = StringDataIn.Split(SPLITTER.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < rawChunks.Length; i++)
            {
                TextDataChunk newChunk = new TextDataChunk();
                newChunk.data = rawChunks[i];
                retval.Add(newChunk);
            }
            
            return retval;
        }
    }

    public class TextDataPositioner_enUS : TextDataPositioner
    {
        private const int SPACER_BUFFER_X = 0;
        private const int SPACER_BUFFER_Y = 2;

        protected override string getSpacer()
        {
            return TextDataChunker_enUS.SPLITTER;
        }

        public override List<TextDataChunk> measureChunks(List<TextDataChunk> dataChunks, SpriteFont font)
        {
            for (int i = 0; i < dataChunks.Count; i++)
            {
                TextDataChunk chunk = dataChunks[i];
                chunk.font = font;
                Vector2 stringDimensions = font.MeasureString(chunk.data);
                chunk.dimensions = new Rectangle(0, 0, (int)Math.Ceiling(stringDimensions.X), (int)Math.Ceiling(stringDimensions.Y));
                //we need to put it back in place because TextDataChunk is a struct which is passed by VALUE, not REFERENCE
                dataChunks[i] = chunk;
            }

            return dataChunks;
        }


        public override List<TextDataPage> paginateChunks(List<TextDataChunk> dataChunks, SpriteFont font, Rectangle boundingBox)
        {
            List<TextDataPage> retval = new List<TextDataPage>();
            TextDataPage currentPage = new TextDataPage();
            retval.Add(currentPage);
            

            List<TextDataChunk> measuredChunks = this.measureChunks(dataChunks, font);

            int currentX = boundingBox.X;
            int currentY = boundingBox.Y;

            for (int i = 0; i < measuredChunks.Count; i++)
            {
                TextDataChunk chunk = measuredChunks[i];
                //does it fit on the current line?
                if ((currentX + chunk.dimensions.Width) <= boundingBox.Width)
                {
                    //yes, add it to the current line and advance the pointer
                    AdvancePointer(font, ref currentX, currentY, ref chunk);
                }
                else
                {
                    //OK, can we fit it on the next line?
                    if ((currentY + chunk.dimensions.Height + SPACER_BUFFER_Y) <= boundingBox.Height)
                    {
                        //yes, line return
                        currentX = boundingBox.X;
                        currentY += (chunk.dimensions.Height + SPACER_BUFFER_Y);
                        AdvancePointer(font, ref currentX, currentY, ref chunk);
                    }
                    else
                    {
                        //nope-- add a new page
                        currentPage = new TextDataPage();
                        retval.Add(currentPage);
                        currentX = boundingBox.X;
                        currentY = boundingBox.Y;
                        AdvancePointer(font, ref currentX, currentY, ref chunk);
                    }
                }

                currentPage.DataChunks.Add(chunk);
            }

            return retval;
        }

        private void AdvancePointer(SpriteFont font, ref int currentX, int currentY, ref TextDataChunk chunk)
        {
            chunk.dimensions.X = currentX;
            chunk.dimensions.Y = currentY;
            currentX += (chunk.dimensions.Width + (int)(getSpacerDimensions(font).X) + SPACER_BUFFER_X);
        }
    }
}