﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using net.PhoebeZeitler.TextWindowSystem;

namespace net.PhoebeZeitler.TextWindowSystem.TextDataChunking
{
    public class TextDataChunk
    {
        public SpriteFont font;
        public string data;
        public Rectangle dimensions;
    }

    public class TextDataPage
    {
        private List<TextDataChunk> dataChunks;
        public List<TextDataChunk> DataChunks {
            get
            {
                return dataChunks;
            }
        }

        public TextDataPage()
        {
            dataChunks = new List<TextDataChunk>();
        }
    }

    public class TextDataChunkerFactory
    {
        private static TextDataChunker dataChunker = null;
        private static TextDataPositioner dataPositioner = null;

        private const string DEFAULT_LOCALE = "en-US";
        private const bool DEBUG_THIS = true;
        /*
        static TextDataChunkerFactory()
        {
            String localeSuffix = ConfigurationManager.AppSettings["TextChunker.Locale"];
            string namespacePrefix = "net.PhoebeZeitler.TextWindowSystem.TextDataChunking.";
            string chunkerName = namespacePrefix + "TextDataChunker_" + localeSuffix;
            Debug.WriteLine("Attempting to load chunker \"" + chunkerName + "\"");
            dataChunker = (TextDataChunker)Activator.CreateInstance(Type.GetType(chunkerName));
            string positionerName = namespacePrefix + "TextDataPositioner_" + localeSuffix;
            Debug.WriteLine("Attempting to load positioner \"" + positionerName + "\"");
            dataPositioner = (TextDataPositioner)Activator.CreateInstance(Type.GetType(positionerName));


        }
        */

        public static void SetupChunkers(string localeIn = null)
        {
            string localeToUse = null;
            if (string.IsNullOrWhiteSpace(localeIn))
            {
                if (DEBUG_THIS)
                {
                    Debug.WriteLine("Blank/null locale passed in: \"" + localeIn + "\"; using default  \"" + DEFAULT_LOCALE + "\"");
                }
                localeToUse = DEFAULT_LOCALE;
            }
            else
            {
                localeToUse = localeIn;
            }

            switch(localeToUse)
            {
                case "en-US":
                    dataChunker = new TextDataChunker_enUS();
                    dataPositioner = new TextDataPositioner_enUS();
                    break;
                default:
                    if (DEBUG_THIS)
                    {
                        Debug.WriteLine("Bad locale passed in: \"" + localeIn + "\"; using default PZ implementation of \"en-US\"");
                    }
                    dataChunker = new TextDataChunker_enUS();
                    dataPositioner = new TextDataPositioner_enUS();
                    break;
            }
        }


        public static TextDataChunker Chunker
        {
            get {
                if (dataChunker == null)
                {
                    SetupChunkers();
                }
                return dataChunker;
            }
        }

        public static TextDataPositioner Positioner
        {
            get {
                if (dataPositioner == null)
                {
                    SetupChunkers();
                }
                return dataPositioner;
            }
        }
    }

    public abstract class TextDataChunker
    {
        public TextDataChunker()
        {
            //default constructor;
        }

        public abstract List<TextDataChunk> chunkThis(string StringDataIn); 
    }

    public abstract class TextDataPositioner
    {
        public TextDataPositioner()
        {
            //default constructor;
        }

        public abstract List<TextDataChunk> measureChunks(List<TextDataChunk> dataChunks, SpriteFont font);

        protected abstract String getSpacer();

        private Vector2 spacerDimensions = Vector2.Zero;

        protected Vector2 getSpacerDimensions(SpriteFont font)
        {
            if (spacerDimensions.Equals(Vector2.Zero))
            {
                spacerDimensions = font.MeasureString(getSpacer());
            }
            return spacerDimensions;
        }

        public abstract List<TextDataPage> paginateChunks(List<TextDataChunk> dataChunks, SpriteFont font, Rectangle boundingBox);
    }
}
