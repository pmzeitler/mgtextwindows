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
        const string SPLITTER = " ";  

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
}