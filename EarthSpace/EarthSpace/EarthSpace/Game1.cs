using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using EarthSpace.Graphics;
using EarthSpace.Graphics.Drawables;
using EarthSpace.Processing;
using EarthSpace.Input;
using EarthSpace.Input.InputHandlers;
using EarthSpace.Processing.Processes;
using EarthSpace.UI;

namespace EarthSpace
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Sprite sprite;
        Label label;

        ClickHandler clickHandler;
        KeyPressHandler keyHandler;

        Menu testMenu;

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
            InputManager.Initialize();

            sprite = new Sprite();
            sprite.Texture = Content.Load<Texture2D>("spacepirate");
            sprite.Scale = new Vector2(0.3f);
            sprite.Show();

            label = new Label();
            label.Text = "Earth Space";
            label.Font = Content.Load<SpriteFont>("font");
            //label.Show();

            clickHandler = new ClickHandler(MouseButton.Right);
            clickHandler.ClickArea = new Rectangle(0, 0, GraphicsManager.Viewport.Width / 2, GraphicsManager.Viewport.Height / 2);
            clickHandler.OnTrigger += onClick;
            //clickHandler.Enable();

            keyHandler = new KeyPressHandler(Keys.Space);
            keyHandler.OnTrigger += onSpace;
            //keyHandler.Enable();

            DelayProcess testDelay = new DelayProcess(10,
                () => sprite.Hide());
            //testDelay.Begin();

            RecurrentDelayProcess testRecurrentDelay = new RecurrentDelayProcess(0.5f,
                () => {
                    if(GraphicsManager.IsVisible(sprite))
                        sprite.Hide();
                    else
                        sprite.Show();
                    },
                () =>
                    sprite.Show());
            //testRecurrentDelay.Begin();

            SpriteFont font = Content.Load<SpriteFont>("font");

            testMenu = new Menu("Earth Space", font, font);

            testMenu.AddEntry("Show Sprite", showSprite);
            testMenu.AddEntry("Hide Sprite", hideSprite);
            testMenu.AddCancelEntry("Exit");
            testMenu.DisableEntry(0);

            Sprite menuSprite = new Sprite();
            testMenu.SelectionSprite.Texture = Content.Load<Texture2D>("spacepirate");
            testMenu.SelectionSprite.Scale = new Vector2(0.1f);

            testMenu.Show();
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

        void onClick(InputState input)
        {
            if (GraphicsManager.IsVisible(sprite))
            {
                sprite.Hide();
                keyHandler.Disable();
            }
            else
            {
                sprite.Show();
                keyHandler.Enable();
            }
        }

        void onSpace(InputState input)
        {
            if (GraphicsManager.IsVisible(sprite))
            {
                sprite.Hide();
                clickHandler.Disable();
            }
            else
            {
                sprite.Show();
                clickHandler.Enable();
            }
        }

        void hideSprite()
        {
            sprite.Hide();
            testMenu.DisableEntry(1);
            testMenu.EnableEntry(0);
        }

        void showSprite()
        {
            sprite.Show();
            testMenu.DisableEntry(0);
            testMenu.EnableEntry(1);
        }
    }
}
