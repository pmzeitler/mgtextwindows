using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using net.PhoebeZeitler.TextWindowSystem.TextDataChunking;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TextWindowSystemLibTests
{
    [TestClass]
    public class TextChunkingTesting_enUS
    {
        [TestMethod]
        public void TestBasicChunking()
        {
            string textToChunk = "Hello World!";
            TextDataChunker chunker = new TextDataChunker_enUS();
            List<TextDataChunk> chunks = chunker.chunkThis(textToChunk);

            Assert.AreEqual(2, chunks.Count, "Chunks.Count is incorrect (hello world)");

            textToChunk = "This is a sample test to determine how textChunking works.";
            chunks = chunker.chunkThis(textToChunk);
            Assert.AreEqual(10, chunks.Count, "Chunks.Count is incorrect (test sentence)");

            string checkString = "";
            for (int i = 0; i < chunks.Count; i++)
            {
                checkString += chunks[i].data;
                if ((i + 1) != chunks.Count)
                {
                    checkString += TextDataChunker_enUS.SPLITTER;
                }
            }
            Assert.AreEqual(textToChunk, checkString, "Text order was not preserved during chunking/reassembly");
        }
    }
}
