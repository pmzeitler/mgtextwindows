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
    }
}