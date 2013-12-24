using EarthSpace.Graphics;
using EarthSpace.Input;
using EarthSpace.Processing;
using EarthSpace.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EarthSpace
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Menu mainMenu;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            IsMouseVisible = true;

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

            GraphicsManager.Initialize(spriteBatch, graphics);
            GraphicsManager.BackgroundColor = Color.Black;
            InputManager.Initialize();

            SpriteFont font = Content.Load<SpriteFont>("Fonts/darkII");

            mainMenu = new Menu("Earth Space", font, font);
            mainMenu.TitleColor = Color.White;
            mainMenu.EntryColor = Color.White;
            mainMenu.EntryColorSelected = Color.Red;
            mainMenu.SelectionSprite.Texture = Content.Load<Texture2D>("Textures/sword");

            mainMenu.AddEntry("Play", null);
            mainMenu.AddEntry("Exit", OnQuit);

            mainMenu.Show();
        

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            InputManager.Update();
            ProcessManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsManager.Draw();

            base.Draw(gameTime);
        }

        private void OnQuit()
        {
            this.Exit();
        }
    }
}