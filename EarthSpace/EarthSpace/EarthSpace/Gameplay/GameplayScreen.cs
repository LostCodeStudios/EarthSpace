using EarthSpace.Gameplay.Drawables;
using EarthSpace.Processing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthSpace.Gameplay
{
    /// <summary>
    /// The gameplay screen.
    /// </summary>
    public class GameplayScreen : EarthSpace.Graphics.IDrawable, IProcess
    {
        #region Fields

        float playerSpeed = 60f;
        PlayerSprite playerSprite;

        #endregion Fields

        #region Initialization

        public GameplayScreen()
        {

        }

        public void LoadContent(ContentManager content)
        {
            playerSprite = new PlayerSprite(content);
        }

        #endregion

        #region IDrawable

        public void Show()
        {
            Begin();

            playerSprite.Show();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public void Hide()
        {
            End();

            playerSprite.Hide();
        }

        #endregion IDrawable

        #region IProcess

        public void Begin()
        {
            ProcessManager.Add(this);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            Vector2 playerVelocity = new Vector2();

            if (keyState.IsKeyDown(Keys.W))
            {
                playerVelocity.Y = -1;
            }
            else if (keyState.IsKeyDown(Keys.S))
            {
                playerVelocity.Y = 1;
            }

            if (keyState.IsKeyDown(Keys.A))
            {
                playerVelocity.X = -1;
            }
            else if (keyState.IsKeyDown(Keys.D))
            {
                playerVelocity.X = 1;
            }

            if (playerVelocity != Vector2.Zero)
            {
                playerVelocity.Normalize();
            }

            playerVelocity *= playerSpeed;

            playerSprite.Position += playerVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (playerVelocity != Vector2.Zero)
            {
                playerSprite.CurrentState = PlayerSprite.State.Moving;
            }
            else
            {
                playerSprite.CurrentState = PlayerSprite.State.Stationary;
            }

            float legRotation = (float)Math.Atan2(playerVelocity.Y, playerVelocity.X);
            playerSprite.LegRotation = legRotation;

            MouseState mouseState = Mouse.GetState();
            Vector2 mousePos = new Vector2(mouseState.X, mouseState.Y);

            Vector2 dir = mousePos - playerSprite.Position;
            if (dir != Vector2.Zero)
            {
                float bodyRot = (float)Math.Atan2(dir.Y, dir.X);
                playerSprite.BodyRotation = bodyRot;
            }
        }

        public void End()
        {
            ProcessManager.Remove(this);
        }

        #endregion IProcess
    }
}
