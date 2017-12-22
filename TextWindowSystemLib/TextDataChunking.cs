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
    public struct TextDataChunk
    {
        public SpriteFont font;
        public string data;
        public Rectangle dimensions;
    }

    public struct TextDataLine
    {
        public SpriteFont font;
        public string dataLine;
        public int lineNumber;
        public Rectangle dimensions;
    }

    public abstract class TextDataChunker
    {
        public TextDataChunker()
        {
            //default constructor;
        }

        public abstract List<TextDataChunk> chunkThis(string StringDataIn); 
    }
}
