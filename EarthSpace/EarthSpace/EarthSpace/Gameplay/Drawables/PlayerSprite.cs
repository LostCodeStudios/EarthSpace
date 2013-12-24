using EarthSpace.Graphics.Drawables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthSpace.Gameplay.Drawables
{
    public class PlayerSprite : EarthSpace.Graphics.IDrawable
    {
        #region Inner Types

        /// <summary>
        /// The state of the player sprite.
        /// </summary>
        public enum State
        {
            Stationary,
            Moving
        }

        #endregion Inner Types

        #region Fields

        Sprite stationarySprite;

        Animation movingSpriteLegs;
        Animation movingSpriteBody;

        State state = State.Stationary;

        #endregion Fields

        #region Initialization

        /// <summary>
        /// Creates a PlayerSprite.
        /// </summary>
        /// <param name="content"></param>
        public PlayerSprite(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Textures/sprites");

            stationarySprite = new Sprite();
            stationarySprite.Texture = texture;
            stationarySprite.Source = new Rectangle(32, 0, 16, 16);
            stationarySprite.Scale = new Vector2(4f);
            stationarySprite.CenterOrigin();

            movingSpriteLegs = new Animation(texture, 0.3f, new Rectangle(48, 16, 32, 16), 2, 1);
            movingSpriteLegs.Scale = new Vector2(4f);
            movingSpriteLegs.LayerDepth = 0.9f;
            movingSpriteLegs.CenterOrigin();

            movingSpriteBody = new Animation(texture, 0.3f, new Rectangle(48, 0, 32, 16), 2, 1);
            movingSpriteBody.Scale = new Vector2(4f);
            movingSpriteBody.LayerDepth = 1f;
            movingSpriteBody.CenterOrigin();

            CurrentState = State.Stationary;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The current state of the player sprite.
        /// </summary>
        public State CurrentState
        {
            get { return state; }
            set
            {
                if (state == value) return;

                state = value;

                switch (state)
                {
                    case State.Stationary:
                        movingSpriteLegs.End();
                        movingSpriteBody.End();

                        movingSpriteLegs.Hide();
                        movingSpriteBody.Hide();

                        stationarySprite.Show();
                        break;

                    case State.Moving:
                        stationarySprite.Hide();

                        movingSpriteLegs.Reset();
                        movingSpriteBody.Reset();

                        movingSpriteLegs.Begin();
                        movingSpriteBody.Begin();

                        movingSpriteLegs.Show();
                        movingSpriteBody.Show();
                        break;
                }
            }
        }

        /// <summary>
        /// The position of the player sprite.
        /// </summary>
        public Vector2 Position
        {
            get { return stationarySprite.Position; }
            set
            {
                stationarySprite.Position = value;

                movingSpriteLegs.Position = value;
                movingSpriteBody.Position = value;
            }
        }

        /// <summary>
        /// The rotation of the player sprite's legs.
        /// </summary>
        public float LegRotation
        {
            get { return movingSpriteLegs.Rotation; }
            set { movingSpriteLegs.Rotation = value; }
        }

        public float BodyRotation
        {
            get { return stationarySprite.Rotation; }
            set
            {
                stationarySprite.Rotation = value;
                movingSpriteBody.Rotation = value;
            }
        }

        #endregion

        #region IDrawable

        public void Show()
        {
            switch (state)
            {
                case State.Stationary:
                    stationarySprite.Show();
                    break;

                case State.Moving:
                    movingSpriteLegs.Show();
                    movingSpriteBody.Show();

                    movingSpriteLegs.Begin();
                    movingSpriteBody.Begin();
                    break;
            }
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {

        }

        public void Hide()
        {
            switch (state)
            {
                case State.Stationary:
                    stationarySprite.Hide();
                    break;

                case State.Moving:
                    movingSpriteLegs.Hide();
                    movingSpriteBody.Hide();

                    movingSpriteLegs.End();
                    movingSpriteBody.End();
                    break;
            }
        }

        #endregion IDrawable
    }
}
