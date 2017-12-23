using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using net.PhoebeZeitler.TextWindowSystem.TextDataChunking;



/// <summary>
/// This application uses LiteDB for its data acquisition and retrieval.
/// Please see https://github.com/mbdavid/LiteDB/wiki for usage instructions
/// </summary>

namespace net.PhoebeZeitler.TextWindowSystem
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont defaultFont;
        SpriteFont smallFont;
        
        //RenderTarget2D rt;
        DefaultTextWindow window;
        DefaultTextWindow window2;

        WindowLayer layer;

        int transparency = 255;
        Int64 currentTime = 0;

        bool testTransparencyFade = false;

        int framesPerSecond = 60;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //load chunker and positioner
            TextDataChunker throwaway = TextDataChunkerFactory.Chunker;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            defaultFont = Content.Load<SpriteFont>("SystemDefault");
            smallFont = Content.Load<SpriteFont>("SmallDefault");

            Texture2D corner = Content.Load<Texture2D>("WindowBorderCornerDemo");
            Texture2D horiz = Content.Load<Texture2D>("WindowBorderHorizDemo");
            Texture2D vert = Content.Load<Texture2D>("WindowBorderVertDemo");

            WindowBorder border = new WindowBorder(corner, horiz, vert);

            window = new DefaultTextWindow("demo", new Rectangle(250, 500, 500, 150), new Color(Color.DarkRed, 128), "AliceBlueDemoBox", defaultFont, Color.DarkOrchid);
            window.SetupBorder(border, 8);

            window2 = new DefaultTextWindow("demo2", new Rectangle(88, 50, 400, 80), Color.DarkSeaGreen, "SmallDemoBox", defaultFont, Color.Silver);

            layer = new WindowLayer("Layer 0");

            layer.AddWindow(window);
            layer.AddWindow(window2);


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            currentTime += gameTime.ElapsedGameTime.Milliseconds;

            if(testTransparencyFade)
            {
                if (currentTime >= (1000 / framesPerSecond))
                {
                    currentTime = 0;
                    transparency -= 4;
                    if (transparency <= 0)
                    {
                        transparency = 255;
                    }
                    window.SetTransparency(transparency);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            RenderTargetBinding[] oldTarget = spriteBatch.GraphicsDevice.GetRenderTargets();
            
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

                Vector2 textPos = new Vector2(200, 525);
                spriteBatch.DrawString(defaultFont, "Testing", textPos, Color.White);
            spriteBatch.End();

            BlendState blendState = BlendState.NonPremultiplied;

            spriteBatch.Begin(SpriteSortMode.Deferred, blendState);
            /*
            Vector2 textPos = new Vector2(200, 525);
            spriteBatch.DrawString(defaultFont, "Testing", textPos, Color.White);
            //*/

            //window.Draw(spriteBatch);
            //window2.Draw(spriteBatch);

            layer.Draw(spriteBatch);

            spriteBatch.End();

            // TODO: Add your drawing code here



            base.Draw(gameTime);
        }
    }
}
